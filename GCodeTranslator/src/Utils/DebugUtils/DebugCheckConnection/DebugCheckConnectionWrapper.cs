using GCodeTranslator.Connection.Utils.CheckConnection;
using GCodeTranslator.Utils.DebugUtils.DebugWindow;
using GCodeTranslator.Utils.DebugUtils.MessageBoxWithTextBox;
using GCodeTranslator.Utils.DebugUtils.StatusLabelProcessor;
using GCodeTranslator.Utils.LogUtils;
using NetMQ;
using NetMQ.Sockets;

namespace GCodeTranslator.Utils.DebugUtils.DebugCheckConnection;

/// <summary>
/// Подсовывается вместо обычного <see cref="CheckConnectionWrapper"/> в <see cref="ConnectionChecker"/>
/// при включении дебаг-режима. Отключает соединение с сервером. Возвращает определенный в <see cref="DebugWindowForm"/>
/// результат проверки соединения
/// </summary>
public class DebugCheckConnectionWrapper
{
    private static readonly object Locker = new();
    private static string? _ipAddress;
    private static int _timeOutInSec;
    private static string _resultState = "1";
    private static string _resultZ = "0";
    private static bool _realConnectionRequired;
    private static string _realResultMessage = "";
    private static StatusLabelChangeProcessor? _statusLabelChangeProcessor;
    private static bool _messageBoxRequired;

    private readonly Logger _logger = LoggerFactory.GetExistingOrCreateNewLogger("root_log");


    public static string IpAddress
    {
        get
        {
            lock (Locker)
            {
                if (_ipAddress != null)
                {
                    return _ipAddress;
                }
                else
                {
                    return "";
                }
            }
        }
        set
        {
            lock (Locker)
            {
                _ipAddress = value;

            }
        }
    }
    
    public static int TimeOutInSec
    {
        get
        {
            lock (Locker)
            {
                return _timeOutInSec;

            }
        }
        set
        {
            lock (Locker)
            {
                _timeOutInSec = value;

            }
        }
    }
    
    public static string ResultState
    {
        get
        {
            lock (Locker)
            {
                return _resultState;

            }
        }
        set
        {
            lock (Locker)
            {
                _resultState = value;

            }
        }
    }
    
    public static string ResultZ
    {
        get
        {
            lock (Locker)
            {
                return _resultZ;

            }
        }
        set
        {
            lock (Locker)
            {
                _resultZ = value;

            }
        }
    }

    public static string RealResultMessage
    {
        get
        {
            lock (Locker)
            {
                return _realResultMessage;
            }
        }
        set
        {
            lock (Locker)
            {
                _realResultMessage = value;
            }
        }
    }

    public static StatusLabelChangeProcessor StatusLabelChangeProcessor
    {
        set
        {
            lock (Locker)
            {
                _statusLabelChangeProcessor = value;
            }
        }
    }

    public static bool RealConnectionRequired
    {
        get => _realConnectionRequired;
        set => _realConnectionRequired = value;
    }

    public static bool MessageBoxRequired
    {
        set => _messageBoxRequired = value;
    }
    
    public KeyValuePair<string, string> CheckConnection(string iPAddress, int timeOutInSec)
    {
        if (RealConnectionRequired)
        {
            return RealCheckConnection();
        }

        return MockCheckConnection();
    }

    

    private KeyValuePair<string, string> MockCheckConnection()
    {
        _statusLabelChangeProcessor?.SetState("Запрошено соединение", Color.DarkOrange);

        if (_messageBoxRequired)
        {
            var textMessageBox = new TextMessageBox();
            textMessageBox.ShowDialog(ResultState, ResultZ);
            ResultState = textMessageBox.State;
            ResultZ = textMessageBox.Z;
            textMessageBox.Dispose();
        }
        
        Thread.Sleep(TimeOutInSec * 1000);


        if (ResultState.Equals("-1"))
        {
            _statusLabelChangeProcessor?.SetState($"Получен результат - state: {-1}, z (не факт): {ResultZ}", Color.Green);
            _logger.LogWithTime("MockCheckConnection DebugCheckConnectionWrapper Timeout Exception");
            throw new TimeoutException();
        }

        _statusLabelChangeProcessor?.SetState($"Получен результат - state: {ResultState}, z: {ResultZ}", Color.Green);
        _logger.LogWithTime($"MockCheckConnection DebugCheckConnectionWrapper Получен результат - state: {ResultState}, z: {ResultZ}");
        return new KeyValuePair<string, string>(ResultState, ResultZ);
    }

    private KeyValuePair<string, string> RealCheckConnection()
    {

        string state;
        var z = "0";
        string firstMessage;
        string secondMessage;
        using (var socket = new RequestSocket())
        {
            Thread.Sleep(100);
            firstMessage = TryToReceiveMessage(socket);
            secondMessage = TryToReceiveMessage(socket);
            RealResultMessage = secondMessage;
        }
        
        try
        {
            state = ParseState(secondMessage);
            z = ParseZ(secondMessage);
        }
        catch (Exception)
        {
            state = firstMessage.Substring(firstMessage.IndexOf('$') + 1);
        }

        return new KeyValuePair<string,string>(state, z);
    }
    
    private string TryToReceiveMessage(RequestSocket socket)
    {
        socket.Connect($"tcp://{IpAddress}:5000");
        socket.SendFrame("states");
        var success = socket.TryReceiveFrameString(TimeSpan.FromSeconds(TimeOutInSec), out var message);

        if (!success || message == null)
        {
            throw new TimeoutException("Слишком долгое подключение к серверу: " + IpAddress);
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