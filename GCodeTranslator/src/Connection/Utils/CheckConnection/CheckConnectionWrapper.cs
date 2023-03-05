using GCodeTranslator.Utils.DebugUtils.DebugCheckConnection;
using GCodeTranslator.Utils.LogUtils;
using NetMQ;
using NetMQ.Sockets;

namespace GCodeTranslator.Connection.Utils.CheckConnection;

public class CheckConnectionWrapper
{
    private static bool _debugModeEnabled;
    private string _ipAddress = "";
    private int _timeOutInSec;
    private readonly Logger _logger = LoggerFactory.GetExistingOrCreateNewLogger("root_log");

    public static bool DebugModeEnabled
    {
        set => _debugModeEnabled = value;
    }


    // (0Готов  1Печать 2Необходим файл)
    public KeyValuePair<string, string> CheckConnection(string iPAddress, int timeOutInSec)
    {
        if (_debugModeEnabled)
        {
            return new DebugCheckConnectionWrapper().CheckConnection(iPAddress, timeOutInSec);
        }

        return StandardCheckConnection(iPAddress, timeOutInSec);
    }

    private KeyValuePair<string, string> StandardCheckConnection(string iPAddress, int timeOutInSec)
    {
        _logger.Log($"_ipAddress: {iPAddress}\n_timeOutInSec: {timeOutInSec}");
        
        _ipAddress = iPAddress;
        _timeOutInSec = timeOutInSec;

        string state;
        var z = "0";
        string firstMessage;
        string secondMessage;
        using (var socket = new RequestSocket())
        {
            Thread.Sleep(100);
            firstMessage = TryToReceiveMessage(socket);
            secondMessage = TryToReceiveMessage(socket);
        }

        try
        {
            state = ParseState(secondMessage);
            z = ParseZ(secondMessage);
        }
        catch (TimeoutException)
        {
            _logger.LogWithTime("CheckConnectionWrapper StandardCheckConnection TimeoutException");
            throw;
        }
        catch (Exception)
        {
            state = firstMessage.Substring(firstMessage.IndexOf('$') + 1);
        }
        
        _logger.Log($"CheckConnectionWrapper StandardCheckConnection, firstMessage: {_logger.Nst(firstMessage)}, secondMessage: {_logger.Nst(secondMessage)}, state: {_logger.Nst(state)}, z: {z}");
        return new KeyValuePair<string,string>(state, z);
    }

    private string TryToReceiveMessage(RequestSocket socket)
    {
        socket.Connect($"tcp://{_ipAddress}:5000");
        socket.SendFrame("states");
        var success = socket.TryReceiveFrameString(TimeSpan.FromSeconds(_timeOutInSec), out var message);

        if (!success || message == null)
        {
            throw new TimeoutException("Слишком долгое подключение к серверу: " + _ipAddress);
        }

        return message;
    }

    private string ParseState(string message) 
    {
        var state = message.Substring(0, message.IndexOf(';'));
        return state.Substring(state.IndexOf('$') + 1);
    }

    private string ParseZ(string message)
    {
        var z = message.Substring(message.IndexOf(';') + 1);
        return z.Substring(z.IndexOf('$') + 1);
    }
}