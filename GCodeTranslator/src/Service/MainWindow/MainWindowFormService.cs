using GCodeTranslator.CmdProcessRunner;
using GCodeTranslator.Connection.DTO;
using GCodeTranslator.Connection.Utils.RobotConnector;
using GCodeTranslator.Connection.Utils.RobotStateChangeProcessor;
using GCodeTranslator.Forms.MainWindow;
using GCodeTranslator.Forms.RobotConnectionWindow;
using GCodeTranslator.Forms.SettingsWindow;
using GCodeTranslator.Parsing.DTO;
using GCodeTranslator.Parsing.FileToObjectParsers;
using GCodeTranslator.Parsing.FileToObjectParsers.PowerMillParser;
using GCodeTranslator.Parsing.FileToObjectParsers.SimplifyParser;
using GCodeTranslator.Parsing.ObjectToRobotParser.DefaultImplementation;
using GCodeTranslator.Parsing.PostProcessors.AfterAllPostProcessor;
using GCodeTranslator.Parsing.PostProcessors.AngleFixPostProcessor;
using GCodeTranslator.Parsing.PostProcessors.SlicePostProcessor;
using GCodeTranslator.Parsing.TpConverter;
using GCodeTranslator.Service.DTO;
using GCodeTranslator.Utils.DebugUtils.DebugWindow;
using GCodeTranslator.Utils.LogUtils;

namespace GCodeTranslator.Service.MainWindow;

/// <summary>
/// Сервис для <see cref="MainWindowForm"/>. Инкапсулирует логику
/// </summary>
public class MainWindowFormService
{
    private readonly MainWindowForm _mainWindowForm;
    private SettingsHolder _settingsHolder; // Актуальные значения из окна "Настройки"
    private BrowsedFileProperties _browsedFileProperties; // Свойства выбранного файла для парсинга

    private readonly List<RequiredPropertiesForConnection> _tableRowsList = new(); // Лист из свойств для роботов в таблице "Avaiable Robots List"

    private readonly Logger _exceptionLogger = LoggerFactory.GetAppendableLogger("exception_log");
    private readonly Logger _logger = LoggerFactory.GetExistingOrCreateNewLogger("root_log");



    public MainWindowFormService(MainWindowForm mainWindowForm)
    {
        
        _mainWindowForm = mainWindowForm;
        _settingsHolder = InitializeActualSettings();
        ToTpConverter.StartTpConvertingServer();  // Запуск python-сервера конвертера в .tp

    }

    private SettingsHolder InitializeActualSettings()
    {
        
        var settingsWindowForm = new SettingsWindowForm();
        var settingsHolder = settingsWindowForm.GetActualSettingsHolder();
        settingsWindowForm.Dispose();
        
        return settingsHolder;
    }

    public void SetPropertiesFromSettingsHolder()
    {
        
        _mainWindowForm.RobotAddressTextBox.Text = _settingsHolder.DefaultRobotIp;
        LoggerFactory.Enabled = _settingsHolder.EnableLogs;
        
    }

    public void StartParse()
    {
        
        // 1. Получить все необходимые данные из формы для парсеров
        var formProperties = GrabFormPropertiesForParsers();

        // 2. Первый парсер. Парсит в List<GCodePoint>
        IToObjectParser toObjectParser;

        if (_mainWindowForm.PowerMillExportCheckBox.Checked)
        {
            toObjectParser = new PowerMillToObjectParser(formProperties);
        }
        else
        {
            toObjectParser = new SimplifyToObjectParser(formProperties);
        }
        
        // 3. Второй парсер. Запускает логику парсинга первого парсера, получает List<GCodePine>. Парсит его в .ls файл/файлы
        var toRobotParser = new ToRobotParser(toObjectParser);
        
        // 4. Python-слайсер. Запускает предыдущую логику. Бьет файл после toRobotParser'а на несколько .ls. Работает по галочке "AutoSplit Layers"
        var pythonSlicePostProcessor = new PythonSlicePostProcessor(toRobotParser);
        
        // 5. Python-фикс углов. Запускает предыдущую логику. Работает по галочке "Pankratov Angle-Technology"
        var pythonAngleFixPostProcessor = new PythonAngleFixPostProcessor(pythonSlicePostProcessor);

        // 5. Финальный пост-процессор. Запускает предыдущую логику. Открывает директорию с результатом
        var lastPostProcessor = new LastPostProcessor(pythonAngleFixPostProcessor);
        
        try
        {
            lastPostProcessor.PostProcess();
        }
        catch (Exception exception)
        {
            _exceptionLogger.LogException(exception);
            _logger.LogException(exception);
            MessageBox.Show(@"Произошла ошибка: " + exception.GetType() + "\n\n" +
                            @"Сообщение: " + exception.Message + "\n\n" +
                            exception.StackTrace + "\n\n" +
                            "Нажмите \"ОК\" и попробуйте еще раз\n\n" +
                            @"Вдруг повезет", @"Фатальная ошибка");
        }
        
    }

