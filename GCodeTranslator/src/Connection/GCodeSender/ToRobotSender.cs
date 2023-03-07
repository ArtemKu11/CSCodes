using GCodeTranslator.Connection.DTO;
using GCodeTranslator.Connection.Utils.InfoTextBoxChangeProcessor;
using GCodeTranslator.Connection.Utils.LayersComboBoxChangeProcessor;
using GCodeTranslator.Connection.Utils.RobotConnector;
using GCodeTranslator.Connection.Utils.RobotStateChangeProcessor;
using GCodeTranslator.Connection.Utils.ZHandler;
using GCodeTranslator.Service.DTO;
using GCodeTranslator.Utils.LogUtils;

namespace GCodeTranslator.Connection.GCodeSender;

/// <summary>
/// Класс, хранящий логику порядка отправки роботу файлов. Обертка над <see cref="RobotServerConnector"/>
/// </summary>
public class ToRobotSender
{
    private readonly RobotStateProcessor _robotStateProcessor;
    private readonly InfoTextBoxProcessor _infoTextBoxProcessor;
    private readonly LayersComboBoxProcessor _layersComboBoxProcessor;
    private readonly ZCoordinateHandler _zCoordinateHandler;

    
    private readonly RobotServerConnector _serverConnector;
    private readonly BrowsedFilesToPrint _filesToPrint;
    private int _currentPrintingFileNumber;
    
    private readonly CancellationToken _cancellationToken;
    private bool _isFilePrinted = true;
    
    private readonly Logger _logger = LoggerFactory.GetExistingOrCreateNewLogger("root_log");


    public ToRobotSender(RobotStateProcessor robotStateProcessor, InfoTextBoxProcessor infoTextBoxProcessor, LayersComboBoxProcessor layersComboBoxProcessor,
        RobotServerConnector serverConnector, BrowsedFilesToPrint filesToPrint, int startId, CancellationToken cancellationToken)
    {
        _logger.LogWithTime("ToRobotSender CONSTR START");
        _logger.Log(filesToPrint + "\n" + "Стартовый id: " + startId);
        
        _robotStateProcessor = robotStateProcessor;
        _infoTextBoxProcessor = infoTextBoxProcessor;
        _layersComboBoxProcessor = layersComboBoxProcessor;
        _zCoordinateHandler = new ZCoordinateHandler();
        _serverConnector = serverConnector;
        _filesToPrint = filesToPrint;
        _currentPrintingFileNumber = startId;
        _cancellationToken = cancellationToken;
        
        _logger.LogWithTime("ToRobotSender CONSTR END");
    }

    public void PrintAll()
    {
        _logger.LogWithTime("ToRobotSender PrintAll START");


        if (_filesToPrint.Count == 0)
        {
            HandleEmptyTp();
            
            _logger.LogWithTime("ToRobotSender PrintAll END EMPTY");
            return;
        }

        while (_currentPrintingFileNumber < _filesToPrint.Count)
        {
            if (_cancellationToken.IsCancellationRequested)
            {
                _logger.LogWithTime("ToRobotSender PrintAll END CANCEL");
                return;
            }
            _layersComboBoxProcessor.SetItem(_currentPrintingFileNumber);
            PrintOne(false);
            ++_currentPrintingFileNumber;
        }
        
        _logger.LogWithTime("ToRobotSender PrintAll END");
    }

    public void PrintOne(bool awaitLayerChecked = true)
    {
        _logger.LogWithTime("ToRobotSender PrintOne START");

        
        if (_currentPrintingFileNumber == _filesToPrint.Count)
        {
            _isFilePrinted = true;
            SendFile(awaitLayerChecked, true);
        }
        else
        {
            _isFilePrinted = true;
            SendFile(awaitLayerChecked);
        }
        DefaultPrintHandleProcessors();
        Thread.Sleep(2000);
        WaitUntilPrinting();
        
        _logger.LogWithTime("ToRobotSender PrintOne END");
    }

    private void WaitUntilPrinting()
    {

        while (!IsSendNextFile())
        {
            if (_cancellationToken.IsCancellationRequested) return;
            Thread.Sleep(100);
        }
        

    }
    
    private void HandleEmptyTp()
    {
        _robotStateProcessor.SetState("Ошибка. Не найдено файлов .tp для отправки", Color.Orange);
        _infoTextBoxProcessor.PrintLine("Ошибка. Не найдено файлов .tp для отправки");
    }
    
    private void DefaultPrintHandleProcessors(bool checkDuplicate = false)
    {
        if (_cancellationToken.IsCancellationRequested) return;

        var message = $"Printing file {_currentPrintingFileNumber + 1}/{_filesToPrint.Count}";
        _infoTextBoxProcessor.PrintLine(message, checkDuplicate);
        _robotStateProcessor.SetState(message, Color.Green);
    }

    private bool IsSendNextFile()
    {
        var result = _serverConnector.CheckConnection();
        if (_cancellationToken.IsCancellationRequested) return false;
        return ParseSendNextFileCheckConnectionResult(result);

    }

    private bool ParseSendNextFileCheckConnectionResult(KeyValuePair<string, string> result)
    {
        var state = result.Key;
        var zCoord = result.Value;
        switch (state)
        {
            case "0":  // Готов
                _robotStateProcessor.SetState("Ready to print", Color.Green);
                if (_isFilePrinted)
                {
                    _isFilePrinted = false;
                    return true;
                }
                return false;
            case "1":  // Печать
                Handle1Or2Result(zCoord);
                return false;
            case "2":  // Необходим файл
                Handle1Or2Result(zCoord);
                return false;
            case "-1":  // TimeoutException
                var message = "Сервер не отвечает";
                _infoTextBoxProcessor.PrintLine(message);
                _robotStateProcessor.SetState(message, Color.Red);
                return false;
            default:
                _robotStateProcessor.SetState("Connection Error", Color.Red);
                return false;
        }
    }

    private void Handle1Or2Result(string zCoord) 
    {
        if (zCoord.Equals("0"))
        {
            DefaultPrintHandleProcessors(true);
        }
        else
        {
            _zCoordinateHandler.ResolveZCoordinate(zCoord);
            var zCoordinateInfo = _zCoordinateHandler.GetAsString();
            if (zCoordinateInfo != null)
            {
                _infoTextBoxProcessor.PrintLine(zCoordinateInfo, true);
            }
            _robotStateProcessor.SetState($"Current Height Z = {zCoord}", Color.Green);
        }
    }
    
    private void SendFile(bool awaitLayerChecked, bool isLastFile = false)
    {
        var fileName = _filesToPrint.FilesList[_currentPrintingFileNumber];
        var filePath = _filesToPrint.Path + "\\" + fileName;
        
        SendFileInfo sendFileInfo;
        if (isLastFile)
        {
            sendFileInfo = new SendFileInfo
            {
                CurrentFileName = fileName,
                IsStart = true,
                StopAfterLayer = awaitLayerChecked,
                IsLastFile = true
            };
        }
        else
        {
            sendFileInfo = new SendFileInfo
            {
                CurrentFileName = fileName,
                IsStart = true,
                StopAfterLayer = awaitLayerChecked,
            };
        }
        
        
        _serverConnector.SendFile(fileName, filePath);
        var result = _serverConnector.SendFileInfo(sendFileInfo);
        if (!result.Equals("1"))
        {
            _robotStateProcessor.SetState("Format Error", Color.Red);
        }
    }
}