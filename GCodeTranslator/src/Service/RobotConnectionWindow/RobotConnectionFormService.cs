using GCodeTranslator.Connection.DTO;
using GCodeTranslator.Connection.GCodeSender;
using GCodeTranslator.Connection.Utils.InfoTextBoxChangeProcessor;
using GCodeTranslator.Connection.Utils.LayersComboBoxChangeProcessor;
using GCodeTranslator.Connection.Utils.RobotConnector;
using GCodeTranslator.Connection.Utils.RobotStateChangeProcessor;
using GCodeTranslator.Connection.Utils.Timers;
using GCodeTranslator.Forms.RobotConnectionWindow;
using GCodeTranslator.Parsing.TpConverter;
using GCodeTranslator.Service.DTO;
using GCodeTranslator.Utils.LogUtils;

namespace GCodeTranslator.Service.RobotConnectionWindow;

public class RobotConnectionFormService
{
    private readonly RobotConnectionForm _robotConnectionForm;
    private readonly RequiredPropertiesForConnection _propertiesForConnection;
    private readonly RobotServerConnector _robotServerConnector;
    
    private readonly InfoTextBoxProcessor _infoTextProcessor;
    private readonly RobotStateProcessor _robotStateProcessor;
    private readonly LayersComboBoxProcessor _layersComboBoxProcessor;
    
    private readonly ToTpConverterStateTimer _tpStatePrintingTimer;
    private CancellationTokenSource _tokenSource = new();
    private CancellationToken _cancellationToken;
    
    private BrowsedFilesToPrint _browsedFilesToPrint;
    
    private readonly Logger _exceptionLogger = LoggerFactory.GetExistingOrCreateNewLogger("exception_log");
    private readonly Logger _logger = LoggerFactory.GetAppendableLogger("root_log");



    public RobotConnectionFormService(RobotConnectionForm robotConnectionForm, RequiredPropertiesForConnection propertiesForConnection)
    {
        
        _robotConnectionForm = robotConnectionForm;
        _propertiesForConnection = propertiesForConnection;
        _robotServerConnector = propertiesForConnection.RobotServerConnector;
        
        _robotStateProcessor = ResolveRobotStateProcessor();
        _infoTextProcessor = CreateInfoTextBoxProcessor();
        _layersComboBoxProcessor = new LayersComboBoxProcessor(_robotConnectionForm.LayersComboBox, _robotConnectionForm);
        
        _tpStatePrintingTimer = new ToTpConverterStateTimer(_infoTextProcessor);
        _cancellationToken = _tokenSource.Token;
            
        _robotConnectionForm.Text = propertiesForConnection.IpAddress;
        
    }
    
    private RobotStateProcessor ResolveRobotStateProcessor()
    {
        
        var robotStateProcessor = _propertiesForConnection.RobotStateProcessor;
        robotStateProcessor.SetRobotConnectionFormProperties(_robotConnectionForm, _robotConnectionForm.RobotStateLabel);
        robotStateProcessor.SetState(_propertiesForConnection.StateBeforeOpenForm, _propertiesForConnection.ColorOfStateBeforeOpenForm);
        
        return robotStateProcessor;
    }
    
    private InfoTextBoxProcessor CreateInfoTextBoxProcessor()
    {
        
        var infoTextBoxProcessor = new InfoTextBoxProcessor();
        infoTextBoxProcessor.SetRobotConnectionFormProperties(_robotConnectionForm, _robotConnectionForm.PrintInfoTextBox);
        
        return infoTextBoxProcessor;
    }
    
    public void SelectPrevLayer()
    {
        
        var layersComboBox = _robotConnectionForm.LayersComboBox;
        
        if (layersComboBox.SelectedItem != null)
            if (layersComboBox.SelectedIndex > 0)
            {
                layersComboBox.SelectedIndex--;
            }
        
    }
    