    public void SelectFile()
    {
        
        var openFileDialog = ResolveOpenFileDialog();
        if (openFileDialog.ShowDialog() != DialogResult.Cancel)
        {
            var filePath = openFileDialog.FileName;
            if (filePath != null)
            {
                ResolveBrowsedFileProperties(filePath);
                _mainWindowForm.GCodeFilenameTextBox.Text = filePath;
                
                return;
            }

            MessageBox.Show(caption: @"Фатальная ошибка", text: @"Не удалось выбрать файл");
        }
        
    }

    private OpenFileDialog ResolveOpenFileDialog()
    {
        
        var openFileDialog = new OpenFileDialog();
        if (_mainWindowForm.PowerMillExportCheckBox.Checked)
        {
            openFileDialog.Filter = "PowerMill (*.lsr ) |*.lsr; | Other files (*.*)|*.*";
        }
        else
        {
            openFileDialog.Filter = "GCode (*.gcode *.gc *.nc) |*.gcode; *.gc' *.nc| Other files (*.*)|*.*";
        }

        return openFileDialog;
    }

    private void ResolveBrowsedFileProperties(string filePath)
    {
        
        var fileNameWithoutExtension = filePath.Substring(filePath.LastIndexOf('\\'));
        fileNameWithoutExtension = fileNameWithoutExtension.Substring(1,
            fileNameWithoutExtension.LastIndexOf(".", StringComparison.Ordinal) - 1);
        var outputDirectory = filePath.Substring(0, filePath.LastIndexOf('\\') + 1) + fileNameWithoutExtension + @"\";

        _browsedFileProperties = new BrowsedFileProperties();
        _browsedFileProperties.FilePath = filePath; // Полный путь
        _browsedFileProperties.FileNameWithoutExtension = fileNameWithoutExtension; // Чисто название без расширения
        _browsedFileProperties.OutputDirectory = outputDirectory; // Директория для результата парсинга. (Выбранная директория + Папка с названием = названию файла
        
    }

    public void AddRobotToTable()
    {
        
        if (_mainWindowForm.RobotAddressTextBox.Text != "")
        {
            if (RobotAlreadyExists())
            {
                MessageBox.Show("Робот с таким IP Адресом уже существует");
                return;
            }
            AddRowToTable();
            var propertiesForConnection = GrabFormPropertiesForConnection();
            _tableRowsList.Add(propertiesForConnection);
        }
        
    }

    private bool RobotAlreadyExists()
    {
        var ipAddress = _mainWindowForm.RobotAddressTextBox.Text;
        foreach (var propertiesForConnection in _tableRowsList)
        {
            if (propertiesForConnection.IpAddress.Equals(ipAddress))
            {
                return true;
            }
        }

        return false;
    }

    private void AddRowToTable()
    {
        
        var id = _mainWindowForm.RobotsTable.Rows.Count + 1;
        var ipAddress = _mainWindowForm.RobotAddressTextBox.Text;
        var state = "Unknown state";
        _mainWindowForm.RobotsTable.Rows.Add(id, ipAddress, state);
        
    }


    private RequiredPropertiesForConnection GrabFormPropertiesForConnection()
    {
        
        var propertiesForConnection = new RequiredPropertiesForConnection();

        propertiesForConnection.IpAddress = _mainWindowForm.RobotAddressTextBox.Text; // Значение поля "Enter Robot Address"
        propertiesForConnection.SettingsHolder = _settingsHolder;

        propertiesForConnection.RobotStateProcessor = CreateRobotStateProcessor(); // Класс, меняющий значение ячейки "Current Robot State" в MainWindowForm и _stateLabel в RobotConnectionForm

        propertiesForConnection.RobotServerConnector = CreateRobotServerConnector(propertiesForConnection.IpAddress);

        return propertiesForConnection;
    }

    private RobotServerConnector CreateRobotServerConnector(string ipAddress)
    {
        
        var maxConnectionTime = ResolveMaxConnectionTime();
        
        return new RobotServerConnector(ipAddress, maxConnectionTime);
    }

