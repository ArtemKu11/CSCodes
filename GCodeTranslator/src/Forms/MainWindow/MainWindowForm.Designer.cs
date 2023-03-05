namespace GCodeTranslator.Forms.MainWindow
{
    
    /// <summary>
    /// Сгенерировано автоматически с помощью Windows Forms Designer в Visual Studio
    /// </summary>
    partial class MainWindowForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainWindowForm));
            this.mainTabControl = new System.Windows.Forms.TabControl();
            this.convertTabPage = new System.Windows.Forms.TabPage();
            this.convertMainPanel = new System.Windows.Forms.Panel();
            this.angleFixGroupBox = new System.Windows.Forms.GroupBox();
            this.criticalAngleDifferenceTextBox = new System.Windows.Forms.TextBox();
            this.criticalAngleDifferenceLabel = new System.Windows.Forms.Label();
            this.maxAngleValueTextBox = new System.Windows.Forms.TextBox();
            this.maxAngleValueLabel = new System.Windows.Forms.Label();
            this.positionerFrameGroupBox = new System.Windows.Forms.GroupBox();
            this.positionerUtTextBox = new System.Windows.Forms.TextBox();
            this.positionerUtLabel = new System.Windows.Forms.Label();
            this.positionerUfTextBox = new System.Windows.Forms.TextBox();
            this.positionerUfLabel = new System.Windows.Forms.Label();
            this.startButton = new System.Windows.Forms.Button();
            this.layersSplitGroupBox = new System.Windows.Forms.GroupBox();
            this.laserPassCheckBox = new System.Windows.Forms.CheckBox();
            this.autoSplitLayersCheckBox = new System.Windows.Forms.CheckBox();
            this.splitLayersTextBox = new System.Windows.Forms.TextBox();
            this.splitLayersLabel = new System.Windows.Forms.Label();
            this.someParametersGroupBox = new System.Windows.Forms.GroupBox();
            this.waveEnableTextBox = new System.Windows.Forms.TextBox();
            this.weldShieldTextBox = new System.Windows.Forms.TextBox();
            this.roEnableCheckBox = new System.Windows.Forms.CheckBox();
            this.waveEnableCheckBox = new System.Windows.Forms.CheckBox();
            this.weldShieldCheckBox = new System.Windows.Forms.CheckBox();
            this.removeSmallStopStartCheckBox = new System.Windows.Forms.CheckBox();
            this.checkingDistanceTextBox = new System.Windows.Forms.TextBox();
            this.useWeldSpeedCheckBox = new System.Windows.Forms.CheckBox();
            this.checkingDistanceLabel = new System.Windows.Forms.Label();
            this.autoArcEnabledByExtrusionCheckBox = new System.Windows.Forms.CheckBox();
            this.runWithoutArcCheckBox = new System.Windows.Forms.CheckBox();
            this.weldingMovementTextBox = new System.Windows.Forms.TextBox();
            this.weldingMovementLabel = new System.Windows.Forms.Label();
            this.normalMovementTextBox = new System.Windows.Forms.TextBox();
            this.normalMovementLabel = new System.Windows.Forms.Label();
            this.xzChtoZaPropertiesToolBox = new System.Windows.Forms.GroupBox();
            this.turnsJ6TextBox = new System.Windows.Forms.TextBox();
            this.turnsJ4TextBox = new System.Windows.Forms.TextBox();
            this.turnsJ1TextBox = new System.Windows.Forms.TextBox();
            this.turnsJ6Label = new System.Windows.Forms.Label();
            this.turnsJ4Label = new System.Windows.Forms.Label();
            this.turnsJ1Label = new System.Windows.Forms.Label();
            this.baseComboBox = new System.Windows.Forms.ComboBox();
            this.baseLabel = new System.Windows.Forms.Label();
            this.armComboBox = new System.Windows.Forms.ComboBox();
            this.armLabel = new System.Windows.Forms.Label();
            this.wristComboBox = new System.Windows.Forms.ComboBox();
            this.wristLabel = new System.Windows.Forms.Label();
            this.robotFrameGroupBox = new System.Windows.Forms.GroupBox();
            this.robotUtTextBox = new System.Windows.Forms.TextBox();
            this.robotUtLabel = new System.Windows.Forms.Label();
            this.robotUfTextBox = new System.Windows.Forms.TextBox();
            this.robotUfLabel = new System.Windows.Forms.Label();
            this.codeOffsetGroupBox = new System.Windows.Forms.GroupBox();
            this.j2OffsetTextBox = new System.Windows.Forms.TextBox();
            this.j2OffsetLabel = new System.Windows.Forms.Label();
            this.j1OffsetTextBox = new System.Windows.Forms.TextBox();
            this.j1OffsetLabel = new System.Windows.Forms.Label();
            this.zTextBox = new System.Windows.Forms.TextBox();
            this.zLabel = new System.Windows.Forms.Label();
            this.yTextBox = new System.Windows.Forms.TextBox();
            this.yLabel = new System.Windows.Forms.Label();
            this.xTextBox = new System.Windows.Forms.TextBox();
            this.xLabel = new System.Windows.Forms.Label();
            this.rTextBox = new System.Windows.Forms.TextBox();
            this.rLabel = new System.Windows.Forms.Label();
            this.pTextBox = new System.Windows.Forms.TextBox();
            this.pLabel = new System.Windows.Forms.Label();
            this.wTextBox = new System.Windows.Forms.TextBox();
            this.wLabel = new System.Windows.Forms.Label();
            this.angleScriptCheckBox = new System.Windows.Forms.CheckBox();
            this.powerMillExportCheckBox = new System.Windows.Forms.CheckBox();
            this.fileSettingsGroupBox = new System.Windows.Forms.GroupBox();
            this.gCodeFilenamePanel = new System.Windows.Forms.Panel();
            this.gCodeFilenameLabel = new System.Windows.Forms.Label();
            this.gCodeFilenameButton = new System.Windows.Forms.Button();
            this.gCodeFilenameTextBox = new System.Windows.Forms.TextBox();
            this.sendToRobotTabPage = new System.Windows.Forms.TabPage();
            this.panel1 = new System.Windows.Forms.Panel();
            this.sendToBoxMainPanel = new System.Windows.Forms.Panel();
            this.robotParamsGroupBox = new System.Windows.Forms.GroupBox();
            this.sendTpFileCheckBox = new System.Windows.Forms.CheckBox();
            this.addButton = new System.Windows.Forms.Button();
            this.robotAddressTextBox = new System.Windows.Forms.TextBox();
            this.enterRobotAddressLabel = new System.Windows.Forms.Label();
            this.avaiableRobotListComboBox = new System.Windows.Forms.ComboBox();
            this.avaibaleRobotListLabel = new System.Windows.Forms.Label();
            this.robotsTable = new System.Windows.Forms.DataGridView();
            this.IDColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ipAddressColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.currentRobotStateColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.convertToTpTabPage = new System.Windows.Forms.TabPage();
            this.convertToTpMainPanel = new System.Windows.Forms.Panel();
            this.convertFileGroupBox = new System.Windows.Forms.GroupBox();
            this.helpPressConvertFileBtnLabel = new System.Windows.Forms.Label();
            this.convertFileButton = new System.Windows.Forms.Button();
            this.convertFolderGroupBox = new System.Windows.Forms.GroupBox();
            this.lsFolderPanel = new System.Windows.Forms.Panel();
            this.lsFolderLabel = new System.Windows.Forms.Label();
            this.openLsFolderButton = new System.Windows.Forms.Button();
            this.selectedLsFolderTextBox = new System.Windows.Forms.TextBox();
            this.helpPressConvertFolderBtnLabel = new System.Windows.Forms.Label();
            this.helpSelectFolderLabel = new System.Windows.Forms.Label();
            this.convertFolderButton = new System.Windows.Forms.Button();
            this.angleFixTabPage = new System.Windows.Forms.TabPage();
            this.separatedAngleFixPanel = new System.Windows.Forms.Panel();
            this.separateAngleFixGroupBox = new System.Windows.Forms.GroupBox();
            this.sepCriticalAngleDifferenceTextBox = new System.Windows.Forms.TextBox();
            this.sepCriticalAngleDifferenceLabel = new System.Windows.Forms.Label();
            this.sepMaxValueTextBox = new System.Windows.Forms.TextBox();
            this.sepMaxValueLabel = new System.Windows.Forms.Label();
            this.sepFixAngleButton = new System.Windows.Forms.Button();
            this.sepLsFolderPanel = new System.Windows.Forms.Panel();
            this.sepLsFolderLabel = new System.Windows.Forms.Label();
            this.sepLsFolderOpenButton = new System.Windows.Forms.Button();
            this.sepLsFolderTextBox = new System.Windows.Forms.TextBox();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.parametersToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.checkUpdatesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mainTabControl.SuspendLayout();
            this.convertTabPage.SuspendLayout();
            this.convertMainPanel.SuspendLayout();
            this.angleFixGroupBox.SuspendLayout();
            this.positionerFrameGroupBox.SuspendLayout();
            this.layersSplitGroupBox.SuspendLayout();
            this.someParametersGroupBox.SuspendLayout();
            this.xzChtoZaPropertiesToolBox.SuspendLayout();
            this.robotFrameGroupBox.SuspendLayout();
            this.codeOffsetGroupBox.SuspendLayout();
            this.fileSettingsGroupBox.SuspendLayout();
            this.gCodeFilenamePanel.SuspendLayout();
            this.sendToRobotTabPage.SuspendLayout();
            this.panel1.SuspendLayout();
            this.sendToBoxMainPanel.SuspendLayout();
            this.robotParamsGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.robotsTable)).BeginInit();
            this.convertToTpTabPage.SuspendLayout();
            this.convertToTpMainPanel.SuspendLayout();
            this.convertFileGroupBox.SuspendLayout();
            this.convertFolderGroupBox.SuspendLayout();
            this.lsFolderPanel.SuspendLayout();
            this.angleFixTabPage.SuspendLayout();
            this.separatedAngleFixPanel.SuspendLayout();
            this.separateAngleFixGroupBox.SuspendLayout();
            this.sepLsFolderPanel.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainTabControl
            // 
            this.mainTabControl.Controls.Add(this.convertTabPage);
            this.mainTabControl.Controls.Add(this.sendToRobotTabPage);
            this.mainTabControl.Controls.Add(this.convertToTpTabPage);
            this.mainTabControl.Controls.Add(this.angleFixTabPage);
            this.mainTabControl.Location = new System.Drawing.Point(15, 30);
            this.mainTabControl.Name = "mainTabControl";
            this.mainTabControl.SelectedIndex = 0;
            this.mainTabControl.Size = new System.Drawing.Size(1097, 585);
            this.mainTabControl.TabIndex = 0;
            // 
            // convertTabPage
            // 
            this.convertTabPage.Controls.Add(this.convertMainPanel);
            this.convertTabPage.Location = new System.Drawing.Point(4, 24);
            this.convertTabPage.Name = "convertTabPage";
            this.convertTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.convertTabPage.Size = new System.Drawing.Size(1089, 557);
            this.convertTabPage.TabIndex = 0;
            this.convertTabPage.Text = "Convert";
            this.convertTabPage.UseVisualStyleBackColor = true;
            // 
            // convertMainPanel
            // 
            this.convertMainPanel.Controls.Add(this.angleFixGroupBox);
            this.convertMainPanel.Controls.Add(this.positionerFrameGroupBox);
            this.convertMainPanel.Controls.Add(this.startButton);
            this.convertMainPanel.Controls.Add(this.layersSplitGroupBox);
            this.convertMainPanel.Controls.Add(this.someParametersGroupBox);
            this.convertMainPanel.Controls.Add(this.weldingMovementTextBox);
            this.convertMainPanel.Controls.Add(this.weldingMovementLabel);
            this.convertMainPanel.Controls.Add(this.normalMovementTextBox);
            this.convertMainPanel.Controls.Add(this.normalMovementLabel);
            this.convertMainPanel.Controls.Add(this.xzChtoZaPropertiesToolBox);
            this.convertMainPanel.Controls.Add(this.robotFrameGroupBox);
            this.convertMainPanel.Controls.Add(this.codeOffsetGroupBox);
            this.convertMainPanel.Controls.Add(this.angleScriptCheckBox);
            this.convertMainPanel.Controls.Add(this.powerMillExportCheckBox);
            this.convertMainPanel.Controls.Add(this.fileSettingsGroupBox);
            this.convertMainPanel.Location = new System.Drawing.Point(0, 0);
            this.convertMainPanel.Name = "convertMainPanel";
            this.convertMainPanel.Size = new System.Drawing.Size(1089, 557);
            this.convertMainPanel.TabIndex = 0;
            // 
            // angleFixGroupBox
            // 
            this.angleFixGroupBox.Controls.Add(this.criticalAngleDifferenceTextBox);
            this.angleFixGroupBox.Controls.Add(this.criticalAngleDifferenceLabel);
            this.angleFixGroupBox.Controls.Add(this.maxAngleValueTextBox);
            this.angleFixGroupBox.Controls.Add(this.maxAngleValueLabel);
            this.angleFixGroupBox.Location = new System.Drawing.Point(632, 24);
            this.angleFixGroupBox.Name = "angleFixGroupBox";
            this.angleFixGroupBox.Size = new System.Drawing.Size(427, 125);
            this.angleFixGroupBox.TabIndex = 29;
            this.angleFixGroupBox.TabStop = false;
            this.angleFixGroupBox.Text = "Angle Fix";
            // 
            // criticalAngleDifferenceTextBox
            // 
            this.criticalAngleDifferenceTextBox.Enabled = false;
            this.criticalAngleDifferenceTextBox.Location = new System.Drawing.Point(201, 57);
            this.criticalAngleDifferenceTextBox.Name = "criticalAngleDifferenceTextBox";
            this.criticalAngleDifferenceTextBox.Size = new System.Drawing.Size(60, 23);
            this.criticalAngleDifferenceTextBox.TabIndex = 3;
            this.criticalAngleDifferenceTextBox.Text = "1";
            // 
            // criticalAngleDifferenceLabel
            // 
            this.criticalAngleDifferenceLabel.AutoSize = true;
            this.criticalAngleDifferenceLabel.Location = new System.Drawing.Point(20, 59);
            this.criticalAngleDifferenceLabel.Name = "criticalAngleDifferenceLabel";
            this.criticalAngleDifferenceLabel.Size = new System.Drawing.Size(162, 15);
            this.criticalAngleDifferenceLabel.TabIndex = 2;
            this.criticalAngleDifferenceLabel.Text = "Критическая разница углов:";
            // 
            // maxAngleValueTextBox
            // 
            this.maxAngleValueTextBox.Enabled = false;
            this.maxAngleValueTextBox.Location = new System.Drawing.Point(201, 27);
            this.maxAngleValueTextBox.Name = "maxAngleValueTextBox";
            this.maxAngleValueTextBox.Size = new System.Drawing.Size(60, 23);
            this.maxAngleValueTextBox.TabIndex = 1;
            this.maxAngleValueTextBox.Text = "20";
            // 
            // maxAngleValueLabel
            // 
            this.maxAngleValueLabel.AutoSize = true;
            this.maxAngleValueLabel.Location = new System.Drawing.Point(20, 30);
            this.maxAngleValueLabel.Name = "maxAngleValueLabel";
            this.maxAngleValueLabel.Size = new System.Drawing.Size(175, 15);
            this.maxAngleValueLabel.TabIndex = 0;
            this.maxAngleValueLabel.Text = "Максимальное значение угла:";
            // 
            // positionerFrameGroupBox
            // 
            this.positionerFrameGroupBox.Controls.Add(this.positionerUtTextBox);
            this.positionerFrameGroupBox.Controls.Add(this.positionerUtLabel);
            this.positionerFrameGroupBox.Controls.Add(this.positionerUfTextBox);
            this.positionerFrameGroupBox.Controls.Add(this.positionerUfLabel);
            this.positionerFrameGroupBox.Location = new System.Drawing.Point(632, 164);
            this.positionerFrameGroupBox.Name = "positionerFrameGroupBox";
            this.positionerFrameGroupBox.Size = new System.Drawing.Size(130, 125);
            this.positionerFrameGroupBox.TabIndex = 29;
            this.positionerFrameGroupBox.TabStop = false;
            this.positionerFrameGroupBox.Text = "Positioner Frame";
            // 
            // positionerUtTextBox
            // 
            this.positionerUtTextBox.Location = new System.Drawing.Point(45, 56);
            this.positionerUtTextBox.Name = "positionerUtTextBox";
            this.positionerUtTextBox.Size = new System.Drawing.Size(60, 23);
            this.positionerUtTextBox.TabIndex = 3;
            this.positionerUtTextBox.Text = "5";
            // 
            // positionerUtLabel
            // 
            this.positionerUtLabel.AutoSize = true;
            this.positionerUtLabel.Location = new System.Drawing.Point(20, 59);
            this.positionerUtLabel.Name = "positionerUtLabel";
            this.positionerUtLabel.Size = new System.Drawing.Size(24, 15);
            this.positionerUtLabel.TabIndex = 2;
            this.positionerUtLabel.Text = "UT:";
            // 
            // positionerUfTextBox
            // 
            this.positionerUfTextBox.Location = new System.Drawing.Point(45, 27);
            this.positionerUfTextBox.Name = "positionerUfTextBox";
            this.positionerUfTextBox.Size = new System.Drawing.Size(60, 23);
            this.positionerUfTextBox.TabIndex = 1;
            this.positionerUfTextBox.Text = "6";
            // 
            // positionerUfLabel
            // 
            this.positionerUfLabel.AutoSize = true;
            this.positionerUfLabel.Location = new System.Drawing.Point(20, 30);
            this.positionerUfLabel.Name = "positionerUfLabel";
            this.positionerUfLabel.Size = new System.Drawing.Size(24, 15);
            this.positionerUfLabel.TabIndex = 0;
            this.positionerUfLabel.Text = "UF:";
            // 
            // startButton
            // 
            this.startButton.Location = new System.Drawing.Point(537, 509);
            this.startButton.Name = "startButton";
            this.startButton.Size = new System.Drawing.Size(75, 23);
            this.startButton.TabIndex = 36;
            this.startButton.Text = "START";
            this.startButton.UseVisualStyleBackColor = true;
            this.startButton.Click += new System.EventHandler(this.StartButton_Click);
            // 
            // layersSplitGroupBox
            // 
            this.layersSplitGroupBox.Controls.Add(this.laserPassCheckBox);
            this.layersSplitGroupBox.Controls.Add(this.autoSplitLayersCheckBox);
            this.layersSplitGroupBox.Controls.Add(this.splitLayersTextBox);
            this.layersSplitGroupBox.Controls.Add(this.splitLayersLabel);
            this.layersSplitGroupBox.Location = new System.Drawing.Point(707, 359);
            this.layersSplitGroupBox.Name = "layersSplitGroupBox";
            this.layersSplitGroupBox.Size = new System.Drawing.Size(352, 125);
            this.layersSplitGroupBox.TabIndex = 29;
            this.layersSplitGroupBox.TabStop = false;
            this.layersSplitGroupBox.Text = "Layers Split";
            // 
            // laserPassCheckBox
            // 
            this.laserPassCheckBox.AutoSize = true;
            this.laserPassCheckBox.Enabled = false;
            this.laserPassCheckBox.Location = new System.Drawing.Point(20, 88);
            this.laserPassCheckBox.Name = "laserPassCheckBox";
            this.laserPassCheckBox.Size = new System.Drawing.Size(79, 19);
            this.laserPassCheckBox.TabIndex = 31;
            this.laserPassCheckBox.Text = "Laser pass";
            this.laserPassCheckBox.UseVisualStyleBackColor = true;
            // 
            // autoSplitLayersCheckBox
            // 
            this.autoSplitLayersCheckBox.AutoSize = true;
            this.autoSplitLayersCheckBox.Location = new System.Drawing.Point(20, 61);
            this.autoSplitLayersCheckBox.Name = "autoSplitLayersCheckBox";
            this.autoSplitLayersCheckBox.Size = new System.Drawing.Size(107, 19);
            this.autoSplitLayersCheckBox.TabIndex = 30;
            this.autoSplitLayersCheckBox.Text = "Autosplit layers";
            this.autoSplitLayersCheckBox.UseVisualStyleBackColor = true;
            this.autoSplitLayersCheckBox.CheckedChanged += new System.EventHandler(this.AutoSplitLayersCheckBox_CheckedChanged);
            // 
            // splitLayersTextBox
            // 
            this.splitLayersTextBox.Location = new System.Drawing.Point(87, 27);
            this.splitLayersTextBox.Name = "splitLayersTextBox";
            this.splitLayersTextBox.Size = new System.Drawing.Size(60, 23);
            this.splitLayersTextBox.TabIndex = 1;
            this.splitLayersTextBox.Text = "999";
            // 
            // splitLayersLabel
            // 
            this.splitLayersLabel.AutoSize = true;
            this.splitLayersLabel.Location = new System.Drawing.Point(20, 30);
            this.splitLayersLabel.Name = "splitLayersLabel";
            this.splitLayersLabel.Size = new System.Drawing.Size(66, 15);
            this.splitLayersLabel.TabIndex = 0;
            this.splitLayersLabel.Text = "Split layers:";
            // 
            // someParametersGroupBox
            // 
            this.someParametersGroupBox.Controls.Add(this.waveEnableTextBox);
            this.someParametersGroupBox.Controls.Add(this.weldShieldTextBox);
            this.someParametersGroupBox.Controls.Add(this.roEnableCheckBox);
            this.someParametersGroupBox.Controls.Add(this.waveEnableCheckBox);
            this.someParametersGroupBox.Controls.Add(this.weldShieldCheckBox);
            this.someParametersGroupBox.Controls.Add(this.removeSmallStopStartCheckBox);
            this.someParametersGroupBox.Controls.Add(this.checkingDistanceTextBox);
            this.someParametersGroupBox.Controls.Add(this.useWeldSpeedCheckBox);
            this.someParametersGroupBox.Controls.Add(this.checkingDistanceLabel);
            this.someParametersGroupBox.Controls.Add(this.autoArcEnabledByExtrusionCheckBox);
            this.someParametersGroupBox.Controls.Add(this.runWithoutArcCheckBox);
            this.someParametersGroupBox.Location = new System.Drawing.Point(30, 359);
            this.someParametersGroupBox.Name = "someParametersGroupBox";
            this.someParametersGroupBox.Size = new System.Drawing.Size(617, 125);
            this.someParametersGroupBox.TabIndex = 30;
            this.someParametersGroupBox.TabStop = false;
            this.someParametersGroupBox.Text = "Some Parameters";
            // 
            // waveEnableTextBox
            // 
            this.waveEnableTextBox.Enabled = false;
            this.waveEnableTextBox.Location = new System.Drawing.Point(573, 59);
            this.waveEnableTextBox.Name = "waveEnableTextBox";
            this.waveEnableTextBox.Size = new System.Drawing.Size(25, 23);
            this.waveEnableTextBox.TabIndex = 29;
            this.waveEnableTextBox.Text = "5";
            // 
            // weldShieldTextBox
            // 
            this.weldShieldTextBox.Enabled = false;
            this.weldShieldTextBox.Location = new System.Drawing.Point(573, 26);
            this.weldShieldTextBox.Name = "weldShieldTextBox";
            this.weldShieldTextBox.Size = new System.Drawing.Size(25, 23);
            this.weldShieldTextBox.TabIndex = 21;
            this.weldShieldTextBox.Text = "5";
            // 
            // roEnableCheckBox
            // 
            this.roEnableCheckBox.AutoSize = true;
            this.roEnableCheckBox.Location = new System.Drawing.Point(461, 88);
            this.roEnableCheckBox.Name = "roEnableCheckBox";
            this.roEnableCheckBox.Size = new System.Drawing.Size(80, 19);
            this.roEnableCheckBox.TabIndex = 28;
            this.roEnableCheckBox.Text = "RO enable";
            this.roEnableCheckBox.UseVisualStyleBackColor = true;
            // 
            // waveEnableCheckBox
            // 
            this.waveEnableCheckBox.AutoSize = true;
            this.waveEnableCheckBox.Location = new System.Drawing.Point(461, 59);
            this.waveEnableCheckBox.Name = "waveEnableCheckBox";
            this.waveEnableCheckBox.Size = new System.Drawing.Size(93, 19);
            this.waveEnableCheckBox.TabIndex = 27;
            this.waveEnableCheckBox.Text = "Wave enable";
            this.waveEnableCheckBox.UseVisualStyleBackColor = true;
            this.waveEnableCheckBox.CheckedChanged += new System.EventHandler(this.WaveEnableCheckBox_CheckedChanged);
            // 
            // weldShieldCheckBox
            // 
            this.weldShieldCheckBox.AutoSize = true;
            this.weldShieldCheckBox.Location = new System.Drawing.Point(461, 28);
            this.weldShieldCheckBox.Name = "weldShieldCheckBox";
            this.weldShieldCheckBox.Size = new System.Drawing.Size(88, 19);
            this.weldShieldCheckBox.TabIndex = 26;
            this.weldShieldCheckBox.Text = "Weld Shield";
            this.weldShieldCheckBox.UseVisualStyleBackColor = true;
            this.weldShieldCheckBox.CheckedChanged += new System.EventHandler(this.WeldShieldCheckBox_CheckedChanged);
            // 
            // removeSmallStopStartCheckBox
            // 
            this.removeSmallStopStartCheckBox.AutoSize = true;
            this.removeSmallStopStartCheckBox.Location = new System.Drawing.Point(259, 59);
            this.removeSmallStopStartCheckBox.Name = "removeSmallStopStartCheckBox";
            this.removeSmallStopStartCheckBox.Size = new System.Drawing.Size(154, 19);
            this.removeSmallStopStartCheckBox.TabIndex = 25;
            this.removeSmallStopStartCheckBox.Text = "Remove small stop-start";
            this.removeSmallStopStartCheckBox.UseVisualStyleBackColor = true;
            this.removeSmallStopStartCheckBox.CheckedChanged += new System.EventHandler(this.RemoveSmallStopStartCheckBox_CheckedChanged);
            // 
            // checkingDistanceTextBox
            // 
            this.checkingDistanceTextBox.Enabled = false;
            this.checkingDistanceTextBox.Location = new System.Drawing.Point(400, 26);
            this.checkingDistanceTextBox.Name = "checkingDistanceTextBox";
            this.checkingDistanceTextBox.Size = new System.Drawing.Size(25, 23);
            this.checkingDistanceTextBox.TabIndex = 22;
            this.checkingDistanceTextBox.Text = "4";
            // 
            // useWeldSpeedCheckBox
            // 
            this.useWeldSpeedCheckBox.AutoSize = true;
            this.useWeldSpeedCheckBox.Location = new System.Drawing.Point(20, 88);
            this.useWeldSpeedCheckBox.Name = "useWeldSpeedCheckBox";
            this.useWeldSpeedCheckBox.Size = new System.Drawing.Size(218, 19);
            this.useWeldSpeedCheckBox.TabIndex = 24;
            this.useWeldSpeedCheckBox.Text = "Use WELD_SPEED instead of feedrate";
            this.useWeldSpeedCheckBox.UseVisualStyleBackColor = true;
            // 
            // checkingDistanceLabel
            // 
            this.checkingDistanceLabel.AutoSize = true;
            this.checkingDistanceLabel.Location = new System.Drawing.Point(259, 30);
            this.checkingDistanceLabel.Name = "checkingDistanceLabel";
            this.checkingDistanceLabel.Size = new System.Drawing.Size(140, 15);
            this.checkingDistanceLabel.TabIndex = 21;
            this.checkingDistanceLabel.Text = "Checking distance (mm):";
            // 
            // autoArcEnabledByExtrusionCheckBox
            // 
            this.autoArcEnabledByExtrusionCheckBox.AutoSize = true;
            this.autoArcEnabledByExtrusionCheckBox.Checked = true;
            this.autoArcEnabledByExtrusionCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.autoArcEnabledByExtrusionCheckBox.Location = new System.Drawing.Point(20, 59);
            this.autoArcEnabledByExtrusionCheckBox.Name = "autoArcEnabledByExtrusionCheckBox";
            this.autoArcEnabledByExtrusionCheckBox.Size = new System.Drawing.Size(186, 19);
            this.autoArcEnabledByExtrusionCheckBox.TabIndex = 23;
            this.autoArcEnabledByExtrusionCheckBox.Text = "Auto Arc enabled by extrusion";
            this.autoArcEnabledByExtrusionCheckBox.UseVisualStyleBackColor = true;
            // 
            // runWithoutArcCheckBox
            // 
            this.runWithoutArcCheckBox.AutoSize = true;
            this.runWithoutArcCheckBox.Location = new System.Drawing.Point(20, 30);
            this.runWithoutArcCheckBox.Name = "runWithoutArcCheckBox";
            this.runWithoutArcCheckBox.Size = new System.Drawing.Size(112, 19);
            this.runWithoutArcCheckBox.TabIndex = 22;
            this.runWithoutArcCheckBox.Text = "Run without Arc";
            this.runWithoutArcCheckBox.UseVisualStyleBackColor = true;
            // 
            // weldingMovementTextBox
            // 
            this.weldingMovementTextBox.Location = new System.Drawing.Point(451, 311);
            this.weldingMovementTextBox.Name = "weldingMovementTextBox";
            this.weldingMovementTextBox.Size = new System.Drawing.Size(122, 23);
            this.weldingMovementTextBox.TabIndex = 34;
            this.weldingMovementTextBox.Text = "CNT100 COORD PTH";
            // 
            // weldingMovementLabel
            // 
            this.weldingMovementLabel.AutoSize = true;
            this.weldingMovementLabel.Location = new System.Drawing.Point(330, 314);
            this.weldingMovementLabel.Name = "weldingMovementLabel";
            this.weldingMovementLabel.Size = new System.Drawing.Size(115, 15);
            this.weldingMovementLabel.TabIndex = 33;
            this.weldingMovementLabel.Text = "Welding movement:";
            // 
            // normalMovementTextBox
            // 
            this.normalMovementTextBox.Location = new System.Drawing.Point(151, 311);
            this.normalMovementTextBox.Name = "normalMovementTextBox";
            this.normalMovementTextBox.Size = new System.Drawing.Size(119, 23);
            this.normalMovementTextBox.TabIndex = 31;
            this.normalMovementTextBox.Text = "CNT100 COORD";
            // 
            // normalMovementLabel
            // 
            this.normalMovementLabel.AutoSize = true;
            this.normalMovementLabel.Location = new System.Drawing.Point(30, 314);
            this.normalMovementLabel.Name = "normalMovementLabel";
            this.normalMovementLabel.Size = new System.Drawing.Size(111, 15);
            this.normalMovementLabel.TabIndex = 27;
            this.normalMovementLabel.Text = "Normal movement:";
            // 
            // xzChtoZaPropertiesToolBox
            // 
            this.xzChtoZaPropertiesToolBox.Controls.Add(this.turnsJ6TextBox);
            this.xzChtoZaPropertiesToolBox.Controls.Add(this.turnsJ4TextBox);
            this.xzChtoZaPropertiesToolBox.Controls.Add(this.turnsJ1TextBox);
            this.xzChtoZaPropertiesToolBox.Controls.Add(this.turnsJ6Label);
            this.xzChtoZaPropertiesToolBox.Controls.Add(this.turnsJ4Label);
            this.xzChtoZaPropertiesToolBox.Controls.Add(this.turnsJ1Label);
            this.xzChtoZaPropertiesToolBox.Controls.Add(this.baseComboBox);
            this.xzChtoZaPropertiesToolBox.Controls.Add(this.baseLabel);
            this.xzChtoZaPropertiesToolBox.Controls.Add(this.armComboBox);
            this.xzChtoZaPropertiesToolBox.Controls.Add(this.armLabel);
            this.xzChtoZaPropertiesToolBox.Controls.Add(this.wristComboBox);
            this.xzChtoZaPropertiesToolBox.Controls.Add(this.wristLabel);
            this.xzChtoZaPropertiesToolBox.Location = new System.Drawing.Point(821, 164);
            this.xzChtoZaPropertiesToolBox.Name = "xzChtoZaPropertiesToolBox";
            this.xzChtoZaPropertiesToolBox.Size = new System.Drawing.Size(238, 125);
            this.xzChtoZaPropertiesToolBox.TabIndex = 32;
            this.xzChtoZaPropertiesToolBox.TabStop = false;
            this.xzChtoZaPropertiesToolBox.Text = "XZ Chto Za Properties";
            // 
            // turnsJ6TextBox
            // 
            this.turnsJ6TextBox.Location = new System.Drawing.Point(193, 85);
            this.turnsJ6TextBox.Name = "turnsJ6TextBox";
            this.turnsJ6TextBox.Size = new System.Drawing.Size(25, 23);
            this.turnsJ6TextBox.TabIndex = 15;
            this.turnsJ6TextBox.Text = "50";
            // 
            // turnsJ4TextBox
            // 
            this.turnsJ4TextBox.Location = new System.Drawing.Point(193, 56);
            this.turnsJ4TextBox.Name = "turnsJ4TextBox";
            this.turnsJ4TextBox.Size = new System.Drawing.Size(25, 23);
            this.turnsJ4TextBox.TabIndex = 14;
            this.turnsJ4TextBox.Text = "0";
            // 
            // turnsJ1TextBox
            // 
            this.turnsJ1TextBox.Location = new System.Drawing.Point(193, 27);
            this.turnsJ1TextBox.Name = "turnsJ1TextBox";
            this.turnsJ1TextBox.Size = new System.Drawing.Size(25, 23);
            this.turnsJ1TextBox.TabIndex = 13;
            this.turnsJ1TextBox.Text = "0";
            // 
            // turnsJ6Label
            // 
            this.turnsJ6Label.AutoSize = true;
            this.turnsJ6Label.Location = new System.Drawing.Point(142, 88);
            this.turnsJ6Label.Name = "turnsJ6Label";
            this.turnsJ6Label.Size = new System.Drawing.Size(52, 15);
            this.turnsJ6Label.TabIndex = 12;
            this.turnsJ6Label.Text = "Turns J6:";
            // 
            // turnsJ4Label
            // 
            this.turnsJ4Label.AutoSize = true;
            this.turnsJ4Label.Location = new System.Drawing.Point(142, 59);
            this.turnsJ4Label.Name = "turnsJ4Label";
            this.turnsJ4Label.Size = new System.Drawing.Size(52, 15);
            this.turnsJ4Label.TabIndex = 10;
            this.turnsJ4Label.Text = "Turns J4:";
            // 
            // turnsJ1Label
            // 
            this.turnsJ1Label.AutoSize = true;
            this.turnsJ1Label.Location = new System.Drawing.Point(142, 30);
            this.turnsJ1Label.Name = "turnsJ1Label";
            this.turnsJ1Label.Size = new System.Drawing.Size(52, 15);
            this.turnsJ1Label.TabIndex = 8;
            this.turnsJ1Label.Text = "Turns J1:";
            // 
            // baseComboBox
            // 
            this.baseComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.baseComboBox.FormattingEnabled = true;
            this.baseComboBox.Items.AddRange(new object[] {
            "FRONT",
            "BACK"});
            this.baseComboBox.Location = new System.Drawing.Point(57, 85);
            this.baseComboBox.Name = "baseComboBox";
            this.baseComboBox.Size = new System.Drawing.Size(70, 23);
            this.baseComboBox.TabIndex = 7;
            // 
            // baseLabel
            // 
            this.baseLabel.AutoSize = true;
            this.baseLabel.Location = new System.Drawing.Point(20, 88);
            this.baseLabel.Name = "baseLabel";
            this.baseLabel.Size = new System.Drawing.Size(34, 15);
            this.baseLabel.TabIndex = 6;
            this.baseLabel.Text = "Base:";
            // 
            // armComboBox
            // 
            this.armComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.armComboBox.FormattingEnabled = true;
            this.armComboBox.Items.AddRange(new object[] {
            "UP",
            "DOWN"});
            this.armComboBox.Location = new System.Drawing.Point(57, 56);
            this.armComboBox.Name = "armComboBox";
            this.armComboBox.Size = new System.Drawing.Size(70, 23);
            this.armComboBox.TabIndex = 5;
            // 
            // armLabel
            // 
            this.armLabel.AutoSize = true;
            this.armLabel.Location = new System.Drawing.Point(20, 59);
            this.armLabel.Name = "armLabel";
            this.armLabel.Size = new System.Drawing.Size(33, 15);
            this.armLabel.TabIndex = 4;
            this.armLabel.Text = "Arm:";
            // 
            // wristComboBox
            // 
            this.wristComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.wristComboBox.FormattingEnabled = true;
            this.wristComboBox.Items.AddRange(new object[] {
            "NO_FLIP",
            "FLIP"});
            this.wristComboBox.Location = new System.Drawing.Point(57, 27);
            this.wristComboBox.Name = "wristComboBox";
            this.wristComboBox.Size = new System.Drawing.Size(70, 23);
            this.wristComboBox.TabIndex = 3;
            // 
            // wristLabel
            // 
            this.wristLabel.AutoSize = true;
            this.wristLabel.Location = new System.Drawing.Point(20, 30);
            this.wristLabel.Name = "wristLabel";
            this.wristLabel.Size = new System.Drawing.Size(37, 15);
            this.wristLabel.TabIndex = 0;
            this.wristLabel.Text = "Wrist:";
            // 
            // robotFrameGroupBox
            // 
            this.robotFrameGroupBox.Controls.Add(this.robotUtTextBox);
            this.robotFrameGroupBox.Controls.Add(this.robotUtLabel);
            this.robotFrameGroupBox.Controls.Add(this.robotUfTextBox);
            this.robotFrameGroupBox.Controls.Add(this.robotUfLabel);
            this.robotFrameGroupBox.Location = new System.Drawing.Point(443, 164);
            this.robotFrameGroupBox.Name = "robotFrameGroupBox";
            this.robotFrameGroupBox.Size = new System.Drawing.Size(130, 125);
            this.robotFrameGroupBox.TabIndex = 28;
            this.robotFrameGroupBox.TabStop = false;
            this.robotFrameGroupBox.Text = "Robot Frame";
            // 
            // robotUtTextBox
            // 
            this.robotUtTextBox.Location = new System.Drawing.Point(45, 56);
            this.robotUtTextBox.Name = "robotUtTextBox";
            this.robotUtTextBox.Size = new System.Drawing.Size(60, 23);
            this.robotUtTextBox.TabIndex = 3;
            this.robotUtTextBox.Text = "5";
            // 
            // robotUtLabel
            // 
            this.robotUtLabel.AutoSize = true;
            this.robotUtLabel.Location = new System.Drawing.Point(20, 59);
            this.robotUtLabel.Name = "robotUtLabel";
            this.robotUtLabel.Size = new System.Drawing.Size(24, 15);
            this.robotUtLabel.TabIndex = 2;
            this.robotUtLabel.Text = "UT:";
            // 
            // robotUfTextBox
            // 
            this.robotUfTextBox.Location = new System.Drawing.Point(45, 27);
            this.robotUfTextBox.Name = "robotUfTextBox";
            this.robotUfTextBox.Size = new System.Drawing.Size(60, 23);
            this.robotUfTextBox.TabIndex = 1;
            this.robotUfTextBox.Text = "6";
            // 
            // robotUfLabel
            // 
            this.robotUfLabel.AutoSize = true;
            this.robotUfLabel.Location = new System.Drawing.Point(20, 30);
            this.robotUfLabel.Name = "robotUfLabel";
            this.robotUfLabel.Size = new System.Drawing.Size(24, 15);
            this.robotUfLabel.TabIndex = 0;
            this.robotUfLabel.Text = "UF:";
            // 
            // codeOffsetGroupBox
            // 
            this.codeOffsetGroupBox.Controls.Add(this.j2OffsetTextBox);
            this.codeOffsetGroupBox.Controls.Add(this.j2OffsetLabel);
            this.codeOffsetGroupBox.Controls.Add(this.j1OffsetTextBox);
            this.codeOffsetGroupBox.Controls.Add(this.j1OffsetLabel);
            this.codeOffsetGroupBox.Controls.Add(this.zTextBox);
            this.codeOffsetGroupBox.Controls.Add(this.zLabel);
            this.codeOffsetGroupBox.Controls.Add(this.yTextBox);
            this.codeOffsetGroupBox.Controls.Add(this.yLabel);
            this.codeOffsetGroupBox.Controls.Add(this.xTextBox);
            this.codeOffsetGroupBox.Controls.Add(this.xLabel);
            this.codeOffsetGroupBox.Controls.Add(this.rTextBox);
            this.codeOffsetGroupBox.Controls.Add(this.rLabel);
            this.codeOffsetGroupBox.Controls.Add(this.pTextBox);
            this.codeOffsetGroupBox.Controls.Add(this.pLabel);
            this.codeOffsetGroupBox.Controls.Add(this.wTextBox);
            this.codeOffsetGroupBox.Controls.Add(this.wLabel);
            this.codeOffsetGroupBox.Location = new System.Drawing.Point(30, 164);
            this.codeOffsetGroupBox.Name = "codeOffsetGroupBox";
            this.codeOffsetGroupBox.Size = new System.Drawing.Size(354, 125);
            this.codeOffsetGroupBox.TabIndex = 26;
            this.codeOffsetGroupBox.TabStop = false;
            this.codeOffsetGroupBox.Text = "Code Offset";
            // 
            // j2OffsetTextBox
            // 
            this.j2OffsetTextBox.Location = new System.Drawing.Point(274, 59);
            this.j2OffsetTextBox.Name = "j2OffsetTextBox";
            this.j2OffsetTextBox.Size = new System.Drawing.Size(60, 23);
            this.j2OffsetTextBox.TabIndex = 15;
            this.j2OffsetTextBox.Text = "0";
            // 
            // j2OffsetLabel
            // 
            this.j2OffsetLabel.AutoSize = true;
            this.j2OffsetLabel.Location = new System.Drawing.Point(231, 62);
            this.j2OffsetLabel.Name = "j2OffsetLabel";
            this.j2OffsetLabel.Size = new System.Drawing.Size(34, 15);
            this.j2OffsetLabel.TabIndex = 14;
            this.j2OffsetLabel.Text = "B(j2):";
            // 
            // j1OffsetTextBox
            // 
            this.j1OffsetTextBox.Location = new System.Drawing.Point(274, 30);
            this.j1OffsetTextBox.Name = "j1OffsetTextBox";
            this.j1OffsetTextBox.Size = new System.Drawing.Size(60, 23);
            this.j1OffsetTextBox.TabIndex = 13;
            this.j1OffsetTextBox.Text = "0";
            // 
            // j1OffsetLabel
            // 
            this.j1OffsetLabel.AutoSize = true;
            this.j1OffsetLabel.Location = new System.Drawing.Point(231, 33);
            this.j1OffsetLabel.Name = "j1OffsetLabel";
            this.j1OffsetLabel.Size = new System.Drawing.Size(35, 15);
            this.j1OffsetLabel.TabIndex = 12;
            this.j1OffsetLabel.Text = "A(j1):";
            // 
            // zTextBox
            // 
            this.zTextBox.Location = new System.Drawing.Point(151, 85);
            this.zTextBox.Name = "zTextBox";
            this.zTextBox.Size = new System.Drawing.Size(60, 23);
            this.zTextBox.TabIndex = 11;
            this.zTextBox.Text = "50";
            // 
            // zLabel
            // 
            this.zLabel.AutoSize = true;
            this.zLabel.Location = new System.Drawing.Point(126, 88);
            this.zLabel.Name = "zLabel";
            this.zLabel.Size = new System.Drawing.Size(17, 15);
            this.zLabel.TabIndex = 10;
            this.zLabel.Text = "Z:";
            // 
            // yTextBox
            // 
            this.yTextBox.Location = new System.Drawing.Point(151, 56);
            this.yTextBox.Name = "yTextBox";
            this.yTextBox.Size = new System.Drawing.Size(60, 23);
            this.yTextBox.TabIndex = 9;
            this.yTextBox.Text = "0";
            // 
            // yLabel
            // 
            this.yLabel.AutoSize = true;
            this.yLabel.Location = new System.Drawing.Point(126, 59);
            this.yLabel.Name = "yLabel";
            this.yLabel.Size = new System.Drawing.Size(17, 15);
            this.yLabel.TabIndex = 8;
            this.yLabel.Text = "Y:";
            // 
            // xTextBox
            // 
            this.xTextBox.Location = new System.Drawing.Point(151, 27);
            this.xTextBox.Name = "xTextBox";
            this.xTextBox.Size = new System.Drawing.Size(60, 23);
            this.xTextBox.TabIndex = 7;
            this.xTextBox.Text = "0";
            // 
            // xLabel
            // 
            this.xLabel.AutoSize = true;
            this.xLabel.Location = new System.Drawing.Point(126, 30);
            this.xLabel.Name = "xLabel";
            this.xLabel.Size = new System.Drawing.Size(17, 15);
            this.xLabel.TabIndex = 6;
            this.xLabel.Text = "X:";
            // 
            // rTextBox
            // 
            this.rTextBox.Location = new System.Drawing.Point(45, 85);
            this.rTextBox.Name = "rTextBox";
            this.rTextBox.Size = new System.Drawing.Size(60, 23);
            this.rTextBox.TabIndex = 5;
            this.rTextBox.Text = "0";
            // 
            // rLabel
            // 
            this.rLabel.AutoSize = true;
            this.rLabel.Location = new System.Drawing.Point(20, 88);
            this.rLabel.Name = "rLabel";
            this.rLabel.Size = new System.Drawing.Size(17, 15);
            this.rLabel.TabIndex = 4;
            this.rLabel.Text = "R:";
            // 
            // pTextBox
            // 
            this.pTextBox.Location = new System.Drawing.Point(45, 56);
            this.pTextBox.Name = "pTextBox";
            this.pTextBox.Size = new System.Drawing.Size(60, 23);
            this.pTextBox.TabIndex = 3;
            this.pTextBox.Text = "0";
            // 
            // pLabel
            // 
            this.pLabel.AutoSize = true;
            this.pLabel.Location = new System.Drawing.Point(20, 59);
            this.pLabel.Name = "pLabel";
            this.pLabel.Size = new System.Drawing.Size(17, 15);
            this.pLabel.TabIndex = 2;
            this.pLabel.Text = "P:";
            // 
            // wTextBox
            // 
            this.wTextBox.Location = new System.Drawing.Point(45, 27);
            this.wTextBox.Name = "wTextBox";
            this.wTextBox.Size = new System.Drawing.Size(60, 23);
            this.wTextBox.TabIndex = 1;
            this.wTextBox.Text = "0";
            // 
            // wLabel
            // 
            this.wLabel.AutoSize = true;
            this.wLabel.Location = new System.Drawing.Point(20, 30);
            this.wLabel.Name = "wLabel";
            this.wLabel.Size = new System.Drawing.Size(21, 15);
            this.wLabel.TabIndex = 0;
            this.wLabel.Text = "W:";
            // 
            // angleScriptCheckBox
            // 
            this.angleScriptCheckBox.AutoSize = true;
            this.angleScriptCheckBox.Location = new System.Drawing.Point(175, 129);
            this.angleScriptCheckBox.Name = "angleScriptCheckBox";
            this.angleScriptCheckBox.Size = new System.Drawing.Size(188, 19);
            this.angleScriptCheckBox.TabIndex = 25;
            this.angleScriptCheckBox.Text = "Pankratov Angle-Technology™";
            this.angleScriptCheckBox.UseVisualStyleBackColor = true;
            this.angleScriptCheckBox.CheckedChanged += new System.EventHandler(this.AngleScriptCheckBox_CheckedChanged);
            // 
            // powerMillExportCheckBox
            // 
            this.powerMillExportCheckBox.AutoSize = true;
            this.powerMillExportCheckBox.Location = new System.Drawing.Point(30, 129);
            this.powerMillExportCheckBox.Name = "powerMillExportCheckBox";
            this.powerMillExportCheckBox.Size = new System.Drawing.Size(116, 19);
            this.powerMillExportCheckBox.TabIndex = 24;
            this.powerMillExportCheckBox.Text = "PowerMill Export";
            this.powerMillExportCheckBox.UseVisualStyleBackColor = true;
            this.powerMillExportCheckBox.CheckedChanged += new System.EventHandler(this.PowerMillExportCheckBox_CheckedChanged);
            // 
            // fileSettingsGroupBox
            // 
            this.fileSettingsGroupBox.Controls.Add(this.gCodeFilenamePanel);
            this.fileSettingsGroupBox.Location = new System.Drawing.Point(30, 24);
            this.fileSettingsGroupBox.Name = "fileSettingsGroupBox";
            this.fileSettingsGroupBox.Size = new System.Drawing.Size(549, 80);
            this.fileSettingsGroupBox.TabIndex = 23;
            this.fileSettingsGroupBox.TabStop = false;
            this.fileSettingsGroupBox.Text = "File Settings";
            // 
            // gCodeFilenamePanel
            // 
            this.gCodeFilenamePanel.Controls.Add(this.gCodeFilenameLabel);
            this.gCodeFilenamePanel.Controls.Add(this.gCodeFilenameButton);
            this.gCodeFilenamePanel.Controls.Add(this.gCodeFilenameTextBox);
            this.gCodeFilenamePanel.Location = new System.Drawing.Point(5, 13);
            this.gCodeFilenamePanel.Name = "gCodeFilenamePanel";
            this.gCodeFilenamePanel.Size = new System.Drawing.Size(540, 61);
            this.gCodeFilenamePanel.TabIndex = 0;
            // 
            // gCodeFilenameLabel
            // 
            this.gCodeFilenameLabel.AutoSize = true;
            this.gCodeFilenameLabel.Location = new System.Drawing.Point(30, 23);
            this.gCodeFilenameLabel.Name = "gCodeFilenameLabel";
            this.gCodeFilenameLabel.Size = new System.Drawing.Size(98, 15);
            this.gCodeFilenameLabel.TabIndex = 0;
            this.gCodeFilenameLabel.Text = "GCode file name:";
            // 
            // gCodeFilenameButton
            // 
            this.gCodeFilenameButton.Location = new System.Drawing.Point(435, 19);
            this.gCodeFilenameButton.Name = "gCodeFilenameButton";
            this.gCodeFilenameButton.Size = new System.Drawing.Size(75, 23);
            this.gCodeFilenameButton.TabIndex = 2;
            this.gCodeFilenameButton.Text = "Open";
            this.gCodeFilenameButton.UseVisualStyleBackColor = true;
            this.gCodeFilenameButton.Click += new System.EventHandler(this.GCodeFilenameButton_Click);
            // 
            // gCodeFilenameTextBox
            // 
            this.gCodeFilenameTextBox.Location = new System.Drawing.Point(146, 19);
            this.gCodeFilenameTextBox.Name = "gCodeFilenameTextBox";
            this.gCodeFilenameTextBox.Size = new System.Drawing.Size(274, 23);
            this.gCodeFilenameTextBox.TabIndex = 1;
            // 
            // sendToRobotTabPage
            // 
            this.sendToRobotTabPage.Controls.Add(this.panel1);
            this.sendToRobotTabPage.Location = new System.Drawing.Point(4, 24);
            this.sendToRobotTabPage.Name = "sendToRobotTabPage";
            this.sendToRobotTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.sendToRobotTabPage.Size = new System.Drawing.Size(1089, 557);
            this.sendToRobotTabPage.TabIndex = 1;
            this.sendToRobotTabPage.Text = "Send to robot";
            this.sendToRobotTabPage.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.sendToBoxMainPanel);
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1149, 557);
            this.panel1.TabIndex = 0;
            // 
            // sendToBoxMainPanel
            // 
            this.sendToBoxMainPanel.Controls.Add(this.robotParamsGroupBox);
            this.sendToBoxMainPanel.Controls.Add(this.avaiableRobotListComboBox);
            this.sendToBoxMainPanel.Controls.Add(this.avaibaleRobotListLabel);
            this.sendToBoxMainPanel.Controls.Add(this.robotsTable);
            this.sendToBoxMainPanel.Location = new System.Drawing.Point(0, 0);
            this.sendToBoxMainPanel.Name = "sendToBoxMainPanel";
            this.sendToBoxMainPanel.Size = new System.Drawing.Size(1089, 557);
            this.sendToBoxMainPanel.TabIndex = 0;
            // 
            // robotParamsGroupBox
            // 
            this.robotParamsGroupBox.Controls.Add(this.sendTpFileCheckBox);
            this.robotParamsGroupBox.Controls.Add(this.addButton);
            this.robotParamsGroupBox.Controls.Add(this.robotAddressTextBox);
            this.robotParamsGroupBox.Controls.Add(this.enterRobotAddressLabel);
            this.robotParamsGroupBox.Location = new System.Drawing.Point(30, 30);
            this.robotParamsGroupBox.Name = "robotParamsGroupBox";
            this.robotParamsGroupBox.Size = new System.Drawing.Size(544, 60);
            this.robotParamsGroupBox.TabIndex = 3;
            this.robotParamsGroupBox.TabStop = false;
            this.robotParamsGroupBox.Text = "Robot Params";
            // 
            // sendTpFileCheckBox
            // 
            this.sendTpFileCheckBox.AutoSize = true;
            this.sendTpFileCheckBox.Checked = true;
            this.sendTpFileCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.sendTpFileCheckBox.Enabled = false;
            this.sendTpFileCheckBox.Location = new System.Drawing.Point(431, 22);
            this.sendTpFileCheckBox.Name = "sendTpFileCheckBox";
            this.sendTpFileCheckBox.Size = new System.Drawing.Size(87, 19);
            this.sendTpFileCheckBox.TabIndex = 3;
            this.sendTpFileCheckBox.Text = "Send TP file";
            this.sendTpFileCheckBox.UseVisualStyleBackColor = true;
            // 
            // addButton
            // 
            this.addButton.Location = new System.Drawing.Point(334, 20);
            this.addButton.Name = "addButton";
            this.addButton.Size = new System.Drawing.Size(75, 23);
            this.addButton.TabIndex = 2;
            this.addButton.Text = "Add";
            this.addButton.UseVisualStyleBackColor = true;
            this.addButton.Click += new System.EventHandler(this.AddButton_Click);
            // 
            // robotAddressTextBox
            // 
            this.robotAddressTextBox.Location = new System.Drawing.Point(157, 20);
            this.robotAddressTextBox.Name = "robotAddressTextBox";
            this.robotAddressTextBox.Size = new System.Drawing.Size(157, 23);
            this.robotAddressTextBox.TabIndex = 1;
            this.robotAddressTextBox.Text = "192.168.8.101";
            // 
            // enterRobotAddressLabel
            // 
            this.enterRobotAddressLabel.AutoSize = true;
            this.enterRobotAddressLabel.Location = new System.Drawing.Point(30, 23);
            this.enterRobotAddressLabel.Name = "enterRobotAddressLabel";
            this.enterRobotAddressLabel.Size = new System.Drawing.Size(112, 15);
            this.enterRobotAddressLabel.TabIndex = 0;
            this.enterRobotAddressLabel.Text = "Enter robot address:";
            // 
            // avaiableRobotListComboBox
            // 
            this.avaiableRobotListComboBox.FormattingEnabled = true;
            this.avaiableRobotListComboBox.Location = new System.Drawing.Point(200, 115);
            this.avaiableRobotListComboBox.Name = "avaiableRobotListComboBox";
            this.avaiableRobotListComboBox.Size = new System.Drawing.Size(374, 23);
            this.avaiableRobotListComboBox.TabIndex = 2;
            this.avaiableRobotListComboBox.Visible = false;
            // 
            // avaibaleRobotListLabel
            // 
            this.avaibaleRobotListLabel.AutoSize = true;
            this.avaibaleRobotListLabel.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.avaibaleRobotListLabel.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.avaibaleRobotListLabel.Location = new System.Drawing.Point(30, 115);
            this.avaibaleRobotListLabel.Name = "avaibaleRobotListLabel";
            this.avaibaleRobotListLabel.Size = new System.Drawing.Size(164, 25);
            this.avaibaleRobotListLabel.TabIndex = 1;
            this.avaibaleRobotListLabel.Text = "Avaibale robot list";
            // 
            // robotsTable
            // 
            this.robotsTable.AllowUserToAddRows = false;
            this.robotsTable.AllowUserToResizeColumns = false;
            this.robotsTable.AllowUserToResizeRows = false;
            this.robotsTable.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.robotsTable.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.IDColumn,
            this.ipAddressColumn,
            this.currentRobotStateColumn});
            this.robotsTable.Location = new System.Drawing.Point(30, 164);
            this.robotsTable.Name = "robotsTable";
            this.robotsTable.ReadOnly = true;
            this.robotsTable.RowTemplate.Height = 25;
            this.robotsTable.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.robotsTable.Size = new System.Drawing.Size(544, 362);
            this.robotsTable.TabIndex = 0;
            this.robotsTable.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.RobotsTable_CellClick);
            this.robotsTable.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.RobotsTable_CellDoubleClick);
            // 
            // IDColumn
            // 
            this.IDColumn.HeaderText = "ID";
            this.IDColumn.Name = "IDColumn";
            this.IDColumn.ReadOnly = true;
            // 
            // ipAddressColumn
            // 
            this.ipAddressColumn.HeaderText = "IP Address";
            this.ipAddressColumn.Name = "ipAddressColumn";
            this.ipAddressColumn.ReadOnly = true;
            this.ipAddressColumn.Width = 200;
            // 
            // currentRobotStateColumn
            // 
            this.currentRobotStateColumn.HeaderText = "Current Robot State";
            this.currentRobotStateColumn.Name = "currentRobotStateColumn";
            this.currentRobotStateColumn.ReadOnly = true;
            this.currentRobotStateColumn.Width = 200;
            // 
            // convertToTpTabPage
            // 
            this.convertToTpTabPage.Controls.Add(this.convertToTpMainPanel);
            this.convertToTpTabPage.Location = new System.Drawing.Point(4, 24);
            this.convertToTpTabPage.Name = "convertToTpTabPage";
            this.convertToTpTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.convertToTpTabPage.Size = new System.Drawing.Size(1089, 557);
            this.convertToTpTabPage.TabIndex = 2;
            this.convertToTpTabPage.Text = "Convert to .tp";
            this.convertToTpTabPage.UseVisualStyleBackColor = true;
            // 
            // convertToTpMainPanel
            // 
            this.convertToTpMainPanel.Controls.Add(this.convertFileGroupBox);
            this.convertToTpMainPanel.Controls.Add(this.convertFolderGroupBox);
            this.convertToTpMainPanel.Location = new System.Drawing.Point(0, 0);
            this.convertToTpMainPanel.Name = "convertToTpMainPanel";
            this.convertToTpMainPanel.Size = new System.Drawing.Size(1089, 557);
            this.convertToTpMainPanel.TabIndex = 0;
            // 
            // convertFileGroupBox
            // 
            this.convertFileGroupBox.Controls.Add(this.helpPressConvertFileBtnLabel);
            this.convertFileGroupBox.Controls.Add(this.convertFileButton);
            this.convertFileGroupBox.Location = new System.Drawing.Point(157, 332);
            this.convertFileGroupBox.Name = "convertFileGroupBox";
            this.convertFileGroupBox.Size = new System.Drawing.Size(782, 101);
            this.convertFileGroupBox.TabIndex = 30;
            this.convertFileGroupBox.TabStop = false;
            this.convertFileGroupBox.Text = "Конвертировать файл";
            // 
            // helpPressConvertFileBtnLabel
            // 
            this.helpPressConvertFileBtnLabel.AutoSize = true;
            this.helpPressConvertFileBtnLabel.Location = new System.Drawing.Point(20, 48);
            this.helpPressConvertFileBtnLabel.Name = "helpPressConvertFileBtnLabel";
            this.helpPressConvertFileBtnLabel.Size = new System.Drawing.Size(129, 15);
            this.helpPressConvertFileBtnLabel.TabIndex = 26;
            this.helpPressConvertFileBtnLabel.Text = "1. Нажмите кнопочку:";
            // 
            // convertFileButton
            // 
            this.convertFileButton.Location = new System.Drawing.Point(300, 48);
            this.convertFileButton.Name = "convertFileButton";
            this.convertFileButton.Size = new System.Drawing.Size(182, 23);
            this.convertFileButton.TabIndex = 26;
            this.convertFileButton.Text = "Конвертировать файл";
            this.convertFileButton.UseVisualStyleBackColor = true;
            this.convertFileButton.Click += new System.EventHandler(this.ConvertFileButton_Click);
            // 
            // convertFolderGroupBox
            // 
            this.convertFolderGroupBox.Controls.Add(this.lsFolderPanel);
            this.convertFolderGroupBox.Controls.Add(this.helpPressConvertFolderBtnLabel);
            this.convertFolderGroupBox.Controls.Add(this.helpSelectFolderLabel);
            this.convertFolderGroupBox.Controls.Add(this.convertFolderButton);
            this.convertFolderGroupBox.Location = new System.Drawing.Point(157, 67);
            this.convertFolderGroupBox.Name = "convertFolderGroupBox";
            this.convertFolderGroupBox.Size = new System.Drawing.Size(782, 186);
            this.convertFolderGroupBox.TabIndex = 29;
            this.convertFolderGroupBox.TabStop = false;
            this.convertFolderGroupBox.Text = "Конвертировать папку";
            // 
            // lsFolderPanel
            // 
            this.lsFolderPanel.Controls.Add(this.lsFolderLabel);
            this.lsFolderPanel.Controls.Add(this.openLsFolderButton);
            this.lsFolderPanel.Controls.Add(this.selectedLsFolderTextBox);
            this.lsFolderPanel.Location = new System.Drawing.Point(175, 34);
            this.lsFolderPanel.Name = "lsFolderPanel";
            this.lsFolderPanel.Size = new System.Drawing.Size(540, 61);
            this.lsFolderPanel.TabIndex = 0;
            // 
            // lsFolderLabel
            // 
            this.lsFolderLabel.AutoSize = true;
            this.lsFolderLabel.Location = new System.Drawing.Point(30, 23);
            this.lsFolderLabel.Name = "lsFolderLabel";
            this.lsFolderLabel.Size = new System.Drawing.Size(67, 15);
            this.lsFolderLabel.TabIndex = 0;
            this.lsFolderLabel.Text = "Папка с .ls:";
            // 
            // openLsFolderButton
            // 
            this.openLsFolderButton.Location = new System.Drawing.Point(435, 19);
            this.openLsFolderButton.Name = "openLsFolderButton";
            this.openLsFolderButton.Size = new System.Drawing.Size(75, 23);
            this.openLsFolderButton.TabIndex = 2;
            this.openLsFolderButton.Text = "Open";
            this.openLsFolderButton.UseVisualStyleBackColor = true;
            this.openLsFolderButton.Click += new System.EventHandler(this.OpenLsFolderButton_Click);
            // 
            // selectedLsFolderTextBox
            // 
            this.selectedLsFolderTextBox.Location = new System.Drawing.Point(119, 19);
            this.selectedLsFolderTextBox.Name = "selectedLsFolderTextBox";
            this.selectedLsFolderTextBox.Size = new System.Drawing.Size(274, 23);
            this.selectedLsFolderTextBox.TabIndex = 1;
            // 
            // helpPressConvertFolderBtnLabel
            // 
            this.helpPressConvertFolderBtnLabel.AutoSize = true;
            this.helpPressConvertFolderBtnLabel.Location = new System.Drawing.Point(20, 138);
            this.helpPressConvertFolderBtnLabel.Name = "helpPressConvertFolderBtnLabel";
            this.helpPressConvertFolderBtnLabel.Size = new System.Drawing.Size(129, 15);
            this.helpPressConvertFolderBtnLabel.TabIndex = 26;
            this.helpPressConvertFolderBtnLabel.Text = "2. Нажмите кнопочку:";
            // 
            // helpSelectFolderLabel
            // 
            this.helpSelectFolderLabel.AutoSize = true;
            this.helpSelectFolderLabel.Location = new System.Drawing.Point(20, 56);
            this.helpSelectFolderLabel.Name = "helpSelectFolderLabel";
            this.helpSelectFolderLabel.Size = new System.Drawing.Size(131, 15);
            this.helpSelectFolderLabel.TabIndex = 25;
            this.helpSelectFolderLabel.Text = "1. Выберите папку с .ls";
            // 
            // convertFolderButton
            // 
            this.convertFolderButton.Location = new System.Drawing.Point(300, 134);
            this.convertFolderButton.Name = "convertFolderButton";
            this.convertFolderButton.Size = new System.Drawing.Size(182, 23);
            this.convertFolderButton.TabIndex = 26;
            this.convertFolderButton.Text = "Конвертировать папку";
            this.convertFolderButton.UseVisualStyleBackColor = true;
            this.convertFolderButton.Click += new System.EventHandler(this.ConvertFolderButton_Click);
            // 
            // angleFixTabPage
            // 
            this.angleFixTabPage.Controls.Add(this.separatedAngleFixPanel);
            this.angleFixTabPage.Location = new System.Drawing.Point(4, 24);
            this.angleFixTabPage.Name = "angleFixTabPage";
            this.angleFixTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.angleFixTabPage.Size = new System.Drawing.Size(1089, 557);
            this.angleFixTabPage.TabIndex = 3;
            this.angleFixTabPage.Text = "Angle Fix";
            this.angleFixTabPage.UseVisualStyleBackColor = true;
            // 
            // separatedAngleFixPanel
            // 
            this.separatedAngleFixPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.separatedAngleFixPanel.Controls.Add(this.separateAngleFixGroupBox);
            this.separatedAngleFixPanel.Controls.Add(this.sepFixAngleButton);
            this.separatedAngleFixPanel.Controls.Add(this.sepLsFolderPanel);
            this.separatedAngleFixPanel.Location = new System.Drawing.Point(244, 114);
            this.separatedAngleFixPanel.Name = "separatedAngleFixPanel";
            this.separatedAngleFixPanel.Size = new System.Drawing.Size(570, 329);
            this.separatedAngleFixPanel.TabIndex = 33;
            // 
            // separateAngleFixGroupBox
            // 
            this.separateAngleFixGroupBox.Controls.Add(this.sepCriticalAngleDifferenceTextBox);
            this.separateAngleFixGroupBox.Controls.Add(this.sepCriticalAngleDifferenceLabel);
            this.separateAngleFixGroupBox.Controls.Add(this.sepMaxValueTextBox);
            this.separateAngleFixGroupBox.Controls.Add(this.sepMaxValueLabel);
            this.separateAngleFixGroupBox.Location = new System.Drawing.Point(87, 30);
            this.separateAngleFixGroupBox.Name = "separateAngleFixGroupBox";
            this.separateAngleFixGroupBox.Size = new System.Drawing.Size(397, 125);
            this.separateAngleFixGroupBox.TabIndex = 30;
            this.separateAngleFixGroupBox.TabStop = false;
            this.separateAngleFixGroupBox.Text = "Angle Fix";
            // 
            // sepCriticalAngleDifferenceTextBox
            // 
            this.sepCriticalAngleDifferenceTextBox.Location = new System.Drawing.Point(201, 57);
            this.sepCriticalAngleDifferenceTextBox.Name = "sepCriticalAngleDifferenceTextBox";
            this.sepCriticalAngleDifferenceTextBox.Size = new System.Drawing.Size(60, 23);
            this.sepCriticalAngleDifferenceTextBox.TabIndex = 3;
            this.sepCriticalAngleDifferenceTextBox.Text = "1";
            // 
            // sepCriticalAngleDifferenceLabel
            // 
            this.sepCriticalAngleDifferenceLabel.AutoSize = true;
            this.sepCriticalAngleDifferenceLabel.Location = new System.Drawing.Point(20, 59);
            this.sepCriticalAngleDifferenceLabel.Name = "sepCriticalAngleDifferenceLabel";
            this.sepCriticalAngleDifferenceLabel.Size = new System.Drawing.Size(162, 15);
            this.sepCriticalAngleDifferenceLabel.TabIndex = 2;
            this.sepCriticalAngleDifferenceLabel.Text = "Критическая разница углов:";
            // 
            // sepMaxValueTextBox
            // 
            this.sepMaxValueTextBox.Location = new System.Drawing.Point(201, 27);
            this.sepMaxValueTextBox.Name = "sepMaxValueTextBox";
            this.sepMaxValueTextBox.Size = new System.Drawing.Size(60, 23);
            this.sepMaxValueTextBox.TabIndex = 1;
            this.sepMaxValueTextBox.Text = "20";
            // 
            // sepMaxValueLabel
            // 
            this.sepMaxValueLabel.AutoSize = true;
            this.sepMaxValueLabel.Location = new System.Drawing.Point(20, 30);
            this.sepMaxValueLabel.Name = "sepMaxValueLabel";
            this.sepMaxValueLabel.Size = new System.Drawing.Size(175, 15);
            this.sepMaxValueLabel.TabIndex = 0;
            this.sepMaxValueLabel.Text = "Максимальное значение угла:";
            // 
            // sepFixAngleButton
            // 
            this.sepFixAngleButton.Location = new System.Drawing.Point(209, 276);
            this.sepFixAngleButton.Name = "sepFixAngleButton";
            this.sepFixAngleButton.Size = new System.Drawing.Size(152, 23);
            this.sepFixAngleButton.TabIndex = 32;
            this.sepFixAngleButton.Text = "Пофиксить углы";
            this.sepFixAngleButton.UseVisualStyleBackColor = true;
            this.sepFixAngleButton.Click += new System.EventHandler(this.SepFixAngleButton_Click);
            // 
            // sepLsFolderPanel
            // 
            this.sepLsFolderPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.sepLsFolderPanel.Controls.Add(this.sepLsFolderLabel);
            this.sepLsFolderPanel.Controls.Add(this.sepLsFolderOpenButton);
            this.sepLsFolderPanel.Controls.Add(this.sepLsFolderTextBox);
            this.sepLsFolderPanel.Location = new System.Drawing.Point(30, 186);
            this.sepLsFolderPanel.Name = "sepLsFolderPanel";
            this.sepLsFolderPanel.Size = new System.Drawing.Size(510, 61);
            this.sepLsFolderPanel.TabIndex = 31;
            // 
            // sepLsFolderLabel
            // 
            this.sepLsFolderLabel.AutoSize = true;
            this.sepLsFolderLabel.Location = new System.Drawing.Point(30, 23);
            this.sepLsFolderLabel.Name = "sepLsFolderLabel";
            this.sepLsFolderLabel.Size = new System.Drawing.Size(67, 15);
            this.sepLsFolderLabel.TabIndex = 0;
            this.sepLsFolderLabel.Text = "Папка с .ls:";
            // 
            // sepLsFolderOpenButton
            // 
            this.sepLsFolderOpenButton.Location = new System.Drawing.Point(415, 19);
            this.sepLsFolderOpenButton.Name = "sepLsFolderOpenButton";
            this.sepLsFolderOpenButton.Size = new System.Drawing.Size(75, 23);
            this.sepLsFolderOpenButton.TabIndex = 2;
            this.sepLsFolderOpenButton.Text = "Open";
            this.sepLsFolderOpenButton.UseVisualStyleBackColor = true;
            this.sepLsFolderOpenButton.Click += new System.EventHandler(this.SepLsFolderOpenButton_Click);
            // 
            // sepLsFolderTextBox
            // 
            this.sepLsFolderTextBox.Location = new System.Drawing.Point(119, 19);
            this.sepLsFolderTextBox.Name = "sepLsFolderTextBox";
            this.sepLsFolderTextBox.Size = new System.Drawing.Size(274, 23);
            this.sepLsFolderTextBox.TabIndex = 1;
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.parametersToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1124, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // parametersToolStripMenuItem
            // 
            this.parametersToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.checkUpdatesToolStripMenuItem,
            this.settingsToolStripMenuItem});
            this.parametersToolStripMenuItem.Name = "parametersToolStripMenuItem";
            this.parametersToolStripMenuItem.Size = new System.Drawing.Size(83, 20);
            this.parametersToolStripMenuItem.Text = "Параметры";
            // 
            // checkUpdatesToolStripMenuItem
            // 
            this.checkUpdatesToolStripMenuItem.Name = "checkUpdatesToolStripMenuItem";
            this.checkUpdatesToolStripMenuItem.Size = new System.Drawing.Size(204, 22);
            this.checkUpdatesToolStripMenuItem.Text = "Проверить обновления";
            // 
            // settingsToolStripMenuItem
            // 
            this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            this.settingsToolStripMenuItem.Size = new System.Drawing.Size(204, 22);
            this.settingsToolStripMenuItem.Text = "Настройки";
            this.settingsToolStripMenuItem.Click += new System.EventHandler(this.SettingsToolStripMenuItem_Click);
            // 
            // MainWindowForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1124, 631);
            this.Controls.Add(this.mainTabControl);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(1140, 670);
            this.MinimumSize = new System.Drawing.Size(1140, 670);
            this.Name = "MainWindowForm";
            this.Text = "Транслятор";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainWindowForm_FormClosing);
            this.Load += new System.EventHandler(this.MainWindowForm_Load);
            this.mainTabControl.ResumeLayout(false);
            this.convertTabPage.ResumeLayout(false);
            this.convertMainPanel.ResumeLayout(false);
            this.convertMainPanel.PerformLayout();
            this.angleFixGroupBox.ResumeLayout(false);
            this.angleFixGroupBox.PerformLayout();
            this.positionerFrameGroupBox.ResumeLayout(false);
            this.positionerFrameGroupBox.PerformLayout();
            this.layersSplitGroupBox.ResumeLayout(false);
            this.layersSplitGroupBox.PerformLayout();
            this.someParametersGroupBox.ResumeLayout(false);
            this.someParametersGroupBox.PerformLayout();
            this.xzChtoZaPropertiesToolBox.ResumeLayout(false);
            this.xzChtoZaPropertiesToolBox.PerformLayout();
            this.robotFrameGroupBox.ResumeLayout(false);
            this.robotFrameGroupBox.PerformLayout();
            this.codeOffsetGroupBox.ResumeLayout(false);
            this.codeOffsetGroupBox.PerformLayout();
            this.fileSettingsGroupBox.ResumeLayout(false);
            this.gCodeFilenamePanel.ResumeLayout(false);
            this.gCodeFilenamePanel.PerformLayout();
            this.sendToRobotTabPage.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.sendToBoxMainPanel.ResumeLayout(false);
            this.sendToBoxMainPanel.PerformLayout();
            this.robotParamsGroupBox.ResumeLayout(false);
            this.robotParamsGroupBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.robotsTable)).EndInit();
            this.convertToTpTabPage.ResumeLayout(false);
            this.convertToTpMainPanel.ResumeLayout(false);
            this.convertFileGroupBox.ResumeLayout(false);
            this.convertFileGroupBox.PerformLayout();
            this.convertFolderGroupBox.ResumeLayout(false);
            this.convertFolderGroupBox.PerformLayout();
            this.lsFolderPanel.ResumeLayout(false);
            this.lsFolderPanel.PerformLayout();
            this.angleFixTabPage.ResumeLayout(false);
            this.separatedAngleFixPanel.ResumeLayout(false);
            this.separateAngleFixGroupBox.ResumeLayout(false);
            this.separateAngleFixGroupBox.PerformLayout();
            this.sepLsFolderPanel.ResumeLayout(false);
            this.sepLsFolderPanel.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private TabControl mainTabControl;
        private TabPage convertTabPage;
        private TabPage sendToRobotTabPage;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem parametersToolStripMenuItem;
        private ToolStripMenuItem checkUpdatesToolStripMenuItem;
        private ToolStripMenuItem settingsToolStripMenuItem;
        private Panel panel1;
        private Panel sendToBoxMainPanel;
        private DataGridViewTextBoxColumn IDColumn;
        private DataGridViewTextBoxColumn ipAddressColumn;
        private DataGridViewTextBoxColumn currentRobotStateColumn;
        private ComboBox avaiableRobotListComboBox;
        private Label avaibaleRobotListLabel;
        private GroupBox robotParamsGroupBox;
        private CheckBox sendTpFileCheckBox;
        private Button addButton;
        private Label enterRobotAddressLabel;
        private Panel convertMainPanel;
        private Button startButton;
        private GroupBox layersSplitGroupBox;
        private Label splitLayersLabel;
        private GroupBox someParametersGroupBox;
        private Label checkingDistanceLabel;
        private Label weldingMovementLabel;
        private Label normalMovementLabel;
        private GroupBox xzChtoZaPropertiesToolBox;
        private Label turnsJ6Label;
        private Label turnsJ4Label;
        private Label turnsJ1Label;
        private Label baseLabel;
        private Label wristLabel;
        private GroupBox robotFrameGroupBox;
        private Label robotUtLabel;
        private Label robotUfLabel;
        private GroupBox codeOffsetGroupBox;
        private Label zLabel;
        private Label yLabel;
        private Label xLabel;
        private Label rLabel;
        private Label pLabel;
        private Label wLabel;
        private GroupBox fileSettingsGroupBox;
        private Panel gCodeFilenamePanel;
        private Label gCodeFilenameLabel;
        private Button gCodeFilenameButton;
        private GroupBox positionerFrameGroupBox;
        private Label positionerUtLabel;
        private Label positionerUfLabel;
        private Label j2OffsetLabel;
        private Label j1OffsetLabel;
        private DataGridView robotsTable;
        private TextBox robotAddressTextBox;
        private CheckBox laserPassCheckBox;
        private CheckBox autoSplitLayersCheckBox;
        private TextBox splitLayersTextBox;
        private TextBox waveEnableTextBox;
        private TextBox weldShieldTextBox;
        private CheckBox roEnableCheckBox;
        private CheckBox waveEnableCheckBox;
        private CheckBox weldShieldCheckBox;
        private CheckBox removeSmallStopStartCheckBox;
        private TextBox checkingDistanceTextBox;
        private CheckBox useWeldSpeedCheckBox;
        private CheckBox autoArcEnabledByExtrusionCheckBox;
        private CheckBox runWithoutArcCheckBox;
        private TextBox weldingMovementTextBox;
        private TextBox normalMovementTextBox;
        private TextBox turnsJ6TextBox;
        private TextBox turnsJ4TextBox;
        private TextBox turnsJ1TextBox;
        private ComboBox baseComboBox;
        private ComboBox armComboBox;
        private Label armLabel;
        private ComboBox wristComboBox;
        private TextBox robotUtTextBox;
        private TextBox robotUfTextBox;
        private TextBox zTextBox;
        private TextBox yTextBox;
        private TextBox xTextBox;
        private TextBox rTextBox;
        private TextBox pTextBox;
        private TextBox wTextBox;
        private CheckBox angleScriptCheckBox;
        private CheckBox powerMillExportCheckBox;
        private TextBox gCodeFilenameTextBox;
        private TextBox positionerUtTextBox;
        private TextBox positionerUfTextBox;
        private TextBox j2OffsetTextBox;
        private TextBox j1OffsetTextBox;
        private TabPage convertToTpTabPage;
        private Panel convertToTpMainPanel;
        private GroupBox convertFileGroupBox;
        private Label helpPressConvertFileBtnLabel;
        private Button convertFileButton;
        private GroupBox convertFolderGroupBox;
        private Label helpPressConvertFolderBtnLabel;
        private Label helpSelectFolderLabel;
        private Button convertFolderButton;
        private Panel lsFolderPanel;
        private Label lsFolderLabel;
        private Button openLsFolderButton;
        private TextBox selectedLsFolderTextBox;
        private GroupBox angleFixGroupBox;
        private TextBox criticalAngleDifferenceTextBox;
        private Label criticalAngleDifferenceLabel;
        private TextBox maxAngleValueTextBox;
        private Label maxAngleValueLabel;
        private TabPage angleFixTabPage;
        private Panel separatedAngleFixPanel;
        private GroupBox separateAngleFixGroupBox;
        private TextBox sepCriticalAngleDifferenceTextBox;
        private Label sepCriticalAngleDifferenceLabel;
        private TextBox sepMaxValueTextBox;
        private Label sepMaxValueLabel;
        private Button sepFixAngleButton;
        private Panel sepLsFolderPanel;
        private Label sepLsFolderLabel;
        private Button sepLsFolderOpenButton;
        private TextBox sepLsFolderTextBox;
    }
}