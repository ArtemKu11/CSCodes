using GCodeTranslator.Service.MainWindow;
using GCodeTranslator.Utils.LogUtils;

namespace GCodeTranslator.Forms.MainWindow
{
    /// <summary>
    /// <para>
    /// �������� ���� ����������.
    /// </para>
    /// ������ ��������� ������, ���-������ � �.�.
    /// <para>
    /// ������ ������ ������ ����� �����
    /// </para>
    /// <para>
    /// ���������� �������� ���������� � ������ �� �� ��������� ���������� � MainWindowForm.Designer.cs
    /// </para>
    /// </summary>
    public partial class MainWindowForm : Form
    {
        private readonly MainWindowFormService _mainWindowFormService;
        private readonly Logger _logger = LoggerFactory.GetExistingOrCreateNewLogger("root_log"); 

        public MainWindowForm()
        {
            _logger.LogWithTime("MainWindowForm CONSTR START");
            
            InitializeComponent(); // �������� ����������. ������������� �������������
            CenterToScreen();
            _mainWindowFormService = new MainWindowFormService(this);
            
            _logger.LogWithTime("MainWindowForm CONSTR END");
        }
        

        // ����� �������� �����
        private void MainWindowForm_Load(object sender, EventArgs e)
        {
            _logger.LogWithTime("MainWindowForm MainWindowForm_Load START");

            _mainWindowFormService.SetPropertiesFromSettingsHolder(); // ���������� �������� ����� �� ��������
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
         * ��������� ������� Convert
         */


        // ����� ������� "PowerMill Export"
        private void PowerMillExportCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            gCodeFilenameLabel.Text = PowerMillExportCheckBox.Checked ? @"PowerMill file name:" : @"GCode filename:";
        }

        // ���� �� ������ "START" �� ������� "Convert". ��������� ������ ��������
        private void StartButton_Click(object sender, EventArgs e)
        {
            _logger.LogWithTime("MainWindowForm StartButton_Click START");
            
            startButton.Enabled = false;
            _mainWindowFormService.StartParse();
            startButton.Enabled = true;
            
            _logger.LogWithTime("MainWindowForm StartButton_Click END");
        }
        
        // ����� ������� "Remove small stop-start"
        private void RemoveSmallStopStartCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            CheckingDistanceTextBox.Enabled = RemoveSmallStopStartCheckBox.Checked;
        }

        // ���� �� ������ "Open"
        private void GCodeFilenameButton_Click(object sender, EventArgs e)
        {
            _mainWindowFormService.SelectFile();
        }

        // ����� ������� "Weld Shield"
        private void WeldShieldCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            WeldShieldTextBox.Enabled = WeldShieldCheckBox.Checked;
        }

        // ����� ������� "Wave enable"
        private void WaveEnableCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            WaveEnableTextBox.Enabled = WaveEnableCheckBox.Checked;
        }

        // ����� ������� "Autosplit layers"
        private void AutoSplitLayersCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            SplitLayersTextBox.Enabled = !AutoSplitLayersCheckBox.Checked;
            LaserPassCheckBox.Enabled = AutoSplitLayersCheckBox.Checked;
        }
        
        // ����� ������� "Pankratov Angle-Technology"
        private void AngleScriptCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            MaxAngleValueTextBox.Enabled = AngleScriptCheckBox.Checked;
            CriticalAngleDifferenceTextBox.Enabled = AngleScriptCheckBox.Checked;
        }



        /*
         * ��������� ������� "Send to robot"
         */


        // ���� �� ������ "Add"
        private void AddButton_Click(object sender, EventArgs e)
        {
            _logger.LogWithTime("MainWindowForm AddButton_Click START");
            
            _mainWindowFormService.AddRobotToTable();
            
            _logger.LogWithTime("MainWindowForm AddButton_Click END");
        }

        // ���� �� ������ ������� � ��������
        private void RobotsTable_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            _logger.LogWithTime("MainWindowForm RobotsTable_CellClick START, Column" + e.ColumnIndex + ", Row" + e.RowIndex);

            _mainWindowFormService.ShowRobotConnectionFormOrDeleteRow(e);
            
            _logger.LogWithTime("MainWindowForm RobotsTable_CellClick END");
        }
        
        // ������� ���� �� ������ (��� ��������� ����� ����)
        private void RobotsTable_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            _mainWindowFormService.ResolveDebugModEnable(e);
        }
        
        
        
        /*
        * ��������� ������� "Convert to .tp"
        */
        
        
        
        // �������� "Open" ��� ������ ����� � .ls
        private void OpenLsFolderButton_Click(object sender, EventArgs e)
        {
            _mainWindowFormService.SelectLsFolder();
        }
        
        // ��������� "�������������� �����"
        private void ConvertFolderButton_Click(object sender, EventArgs e)
        {
            _mainWindowFormService.ConvertFolderToTp();
        }

        // ��������� "�������������� ����"
        private void ConvertFileButton_Click(object sender, EventArgs e)
        {
            _mainWindowFormService.ConvertFileToTp();
        }



        /*
         * ��������� ������ ������� "Angle Fix"
         */
        
        
        
        // �������� "Open" ��� ������ ����� � .ls
        private void SepLsFolderOpenButton_Click(object sender, EventArgs e)
        {
            _mainWindowFormService.SelectLsFolderForAngleFix();
        }
        
        // �������� "��������� ����"
        private void SepFixAngleButton_Click(object sender, EventArgs e)
        {
            _mainWindowFormService.FixAngles();
        }
        


        /*
         * ��������� ������ � "���������"
         */



        // ���� �� ������ "���������"
        private void SettingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _mainWindowFormService.ShowSettingsWindowForm();
        }
    }
}