    private RobotStateProcessor CreateRobotStateProcessor()
    {
        
        var robotStateProcessor = new RobotStateProcessor();

        var rowsCount = _mainWindowForm.RobotsTable.Rows.Count;
        var robotStateCell = _mainWindowForm.RobotsTable.Rows[rowsCount - 1].Cells[2];

        robotStateProcessor.SetMainFormProperties(_mainWindowForm, robotStateCell);
        
        return robotStateProcessor;
    }

    public void ShowRobotConnectionFormOrDeleteRow(DataGridViewCellEventArgs e)
    {
        
        if (e.ColumnIndex == -1 && e.RowIndex >= 0 && e.RowIndex <= _tableRowsList.Count) // Если крайний левый столбец
        {
            DeleteRobotRow(e.RowIndex);
            
            return;
        }

        if (e.RowIndex >= 0 && e.RowIndex <= _tableRowsList.Count) // Если любой другой, кроме шапки
        {
            var propertiesForConnection = _tableRowsList[e.RowIndex];

            if (IsConnectionToRobotSuccessful(ref propertiesForConnection))
            {
                OpenRobotConnectionForm(propertiesForConnection);
            }
        }
        
    }

    private void OpenRobotConnectionForm(RequiredPropertiesForConnection propertiesForConnection)
    {
        
        if (_debugWindowForm is { IsDisposed: false })
        {
            new RobotConnectionForm(propertiesForConnection).Show();
        }
        else
        {
            new RobotConnectionForm(propertiesForConnection).ShowDialog();
        }
        
    }

    private void DeleteRobotRow(int i)
    {
        
        _tableRowsList.RemoveAt(i);
        _mainWindowForm.RobotsTable.Rows.RemoveAt(i);
        var idCounter = 1;
        foreach (DataGridViewRow robotsTableRow in _mainWindowForm.RobotsTable.Rows) // Пересчет столбца "ID"
        {
            robotsTableRow.Cells[0].Value = idCounter;
            ++idCounter;
        }
        
    }

    private bool IsConnectionToRobotSuccessful(ref RequiredPropertiesForConnection requiredPropertiesForConnection) // Проверка соединения с роботом перед тем, как показать форму
    {
        
        var result = requiredPropertiesForConnection.RobotServerConnector.CheckConnection(true);
        switch (result.Key)
        {
            case "-1":
                requiredPropertiesForConnection.StateBeforeOpenForm = "Unknown state";
                requiredPropertiesForConnection.ColorOfStateBeforeOpenForm = Color.Black;
                return false;
            case "0":
                requiredPropertiesForConnection.StateBeforeOpenForm = "Ready to print";
                requiredPropertiesForConnection.ColorOfStateBeforeOpenForm = Color.Green;
                break;
            case "1":
                requiredPropertiesForConnection.StateBeforeOpenForm = "Printing file";
                requiredPropertiesForConnection.ColorOfStateBeforeOpenForm = Color.Green;
                break;
            case "2":
                requiredPropertiesForConnection.StateBeforeOpenForm = "File required / Printing file";
                requiredPropertiesForConnection.ColorOfStateBeforeOpenForm = Color.Green;
                break;
        }

        return true;
    }

    private int ResolveMaxConnectionTime()
    {
        
        int maxConnectionTime;
        try
        {
            maxConnectionTime = int.Parse(_settingsHolder.MaxConnectionTime);
        }
        catch (Exception)
        {
            MessageBox.Show("Не удалось получить время подключения из настроек.\n" +
                            "Установлено значение: 5 секунд");
            _settingsHolder.MaxConnectionTime = "5";
            maxConnectionTime = 5;
        }

        return maxConnectionTime;
    }

    public void ShowSettingsWindowForm()
    {
        
        var settingsWindowForm = new SettingsWindowForm();
        settingsWindowForm.ShowDialog(); // Показать форму
        _settingsHolder = settingsWindowForm.GetActualSettingsHolder(); // После закрытия формы получить из нее актуальные настройки
        SetPropertiesFromSettingsHolder(); // Изменить зависящие от них поля главной формы
        
    }

