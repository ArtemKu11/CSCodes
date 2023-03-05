namespace GCodeTranslator.Forms.RobotConnectionWindow
{
    /*
     * Часть класса, инициализирующая компоненты интерфейса окна "Соединение с роботом".
     * Сгенерирована автоматически Windows Form Designer в Visual Studio
     * 
     * Определяет параметры, расположение, листенеры для каждого элемента интерфейса
     * Добавлять дополнительные элементы интерфеса можно через Windows Form Designer в Visual Studio,
     * либо по аналогии с остальными вручную
     */
    sealed partial class RobotConnectionForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RobotConnectionForm));
            this.mainPanel = new System.Windows.Forms.Panel();
            this.mainContentConnectionToRobotPanel = new System.Windows.Forms.Panel();
            this.refreshStateButton = new System.Windows.Forms.Button();
            this.printInfoTextBox = new System.Windows.Forms.TextBox();
            this.resetButton = new System.Windows.Forms.Button();
            this.exportOneCheckBox = new System.Windows.Forms.CheckBox();
            this.exportToTpButton = new System.Windows.Forms.Button();
            this.startPrintingButton = new System.Windows.Forms.Button();
            this.prevButton = new System.Windows.Forms.Button();
            this.nextButton = new System.Windows.Forms.Button();
            this.repeatButton = new System.Windows.Forms.Button();
            this.awaitLayerCheckBox = new System.Windows.Forms.CheckBox();
            this.layersComboBox = new System.Windows.Forms.ComboBox();
            this.browseFolderButton = new System.Windows.Forms.Button();
            this.folderTextBox = new System.Windows.Forms.TextBox();
            this.robotStateLabel = new System.Windows.Forms.Label();
            this.mainPanel.SuspendLayout();
            this.mainContentConnectionToRobotPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainPanel
            // 
            this.mainPanel.Controls.Add(this.mainContentConnectionToRobotPanel);
            this.mainPanel.Location = new System.Drawing.Point(12, 12);
            this.mainPanel.Name = "mainPanel";
            this.mainPanel.Size = new System.Drawing.Size(722, 292);
            this.mainPanel.TabIndex = 0;
            // 
            // mainContentConnectionToRobotPanel
            // 
            this.mainContentConnectionToRobotPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.mainContentConnectionToRobotPanel.Controls.Add(this.refreshStateButton);
            this.mainContentConnectionToRobotPanel.Controls.Add(this.printInfoTextBox);
            this.mainContentConnectionToRobotPanel.Controls.Add(this.resetButton);
            this.mainContentConnectionToRobotPanel.Controls.Add(this.exportOneCheckBox);
            this.mainContentConnectionToRobotPanel.Controls.Add(this.exportToTpButton);
            this.mainContentConnectionToRobotPanel.Controls.Add(this.startPrintingButton);
            this.mainContentConnectionToRobotPanel.Controls.Add(this.prevButton);
            this.mainContentConnectionToRobotPanel.Controls.Add(this.nextButton);
            this.mainContentConnectionToRobotPanel.Controls.Add(this.repeatButton);
            this.mainContentConnectionToRobotPanel.Controls.Add(this.awaitLayerCheckBox);
            this.mainContentConnectionToRobotPanel.Controls.Add(this.layersComboBox);
            this.mainContentConnectionToRobotPanel.Controls.Add(this.browseFolderButton);
            this.mainContentConnectionToRobotPanel.Controls.Add(this.folderTextBox);
            this.mainContentConnectionToRobotPanel.Controls.Add(this.robotStateLabel);
            this.mainContentConnectionToRobotPanel.Location = new System.Drawing.Point(0, 0);
            this.mainContentConnectionToRobotPanel.Name = "mainContentConnectionToRobotPanel";
            this.mainContentConnectionToRobotPanel.Size = new System.Drawing.Size(722, 292);
            this.mainContentConnectionToRobotPanel.TabIndex = 2;
            // 
            // refreshStateButton
            // 
            this.refreshStateButton.Location = new System.Drawing.Point(242, 26);
            this.refreshStateButton.Name = "refreshStateButton";
            this.refreshStateButton.Size = new System.Drawing.Size(100, 23);
            this.refreshStateButton.TabIndex = 13;
            this.refreshStateButton.Text = "Обновить";
            this.refreshStateButton.UseVisualStyleBackColor = true;
            this.refreshStateButton.Click += new System.EventHandler(this.refreshStateButton_Click);
            // 
            // printInfoTextBox
            // 
            this.printInfoTextBox.Enabled = false;
            this.printInfoTextBox.Location = new System.Drawing.Point(392, 30);
            this.printInfoTextBox.Multiline = true;
            this.printInfoTextBox.Name = "printInfoTextBox";
            this.printInfoTextBox.ReadOnly = true;
            this.printInfoTextBox.Size = new System.Drawing.Size(302, 235);
            this.printInfoTextBox.TabIndex = 12;
            // 
            // resetButton
            // 
            this.resetButton.Location = new System.Drawing.Point(30, 242);
            this.resetButton.Name = "resetButton";
            this.resetButton.Size = new System.Drawing.Size(312, 23);
            this.resetButton.TabIndex = 11;
            this.resetButton.Text = "Сброс";
            this.resetButton.UseVisualStyleBackColor = true;
            this.resetButton.Click += new System.EventHandler(this.ResetButton_Click);
            // 
            // exportOneCheckBox
            // 
            this.exportOneCheckBox.AutoSize = true;
            this.exportOneCheckBox.Location = new System.Drawing.Point(197, 211);
            this.exportOneCheckBox.Name = "exportOneCheckBox";
            this.exportOneCheckBox.Size = new System.Drawing.Size(145, 19);
            this.exportOneCheckBox.TabIndex = 10;
            this.exportOneCheckBox.Text = "Экспортировать один";
            this.exportOneCheckBox.UseVisualStyleBackColor = true;
            // 
            // exportToTpButton
            // 
            this.exportToTpButton.Location = new System.Drawing.Point(30, 208);
            this.exportToTpButton.Name = "exportToTpButton";
            this.exportToTpButton.Size = new System.Drawing.Size(135, 23);
            this.exportToTpButton.TabIndex = 9;
            this.exportToTpButton.Text = "Экспорт в TP";
            this.exportToTpButton.UseVisualStyleBackColor = true;
            this.exportToTpButton.Click += new System.EventHandler(this.ExportToTpButton_Click);
            // 
            // startPrintingButton
            // 
            this.startPrintingButton.Enabled = false;
            this.startPrintingButton.Location = new System.Drawing.Point(30, 179);
            this.startPrintingButton.Name = "startPrintingButton";
            this.startPrintingButton.Size = new System.Drawing.Size(135, 23);
            this.startPrintingButton.TabIndex = 8;
            this.startPrintingButton.Text = "Начать печать";
            this.startPrintingButton.UseVisualStyleBackColor = true;
            this.startPrintingButton.Click += new System.EventHandler(this.StartPrintingButton_Click);
            // 
            // prevButton
            // 
            this.prevButton.Location = new System.Drawing.Point(30, 126);
            this.prevButton.Name = "prevButton";
            this.prevButton.Size = new System.Drawing.Size(35, 23);
            this.prevButton.TabIndex = 7;
            this.prevButton.Text = "<<";
            this.prevButton.UseVisualStyleBackColor = true;
            this.prevButton.Click += new System.EventHandler(this.PrevButton_Click);
            // 
            // nextButton
            // 
            this.nextButton.Location = new System.Drawing.Point(184, 126);
            this.nextButton.Name = "nextButton";
            this.nextButton.Size = new System.Drawing.Size(35, 23);
            this.nextButton.TabIndex = 6;
            this.nextButton.Text = ">>";
            this.nextButton.UseVisualStyleBackColor = true;
            this.nextButton.Click += new System.EventHandler(this.NextButton_Click);
            // 
            // repeatButton
            // 
            this.repeatButton.Location = new System.Drawing.Point(87, 126);
            this.repeatButton.Name = "repeatButton";
            this.repeatButton.Size = new System.Drawing.Size(75, 23);
            this.repeatButton.TabIndex = 5;
            this.repeatButton.Text = "Повтор";
            this.repeatButton.UseVisualStyleBackColor = true;
            this.repeatButton.Click += new System.EventHandler(this.RepeatButton_Click);
            // 
            // awaitLayerCheckBox
            // 
            this.awaitLayerCheckBox.AutoSize = true;
            this.awaitLayerCheckBox.Location = new System.Drawing.Point(242, 95);
            this.awaitLayerCheckBox.Name = "awaitLayerCheckBox";
            this.awaitLayerCheckBox.Size = new System.Drawing.Size(84, 19);
            this.awaitLayerCheckBox.TabIndex = 4;
            this.awaitLayerCheckBox.Text = "Await layer";
            this.awaitLayerCheckBox.UseVisualStyleBackColor = true;
            // 
            // layersComboBox
            // 
            this.layersComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.layersComboBox.FormattingEnabled = true;
            this.layersComboBox.Location = new System.Drawing.Point(30, 93);
            this.layersComboBox.Name = "layersComboBox";
            this.layersComboBox.Size = new System.Drawing.Size(189, 23);
            this.layersComboBox.TabIndex = 3;
            // 
            // browseFolderButton
            // 
            this.browseFolderButton.Location = new System.Drawing.Point(242, 60);
            this.browseFolderButton.Name = "browseFolderButton";
            this.browseFolderButton.Size = new System.Drawing.Size(100, 23);
            this.browseFolderButton.TabIndex = 2;
            this.browseFolderButton.Text = "Выбрать папку";
            this.browseFolderButton.UseVisualStyleBackColor = true;
            this.browseFolderButton.Click += new System.EventHandler(this.BrowseFolderButton_Click);
            // 
            // folderTextBox
            // 
            this.folderTextBox.Location = new System.Drawing.Point(30, 60);
            this.folderTextBox.Name = "folderTextBox";
            this.folderTextBox.ReadOnly = true;
            this.folderTextBox.Size = new System.Drawing.Size(189, 23);
            this.folderTextBox.TabIndex = 1;
            // 
            // robotStateLabel
            // 
            this.robotStateLabel.AutoSize = true;
            this.robotStateLabel.Location = new System.Drawing.Point(30, 30);
            this.robotStateLabel.Name = "robotStateLabel";
            this.robotStateLabel.Size = new System.Drawing.Size(68, 15);
            this.robotStateLabel.TabIndex = 0;
            this.robotStateLabel.Text = "Robot State";
            // 
            // RobotConnectionForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(746, 316);
            this.Controls.Add(this.mainPanel);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(762, 355);
            this.Name = "RobotConnectionForm";
            this.Text = "Соединение с роботом";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.RobotConnectionForm_FormClosing);
            this.Resize += new System.EventHandler(this.RobotConnectionForm_Resize);
            this.mainPanel.ResumeLayout(false);
            this.mainContentConnectionToRobotPanel.ResumeLayout(false);
            this.mainContentConnectionToRobotPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Panel mainPanel;
        private Panel mainContentConnectionToRobotPanel;
        private TextBox printInfoTextBox;
        private Button resetButton;
        private CheckBox exportOneCheckBox;
        private Button exportToTpButton;
        private Button startPrintingButton;
        private Button prevButton;
        private Button nextButton;
        private Button repeatButton;
        private CheckBox awaitLayerCheckBox;
        private ComboBox layersComboBox;
        private Button browseFolderButton;
        private TextBox folderTextBox;
        private Label robotStateLabel;
        private Button refreshStateButton;
    }
}