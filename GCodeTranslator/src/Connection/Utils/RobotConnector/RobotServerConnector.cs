using System.Net;
using GCodeTranslator.Connection.DTO;
using GCodeTranslator.Connection.Utils.CheckConnection;
using GCodeTranslator.Utils.LogUtils;
using NetMQ;
using NetMQ.Sockets;

namespace GCodeTranslator.Connection.Utils.RobotConnector;


/// <inheritdoc cref="IRobotServerConnector"/>
public class RobotServerConnector : IRobotServerConnector
{
    private static bool _debugModeEnabled;
    private readonly string _ipAddress;
    private readonly int _maxConnectionTime;
    private readonly ConnectionChecker _connectionChecker = new();
    private readonly Logger _logger = LoggerFactory.GetExistingOrCreateNewLogger("root_log");

    public static bool DebugModeEnabled
    {
        set => _debugModeEnabled = value;
    }


    public RobotServerConnector(string ipAddress, int maxConnectionTime)
    {
        _logger.LogWithTime("Creating RobotServerConnector START");
        _logger.Log($"_ipAddress: {ipAddress}\n_maxConnectionTime: {maxConnectionTime}");
        
        _ipAddress = ipAddress;
        _maxConnectionTime = maxConnectionTime;
        
        _logger.LogWithTime("Creating RobotServerConnector END");
    }
    
    public KeyValuePair<string, string> CheckConnection(bool requiredForm = false, 
        bool autoCloseIf0 = true, bool autoCloseIf1 = true, bool autoCloseIf2 = true, bool autoCloseIfMinus1 = false)
    {
        return _connectionChecker.CheckConnection(_ipAddress, _maxConnectionTime, requiredForm,
            autoCloseIf0, autoCloseIf1, autoCloseIf2, autoCloseIfMinus1);
    }

    public void SendFile(string fileName, string filePath)
    {
        _logger.LogWithTime("RobotServerConnector SendFile START");
        _logger.Log($"fileName: {fileName}, filePath: {filePath}");

        if (_debugModeEnabled) return;
        
        
        WebClient client = new WebClient();
        client.Credentials = new NetworkCredential("pi", "8");
        client.UploadFile($"ftp://{_ipAddress}/files/{fileName}", $"{filePath}");
        
        _logger.LogWithTime("RobotServerConnector SendFile END");
    }

    public string SendFileInfo(SendFileInfo sendFileInfo)
    {
        _logger.LogWithTime("RobotServerConnector SendFileInfo START");
        _logger.Log(sendFileInfo.ToString());

        if (_debugModeEnabled) {
            _logger.LogWithTime("RobotServerConnector SendFileInfo END, result: 1");
            return "1";
        }


        using (var client = new RequestSocket())
        {
            client.Connect($"tcp://{_ipAddress}:5000");
            client.SendFrame(sendFileInfo.ToString());
            var receiveFrameString = client.ReceiveFrameString();

            _logger.LogWithTime($"RobotServerConnector SendFileInfo END, result: {receiveFrameString}");
            return receiveFrameString;
        }
    }
}