    private RequiredPropertiesForParsers GrabFormPropertiesForParsers()
    {
        _logger.LogWithTime("MainWindowFormService GrabFormPropertiesForParsers START");
        
        var properties = new RequiredPropertiesForParsers();
        properties.BrowsedFileProperties = _browsedFileProperties; // Параметры выбранного файла
        properties.SplitLayersTextBoxText = _mainWindowForm.SplitLayersTextBox.Text; // Значение поля "Split layers" 
        properties.AutoSplitLayersCheckBoxChecked = _mainWindowForm.AutoSplitLayersCheckBox.Checked; // Значение галочки "Autosplit layers"
        properties.NormalMovementTextBoxText = _mainWindowForm.NormalMovementTextBox.Text; // Значение поля "Normal movement"
        properties.WeldingMovementTextBoxText = _mainWindowForm.WeldingMovementTextBox.Text; // Значение поля "Welding movement" 
        properties.AutoArcByExtrusionCheckBoxChecked = _mainWindowForm.AutoArcEnabledByExtrusionCheckBox.Checked; // Значение галочки "Auto Arc Enabled By Extrusion"
        properties.RunWithoutArcCheckBoxChecked = _mainWindowForm.RunWithoutArcCheckBox.Checked; // Значение галочки "Run Without Arc" 
        properties.RoEnableCheckBoxChecked = _mainWindowForm.RoEnableCheckBox.Checked; // Значение галочки "RO enable"; 
        properties.WaveEnableCheckBoxChecked = _mainWindowForm.WaveEnableCheckBox.Checked; // Значение галочки "Wave enable";
        properties.WaveEnableTextBoxText = _mainWindowForm.WaveEnableTextBox.Text; // Значение поля "Wave enable"; 
        properties.WeldShieldTextBoxText = _mainWindowForm.WeldShieldTextBox.Text; // Значение поля "Weld Shield"
        properties.WeldShieldCheckBoxChecked = _mainWindowForm.WeldShieldCheckBox.Checked; // Значение галочки "Weld Shield"
        properties.UseWeldSpeedCheckBoxChecked = _mainWindowForm.UseWeldSpeedCheckBox.Checked; // Значение галочки "Use WELD_SPEED instead of feedrate" 
        properties.RobotUfTextBoxText = _mainWindowForm.RobotUfTextBox.Text; // Значение поля "UF" в Robot Frame
        properties.RobotUtTextBoxText = _mainWindowForm.RobotUtTextBox.Text; // Значение поля "UT" в Robot Frame
        properties.PositionerUfTextBoxTex = _mainWindowForm.PositionerUfTextBox.Text; // Значение поля "UF" в Positioner Frame  
        properties.PositionerUtTextBoxTex = _mainWindowForm.PositionerUtTextBox.Text; // Значение поля "UT" в Positioner Frame  
        properties.XTextBoxText = _mainWindowForm.XTextBox.Text; // Значение поля "X"
        properties.YTextBoxText = _mainWindowForm.YTextBox.Text; // Значение поля "Y"
        properties.ZTextBoxText = _mainWindowForm.ZTextBox.Text; // Значение поля "Z"
        properties.WTextBoxText = _mainWindowForm.WTextBox.Text; // Значение поля "W"
        properties.PTextBoxText = _mainWindowForm.PTextBox.Text; // Значение поля "P"
        properties.RTextBoxText = _mainWindowForm.RTextBox.Text; // Значение поля "R"
        properties.LaserPassCheckBoxChecked = _mainWindowForm.LaserPassCheckBox.Checked; // Значение галочки "Laser pass"
        properties.RemoveSmallStopStartCheckBoxChecked = _mainWindowForm.RemoveSmallStopStartCheckBox.Checked; // Значение галочки "Remove Small Stop Start"
        properties.CheckingDistanceTextBoxText = _mainWindowForm.CheckingDistanceTextBox.Text; // Значение поля "Checking distance (mm)"
        properties.ShortWristComboBoxText = _mainWindowForm.WristComboBox.Text.Equals("NO-FLIP") ? "N" : "F"; // Короткое значение комбо-бокса "Wrist" "F"/"N"
        properties.ShortArmComboBoxText = _mainWindowForm.ArmComboBox.Text.Equals("UP") ? "U" : "D"; // Короткое значение комбо-бокса "Arm" "U"/"D"
        properties.ShortBaseComboBoxText = _mainWindowForm.BaseComboBox.Text.Equals("FRONT") ? "T" : "B"; // Короткое значение комбо-бокса "Base" "T"/"B"
        properties.TurnsJ1TextBox = _mainWindowForm.TurnsJ1TextBox.Text; // Значение поля "Turns J1" 
        properties.TurnsJ4TextBox = _mainWindowForm.TurnsJ4TextBox.Text; // Значение поля "Turns J4" 
        properties.TurnsJ6TextBox = _mainWindowForm.TurnsJ6TextBox.Text; // Значение поля "Turns J6" 
        properties.J1OffsetTextBoxText = _mainWindowForm.J1OffsetTextBox.Text; // Значение поля "A(j1)  
        properties.J2OffsetTextBoxText = _mainWindowForm.J2OffsetTextBox.Text; // Значение поля "B(j2)  
        properties.AngleScriptCheckBoxChecked = _mainWindowForm.AngleScriptCheckBox.Checked;  // Значение галочки "Pankratov Angle-Technology™"
        properties.MaxAngleValueTextBoxText = _mainWindowForm.MaxAngleValueTextBox.Text;  // Значение поля "Максимальное значение угла"
        properties.CriticalAngleDifferenceTextBoxText = _mainWindowForm.CriticalAngleDifferenceTextBox.Text;  // Значение поля "Критическая разница углов"

        _logger.LogWithTime("MainWindowFormService GrabFormPropertiesForParsers END");
        _logger.Log(properties.ToString());
        return properties;
    }


