using GCodeTranslator.Service.MainWindow;
using GCodeTranslator.Utils.LogUtils;

namespace GCodeTranslator.Forms.MainWindow
{
    /// <summary>
    /// <para>
    /// Основное окно приложения.
    /// </para>
    /// Хранит листенеры кнопок, чек-боксов и т.д.
    /// <para>
    /// Искать начало логики можно здесь
    /// </para>
    /// <para>
    /// Конкретные элементы интерфейса и ссылки на их листенеры определены в MainWindowForm.Designer.cs
    /// </para>
    /// </summary>
    public partial class MainWindowForm : Form
    {
        private readonly MainWindowFormService _mainWindowFormService;
        private readonly Logger _logger = LoggerFactory.GetExistingOrCreateNewLogger("root_log"); 

        public MainWindowForm()
        {
            _logger.LogWithTime("MainWindowForm CONSTR START");
            
            InitializeComponent(); // Создание интерфейса. Сгененировано автоматически
            CenterToScreen();
            _mainWindowFormService = new MainWindowFormService(this);
            
            _logger.LogWithTime("MainWindowForm CONSTR END");
        }
        

        // Ивент загрузки формы
        private void MainWindowForm_Load(object sender, EventArgs e)
        {
            _logger.LogWithTime("MainWindowForm MainWindowForm_Load START");

            _mainWindowFormService.SetPropertiesFromSettingsHolder(); // Установить значения полей из настроек
            wristComboBox.SelectedIndex = 0;
            armComboBox.SelectedIndex = 1;
            baseComboBox.SelectedIndex = 0;
            _logger.LogWithTime("MainWindowForm MainWindowForm_Load END");
        }
        
        private void MainWindowForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            _mainWindowFormService.CloseDebugWindow();
        }



        /*
         * Листенеры вкладки Convert
         */


        // Ивент галочки "PowerMill Export"
        private void PowerMillExportCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            gCodeFilenameLabel.Text = PowerMillExportCheckBox.Checked ? @"PowerMill file name:" : @"GCode filename:";
        }

        // Клик по кнопке "START" на вкладке "Convert". Запускает логику парсинга
        private void StartButton_Click(object sender, EventArgs e)
        {
            _logger.LogWithTime("MainWindowForm StartButton_Click START");
            
            startButton.Enabled = false;
            _mainWindowFormService.StartParse();
            startButton.Enabled = true;
            
            _logger.LogWithTime("MainWindowForm StartButton_Click END");
        }
        
        // Ивент галочки "Remove small stop-start"
        private void RemoveSmallStopStartCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            CheckingDistanceTextBox.Enabled = RemoveSmallStopStartCheckBox.Checked;
        }

        // Клик по кнопке "Open"
        private void GCodeFilenameButton_Click(object sender, EventArgs e)
        {
            _mainWindowFormService.SelectFile();
        }

        // Ивент галочки "Weld Shield"
        private void WeldShieldCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            WeldShieldTextBox.Enabled = WeldShieldCheckBox.Checked;
        }

        // Ивент галочки "Wave enable"
        private void WaveEnableCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            WaveEnableTextBox.Enabled = WaveEnableCheckBox.Checked;
        }

        // Ивент галочки "Autosplit layers"
        private void AutoSplitLayersCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            SplitLayersTextBox.Enabled = !AutoSplitLayersCheckBox.Checked;
            LaserPassCheckBox.Enabled = AutoSplitLayersCheckBox.Checked;
        }
        
        // Ивент галочки "Pankratov Angle-Technology"
        private void AngleScriptCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            MaxAngleValueTextBox.Enabled = AngleScriptCheckBox.Checked;
            CriticalAngleDifferenceTextBox.Enabled = AngleScriptCheckBox.Checked;
        }



        /*
         * Листенеры вкладки "Send to robot"
         */


        // Клик по кнопке "Add"
        private void AddButton_Click(object sender, EventArgs e)
        {
            _logger.LogWithTime("MainWindowForm AddButton_Click START");
            
            _mainWindowFormService.AddRobotToTable();
            
            _logger.LogWithTime("MainWindowForm AddButton_Click END");
        }

        // Клик по ячейке таблицы с роботами
        private void RobotsTable_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            _logger.LogWithTime("MainWindowForm RobotsTable_CellClick START, Column" + e.ColumnIndex + ", Row" + e.RowIndex);

            _mainWindowFormService.ShowRobotConnectionFormOrDeleteRow(e);
            
            _logger.LogWithTime("MainWindowForm RobotsTable_CellClick END");
        }
        
        // Двойной клик по ячейке (Для включения дебаг мода)
        private void RobotsTable_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            _mainWindowFormService.ResolveDebugModEnable(e);
        }
        
        
        
        /*
        * Листенеры вкладки "Convert to .tp"
        */
        
        
        
        // Кнопочка "Open" для выбора папки с .ls
        private void OpenLsFolderButton_Click(object sender, EventArgs e)
        {
            _mainWindowFormService.SelectLsFolder();
        }
        
        // Конопочка "Конвертировать папку"
        private void ConvertFolderButton_Click(object sender, EventArgs e)
        {
            _mainWindowFormService.ConvertFolderToTp();
        }

        // Конопочка "Конвертировать файл"
        private void ConvertFileButton_Click(object sender, EventArgs e)
        {
            _mainWindowFormService.ConvertFileToTp();
        }



        /*
         * Листенеры кнопок вкладки "Angle Fix"
         */
        
        
        
        // Кнопочка "Open" для выбора папки с .ls
        private void SepLsFolderOpenButton_Click(object sender, EventArgs e)
        {
            _mainWindowFormService.SelectLsFolderForAngleFix();
        }
        
        // Кнопочка "Пофиксить углы"
        private void SepFixAngleButton_Click(object sender, EventArgs e)
        {
            _mainWindowFormService.FixAngles();
        }
        


        /*
         * Листенеры кнопок в "Параметры"
         */



        // Клик по кнопке "Настройки"
        private void SettingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _mainWindowFormService.ShowSettingsWindowForm();
        }
    }
}