    public void SelectNextLayer()
    {
        
        var layersComboBox = _robotConnectionForm.LayersComboBox;

        if (layersComboBox.SelectedItem != null)
            if (layersComboBox.SelectedIndex < layersComboBox.Items.Count-1)
            {
                layersComboBox.SelectedIndex++;
            }
        
    }

    public void SelectFolder()
    {
        
        var selectedDirectory = ShowFolderBrowserDialog();
        if (selectedDirectory == null) return;
        ClearLayerComboBox();
        _robotConnectionForm.FolderTextBox.Text = selectedDirectory;
        HandleSelectedDirectoryFiles(selectedDirectory);
        ResolveLayerComboBoxDropDownSize();
        
    }

    private void ClearLayerComboBox()
    {
        
        _robotConnectionForm.LayersComboBox.Items.Clear();
        _robotConnectionForm.LayersComboBox.DropDownHeight = 10;
        
    }

    private void ResolveLayerComboBoxDropDownSize()
    {
        
        var layersComboBox = _robotConnectionForm.LayersComboBox;
        
        var height = layersComboBox.Items.Count *
                     layersComboBox.ItemHeight + 10;
        if (height == 0)
        {
            height = 10;
        }

        if (height > 500)
        {
            height = 500;
        }

        layersComboBox.DropDownHeight = height;
        
    }
    
    private string? ShowFolderBrowserDialog()
    {
        
        var folderBrowserDialog = new FolderBrowserDialog();
        folderBrowserDialog.ShowNewFolderButton = false;
        if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
        {
            return folderBrowserDialog.SelectedPath;
        }

        return null;
    }

    private void HandleSelectedDirectoryFiles(string selectedDirectory)
    {
        
        var listOfTpFiles = GetListOfTpFiles(selectedDirectory);
        if (listOfTpFiles.Count > 0)
        {
            RefreshBrowsedFilesToPrint(listOfTpFiles, selectedDirectory);
            AddToComboBox(listOfTpFiles);
            _robotConnectionForm.LayersComboBox.SelectedItem = _robotConnectionForm.LayersComboBox.Items[0];
            _robotConnectionForm.StartPrintingButton.Enabled = true;
        }
        else
        {
            _robotConnectionForm.StartPrintingButton.Enabled = false;
        }
        
    }

    private List<string> GetListOfTpFiles(string selectedDirectory)
    {
        
        var directoryName = selectedDirectory.Substring(selectedDirectory.LastIndexOf("\\") + 1);
        var fileList = ResolveFirstFile(selectedDirectory, directoryName);
        if (fileList.Count == 0)
        {
            return fileList;
        }
        ResolveAnotherFiles(selectedDirectory, directoryName, fileList);
        
        return fileList;
    }
    
    private List<string> ResolveFirstFile(string path, string directoryName)
    {
        
        var files = new List<string>();
            
        if (File.Exists(path + "\\" + directoryName + ".tp"))
        {
            files.Add(directoryName + ".tp");
        }
        else if (File.Exists(path + "\\" + directoryName + $"_1" + ".tp"))
        {
            files.Add(directoryName + $"_1" + ".tp");
        }

        return files;
    }
    
    private void ResolveAnotherFiles(string path, string directoryName, List<string> fileList)
    {
        
        var filesCount = new DirectoryInfo(path).GetFiles().Length;

        for (int i = 1; i < filesCount; i++)
        {
            if (File.Exists(path + "\\" + directoryName + $"_{i+1}" + ".tp"))
            { 
                fileList.Add(directoryName + $"_{i+1}" + ".tp");
            }
            else
            {
                return;
            }
        }
        
    }

    private void RefreshBrowsedFilesToPrint(List<string> listOfTpFiles, string selectedDirectory)
    {
        
        _browsedFilesToPrint.Path = selectedDirectory;
        _browsedFilesToPrint.DirectoryName = selectedDirectory.Substring(selectedDirectory.LastIndexOf("\\") + 1);
        _browsedFilesToPrint.Count = listOfTpFiles.Count;
        _browsedFilesToPrint.FilesList = listOfTpFiles;
        
    }
    