    /*
     * Для дебага
     */


    private DateTime _prevDoubleDebugClick = DateTime.Now;
    private int _clickCounter;
    private DebugWindowForm? _debugWindowForm;

    public void ResolveDebugModEnable(DataGridViewCellEventArgs e)
    {
        
        if (_debugWindowForm is { IsDisposed: false }) return;
        if (e.RowIndex != -1 || e.ColumnIndex != -1) return;
        var nowTime = DateTime.Now;
        var difference = Math.Round((nowTime - _prevDoubleDebugClick).TotalMilliseconds);
        if (difference < 400)
        {
            ++_clickCounter;
        }
        else
        {
            _clickCounter = 0;
        }

        _prevDoubleDebugClick = nowTime;
        if (_clickCounter == 1)
        {
            _debugWindowForm = new DebugWindowForm();
            _debugWindowForm.Show();
            _clickCounter = 0;
        }
        
    }

    public void CloseDebugWindow()
    {
        
        if (_debugWindowForm is { IsDisposed: false })
        {
            _debugWindowForm.Dispose();
        }
        
        ToTpConverter.EliminateTpConvertingProcess();
        
    }

    public void SelectLsFolder()
    {
        var selectedDirectory = ShowFolderBrowserDialog();
        if (selectedDirectory == null) return;
        _mainWindowForm.SelectedLsFolderTextBox.Text = selectedDirectory;
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

    public void ConvertFolderToTp()
    {
        if (!IsLsFolderSelected())
        {
            MessageBox.Show("Сначала выберите папку");
            return;
        }
        new ToTpConverter().ConvertAllFiles(_mainWindowForm.SelectedLsFolderTextBox.Text);
        
    }
    
    private bool IsLsFolderSelected()
    {
        return !(_mainWindowForm.SelectedLsFolderTextBox.Text == null || _mainWindowForm.SelectedLsFolderTextBox.Text.Equals(""));
    }

    public void ConvertFileToTp()
    {
        var tpOpenFileDialog = CreateLsOpenFileDialog();
        var dialogResult = tpOpenFileDialog.ShowDialog();
        if (dialogResult == DialogResult.OK)
        {
            var filePath = tpOpenFileDialog.FileName;
            new ToTpConverter().ConvertOneFile(filePath);
        }
    }

    private OpenFileDialog CreateLsOpenFileDialog()
    {
        
        OpenFileDialog fileDialog = new OpenFileDialog();
        fileDialog.Filter = "ls files (*.ls)|*.ls|All files (*.*)|*.*";
        fileDialog.FilterIndex = 2;
        fileDialog.RestoreDirectory = true;
        
        return fileDialog;
    }
    
    public void SelectLsFolderForAngleFix()
    {
        var selectedDirectory = ShowFolderBrowserDialog();
        if (selectedDirectory == null) return;
        _mainWindowForm.SepLsFolderTextBox.Text = selectedDirectory;
    }

    public void FixAngles()
    {
        if (IsLsFolderForAnglesSelected())
        {
            var fileDirectory = _mainWindowForm.SepLsFolderTextBox.Text;
            var maxAngleValue = _mainWindowForm.SepMaxValueTextBox.Text;
            var criticalDifferenceValue = _mainWindowForm.SepCriticalAngleDifferenceTextBox.Text;
            new ProcessRunner().RunPythonAngleFixProcess(fileDirectory, maxAngleValue, criticalDifferenceValue, true);
        }
    }
    
    private bool IsLsFolderForAnglesSelected()
    {
        return !(_mainWindowForm.SepLsFolderTextBox.Text == null || _mainWindowForm.SepLsFolderTextBox.Text.Equals(""));
    }
}