    private void AddToComboBox(List<string> listOfTpFiles)
    {
        
        var layersComboBox = _robotConnectionForm.LayersComboBox;
        
        foreach (var fileName in listOfTpFiles)
        {
            layersComboBox.Items.Add(fileName);
        }
        
    }

    public void HandleExportToTp()
    {
        
        if (_robotConnectionForm.ExportOneCheckBox.Checked)
        {
            HandleOneFileConverting();
        }
        else
        {
            if (IsFolderSelected())
            {
                HandleAllFilesConverting();
            }
            else
            {
                MessageBox.Show("Сначала выберите папку");
            }
        }
        
    }
    
    private void HandleOneFileConverting()
    {
        
        var tpOpenFileDialog = CreateTpOpenFileDialog();
        var dialogResult = tpOpenFileDialog.ShowDialog();
        if (dialogResult == DialogResult.OK)
        {
            var filePath = tpOpenFileDialog.FileName;
            new ToTpConverter().ConvertOneFile(filePath);
        }
        
    }

    private OpenFileDialog CreateTpOpenFileDialog()
    {
        
        OpenFileDialog fileDialog = new OpenFileDialog();
        fileDialog.Filter = "ls files (*.ls)|*.ls|All files (*.*)|*.*";
        fileDialog.FilterIndex = 2;
        fileDialog.RestoreDirectory = true;
        
        return fileDialog;
    }

    private bool IsFolderSelected()
    {

        return !(_robotConnectionForm.FolderTextBox.Text == null || _robotConnectionForm.FolderTextBox.Text.Equals(""));
    }
    
    private void HandleAllFilesConverting()
    {
        
        var resultMessage = new ToTpConverter().ConvertAllFiles(_robotConnectionForm.FolderTextBox.Text);
        if (resultMessage == "1")
        {
            _tpStatePrintingTimer.Start();
        }
        
    }

    public void EliminateEverything()
    {
        
        _tokenSource.Cancel();
        _tpStatePrintingTimer.Close();
        
    }

    public void ResolveNewSizeAndLocation(int newWidth, int newHeight)
    {
        var mainPanel = _robotConnectionForm.MainPanel;
        var mainContentConnectionToRobotPanel = _robotConnectionForm.MainContentConnectionToRobotPanel;
        mainPanel.Size = new Size(newWidth - 40, newHeight - 63);

        var contentWidth = mainContentConnectionToRobotPanel.Size.Width;
        var contentHeight = mainContentConnectionToRobotPanel.Size.Height;
            
            
        mainContentConnectionToRobotPanel.Location = new Point(mainPanel.Size.Width / 2 - contentWidth / 2, mainPanel.Size.Height / 2 - contentHeight / 2);
    }

    public void Reset()
    {
        
        _tokenSource.Cancel();
        EnableForm();
        _robotConnectionForm.FolderTextBox.Text = "";
        ClearLayerComboBox();
        _robotConnectionForm.AwaitLayerCheckBox.Checked = false;
        _robotConnectionForm.ExportOneCheckBox.Checked = false;
        _robotStateProcessor.SetState("Unknown state", Color.Black);
        _robotConnectionForm.StartPrintingButton.Enabled = false;
        _robotConnectionForm.PrintInfoTextBox.Text = "";
        
    }

    public void StartPrinting()
    {
        _logger.LogWithTime("RobotConnectionFormServce StartPrinting START");
        
        if (IsConnectionSuccessful())
        {
            RefreshCancellationToken();
            var printTask = new Task(() =>
            {
                try
                {
                    AsyncStartPrinting(_cancellationToken);
                }
                catch (Exception e)
                {
                    _logger.LogException(e);
                    _exceptionLogger.LogException(e);
                    throw;
                }
            });
            printTask.Start();
        }
        
        _logger.LogWithTime("RobotConnectionFormServce StartPrinting END");

    }

    private bool IsConnectionSuccessful()
    {
        
        var (state, _) = _robotServerConnector.CheckConnection(true);
        ParseState(state);
        if (!state.Equals("-1"))
        {
            return true;
        }

        return false;
    }

    private void AsyncStartPrinting(CancellationToken cancellationToken)
    {
        
        _robotConnectionForm.Invoke(DisableForm);

        var startId = _robotConnectionForm.Invoke(GetStartId);
        var toRobotSender = new ToRobotSender(_robotStateProcessor, _infoTextProcessor, _layersComboBoxProcessor, _robotServerConnector, _browsedFilesToPrint,
            startId, cancellationToken);
        
        if (_robotConnectionForm.AwaitLayerCheckBox.Checked)
        {
            toRobotSender.PrintOne();
        }
        else
        {
            toRobotSender.PrintAll();
        }
        
        AfterPrintingProcessing(cancellationToken);
        
    }

    private int GetStartId()
    {
        return _robotConnectionForm.LayersComboBox.SelectedIndex;
    }

    private void RefreshCancellationToken()
    {
        
        _tokenSource = new CancellationTokenSource();
        _cancellationToken = _tokenSource.Token;
        
    }

    private void AfterPrintingProcessing(CancellationToken cancellationToken)
    {
        
        if (!cancellationToken.IsCancellationRequested)
        {
            _robotConnectionForm.Invoke(EnableForm);
            SetStateAfterPrinting();
            MessageBox.Show("Печать завершена!");
        }
        
    }

    private void SetStateAfterPrinting()
    {
        
        if (_cancellationToken.IsCancellationRequested)
        {
            _robotStateProcessor.SetState("Unknown state", Color.Black);

        }
        else
        {
            _robotStateProcessor.SetState("Ready to print", Color.Green);
            _infoTextProcessor.PrintLine("Печать завершена!");
        }
        
    }

    private void DisableForm()
    {
        
        DisableEnableForm(false);
        
    }

    private void EnableForm()
    {
        
        DisableEnableForm(true);
        
    }

    private void DisableEnableForm(bool enabledValue)
    {
        
        _robotConnectionForm.StartPrintingButton.Enabled = enabledValue;
        _robotConnectionForm.LayersComboBox.Enabled = enabledValue;
        _robotConnectionForm.AwaitLayerCheckBox.Enabled = enabledValue;
        _robotConnectionForm.BrowseFolderButton.Enabled = enabledValue;
        _robotConnectionForm.ExportOneCheckBox.Enabled = enabledValue;
        _robotConnectionForm.ExportToTpButton.Enabled = enabledValue;
        _robotConnectionForm.NextButton.Enabled = enabledValue;
        _robotConnectionForm.PrevButton.Enabled = enabledValue;
        _robotConnectionForm.RepeatButton.Enabled = enabledValue;
        _robotConnectionForm.RefreshStateButton.Enabled = enabledValue;
        
    }

    public void RefreshRobotState()
    {
        
        var (state, _) = _robotServerConnector.CheckConnection(true);
        ParseState(state);
        
    }

    private void ParseState(string state)
    {
        
        switch (state)
        {
            case "0":  // Готов
                _robotStateProcessor.SetState("Ready to print", Color.Green);
                break;
            case "1":  // Печать
                _robotStateProcessor.SetState("Printing file", Color.Green);
                break;
            case "2":  // Необходим файл
                _robotStateProcessor.SetState("File required / Printing file", Color.Green);
                break;
            case "-1":  // TimeoutException
                _robotStateProcessor.SetState("Сервер не отвечает", Color.Red);
                break;
            default:
                _robotStateProcessor.SetState("Connection Error", Color.Red);
                break;
        }
        
    }
}