namespace GCodeTranslator.Utils.DebugUtils.DebugWindow
{
    partial class DebugWindowForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DebugWindowForm));
            this.mainTabControl = new System.Windows.Forms.TabControl();
            this.connectionEmulationTabPage = new System.Windows.Forms.TabPage();
            this.zLabel = new System.Windows.Forms.Label();
            this.stateLabel = new System.Windows.Forms.Label();
            this.zTextBox = new System.Windows.Forms.TextBox();
            this.fileRequiredButton = new System.Windows.Forms.Button();
            this.printButton = new System.Windows.Forms.Button();
            this.readyButton = new System.Windows.Forms.Button();
            this.timeOutExceptionButton = new System.Windows.Forms.Button();
            this.applyValueButton = new System.Windows.Forms.Button();
            this.stateTextBox = new System.Windows.Forms.TextBox();
            this.valueHolderLabel = new System.Windows.Forms.Label();
            this.valueRequiredCheckBox = new System.Windows.Forms.CheckBox();
            this.messageBoxRequiredCheckBox = new System.Windows.Forms.CheckBox();
            this.statusHolderLabel = new System.Windows.Forms.Label();
            this.statusLabel = new System.Windows.Forms.Label();
            this.realConnectionTabPage = new System.Windows.Forms.TabPage();
            this.saveButton = new System.Windows.Forms.Button();
            this.messageHolderLabel = new System.Windows.Forms.Label();
            this.resultHolderLabel = new System.Windows.Forms.Label();
            this.messageLabel = new System.Windows.Forms.Label();
            this.resultLabel = new System.Windows.Forms.Label();
            this.formNotRequiredCheckConnectionButton = new System.Windows.Forms.Button();
            this.formRequiredCheckConnectionButton = new System.Windows.Forms.Button();
            this.timeOutTextBox = new System.Windows.Forms.TextBox();
            this.timeOutLabel = new System.Windows.Forms.Label();
            this.ipAddressTextBox = new System.Windows.Forms.TextBox();
            this.ipAddressLabel = new System.Windows.Forms.Label();
            this.mainTabControl.SuspendLayout();
            this.connectionEmulationTabPage.SuspendLayout();
            this.realConnectionTabPage.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainTabControl
            // 
            this.mainTabControl.Controls.Add(this.connectionEmulationTabPage);
            this.mainTabControl.Controls.Add(this.realConnectionTabPage);
            this.mainTabControl.Location = new System.Drawing.Point(12, 12);
            this.mainTabControl.Name = "mainTabControl";
            this.mainTabControl.SelectedIndex = 0;
            this.mainTabControl.Size = new System.Drawing.Size(384, 330);
            this.mainTabControl.TabIndex = 0;
            // 
            // connectionEmulationTabPage
            // 
            this.connectionEmulationTabPage.Controls.Add(this.zLabel);
            this.connectionEmulationTabPage.Controls.Add(this.stateLabel);
            this.connectionEmulationTabPage.Controls.Add(this.zTextBox);
            this.connectionEmulationTabPage.Controls.Add(this.fileRequiredButton);
            this.connectionEmulationTabPage.Controls.Add(this.printButton);
            this.connectionEmulationTabPage.Controls.Add(this.readyButton);
            this.connectionEmulationTabPage.Controls.Add(this.timeOutExceptionButton);
            this.connectionEmulationTabPage.Controls.Add(this.applyValueButton);
            this.connectionEmulationTabPage.Controls.Add(this.stateTextBox);
            this.connectionEmulationTabPage.Controls.Add(this.valueHolderLabel);
            this.connectionEmulationTabPage.Controls.Add(this.valueRequiredCheckBox);
            this.connectionEmulationTabPage.Controls.Add(this.messageBoxRequiredCheckBox);
            this.connectionEmulationTabPage.Controls.Add(this.statusHolderLabel);
            this.connectionEmulationTabPage.Controls.Add(this.statusLabel);
            this.connectionEmulationTabPage.Location = new System.Drawing.Point(4, 24);
            this.connectionEmulationTabPage.Name = "connectionEmulationTabPage";
            this.connectionEmulationTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.connectionEmulationTabPage.Size = new System.Drawing.Size(376, 302);
            this.connectionEmulationTabPage.TabIndex = 0;
            this.connectionEmulationTabPage.Text = "Эмуляция соединения";
            this.connectionEmulationTabPage.UseVisualStyleBackColor = true;
            // 
            // zLabel
            // 
            this.zLabel.AutoSize = true;
            this.zLabel.Location = new System.Drawing.Point(135, 142);
            this.zLabel.Name = "zLabel";
            this.zLabel.Size = new System.Drawing.Size(15, 15);
            this.zLabel.TabIndex = 13;
            this.zLabel.Text = "z:";
            // 
            // stateLabel
            // 
            this.stateLabel.AutoSize = true;
            this.stateLabel.Location = new System.Drawing.Point(30, 142);
            this.stateLabel.Name = "stateLabel";
            this.stateLabel.Size = new System.Drawing.Size(35, 15);
            this.stateLabel.TabIndex = 12;
            this.stateLabel.Text = "state:";
            // 
            // zTextBox
            // 
            this.zTextBox.Location = new System.Drawing.Point(156, 139);
            this.zTextBox.Name = "zTextBox";
            this.zTextBox.Size = new System.Drawing.Size(46, 23);
            this.zTextBox.TabIndex = 11;
            this.zTextBox.Text = "0";
            // 
            // fileRequiredButton
            // 
            this.fileRequiredButton.Location = new System.Drawing.Point(30, 266);
            this.fileRequiredButton.Name = "fileRequiredButton";
            this.fileRequiredButton.Size = new System.Drawing.Size(130, 23);
            this.fileRequiredButton.TabIndex = 10;
            this.fileRequiredButton.Text = "2: Необходим файл";
            this.fileRequiredButton.UseVisualStyleBackColor = true;
            this.fileRequiredButton.Click += new System.EventHandler(this.fileRequiredButton_Click);
            // 
            // printButton
            // 
            this.printButton.Location = new System.Drawing.Point(30, 237);
            this.printButton.Name = "printButton";
            this.printButton.Size = new System.Drawing.Size(130, 23);
            this.printButton.TabIndex = 9;
            this.printButton.Text = "1: Печать";
            this.printButton.UseVisualStyleBackColor = true;
            this.printButton.Click += new System.EventHandler(this.printButton_Click);
            // 
            // readyButton
            // 
            this.readyButton.Location = new System.Drawing.Point(30, 208);
            this.readyButton.Name = "readyButton";
            this.readyButton.Size = new System.Drawing.Size(130, 23);
            this.readyButton.TabIndex = 8;
            this.readyButton.Text = "0: Готов";
            this.readyButton.UseVisualStyleBackColor = true;
            this.readyButton.Click += new System.EventHandler(this.readyButton_Click);
            // 
            // timeOutExceptionButton
            // 
            this.timeOutExceptionButton.Location = new System.Drawing.Point(30, 179);
            this.timeOutExceptionButton.Name = "timeOutExceptionButton";
            this.timeOutExceptionButton.Size = new System.Drawing.Size(130, 23);
            this.timeOutExceptionButton.TabIndex = 7;
            this.timeOutExceptionButton.Text = "-1: TimeOutException";
            this.timeOutExceptionButton.UseVisualStyleBackColor = true;
            this.timeOutExceptionButton.Click += new System.EventHandler(this.timeOutExceptionButton_Click);
            // 
            // applyValueButton
            // 
            this.applyValueButton.Location = new System.Drawing.Point(220, 139);
            this.applyValueButton.Name = "applyValueButton";
            this.applyValueButton.Size = new System.Drawing.Size(96, 23);
            this.applyValueButton.TabIndex = 6;
            this.applyValueButton.Text = "Использовать";
            this.applyValueButton.UseVisualStyleBackColor = true;
            this.applyValueButton.Click += new System.EventHandler(this.applyValueButton_Click);
            // 
            // stateTextBox
            // 
            this.stateTextBox.Location = new System.Drawing.Point(71, 138);
            this.stateTextBox.Name = "stateTextBox";
            this.stateTextBox.Size = new System.Drawing.Size(46, 23);
            this.stateTextBox.TabIndex = 5;
            this.stateTextBox.Text = "1";
            // 
            // valueHolderLabel
            // 
            this.valueHolderLabel.AutoSize = true;
            this.valueHolderLabel.Location = new System.Drawing.Point(195, 100);
            this.valueHolderLabel.Name = "valueHolderLabel";
            this.valueHolderLabel.Size = new System.Drawing.Size(67, 15);
            this.valueHolderLabel.TabIndex = 4;
            this.valueHolderLabel.Text = "state: 1, z: 0";
            // 
            // valueRequiredCheckBox
            // 
            this.valueRequiredCheckBox.AutoSize = true;
            this.valueRequiredCheckBox.Checked = true;
            this.valueRequiredCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.valueRequiredCheckBox.Location = new System.Drawing.Point(30, 99);
            this.valueRequiredCheckBox.Name = "valueRequiredCheckBox";
            this.valueRequiredCheckBox.Size = new System.Drawing.Size(160, 19);
            this.valueRequiredCheckBox.TabIndex = 3;
            this.valueRequiredCheckBox.Text = "Использовать значение:";
            this.valueRequiredCheckBox.UseVisualStyleBackColor = true;
            this.valueRequiredCheckBox.CheckedChanged += new System.EventHandler(this.valueRequiredCheckBox_CheckedChanged);
            // 
            // messageBoxRequiredCheckBox
            // 
            this.messageBoxRequiredCheckBox.AutoSize = true;
            this.messageBoxRequiredCheckBox.Location = new System.Drawing.Point(30, 62);
            this.messageBoxRequiredCheckBox.Name = "messageBoxRequiredCheckBox";
            this.messageBoxRequiredCheckBox.Size = new System.Drawing.Size(169, 19);
            this.messageBoxRequiredCheckBox.TabIndex = 2;
            this.messageBoxRequiredCheckBox.Text = "Запрашивать MessageBox";
            this.messageBoxRequiredCheckBox.UseVisualStyleBackColor = true;
            this.messageBoxRequiredCheckBox.CheckedChanged += new System.EventHandler(this.messageBoxRequiredCheckBox_CheckedChanged);
            // 
            // statusHolderLabel
            // 
            this.statusHolderLabel.AutoSize = true;
            this.statusHolderLabel.Location = new System.Drawing.Point(82, 30);
            this.statusHolderLabel.Name = "statusHolderLabel";
            this.statusHolderLabel.Size = new System.Drawing.Size(180, 15);
            this.statusHolderLabel.TabIndex = 1;
            this.statusHolderLabel.Text = "Соединение не запрашивалось";
            // 
            // statusLabel
            // 
            this.statusLabel.AutoSize = true;
            this.statusLabel.Location = new System.Drawing.Point(30, 30);
            this.statusLabel.Name = "statusLabel";
            this.statusLabel.Size = new System.Drawing.Size(46, 15);
            this.statusLabel.TabIndex = 0;
            this.statusLabel.Text = "Статус:";
            // 
            // realConnectionTabPage
            // 
            this.realConnectionTabPage.Controls.Add(this.saveButton);
            this.realConnectionTabPage.Controls.Add(this.messageHolderLabel);
            this.realConnectionTabPage.Controls.Add(this.resultHolderLabel);
            this.realConnectionTabPage.Controls.Add(this.messageLabel);
            this.realConnectionTabPage.Controls.Add(this.resultLabel);
            this.realConnectionTabPage.Controls.Add(this.formNotRequiredCheckConnectionButton);
            this.realConnectionTabPage.Controls.Add(this.formRequiredCheckConnectionButton);
            this.realConnectionTabPage.Controls.Add(this.timeOutTextBox);
            this.realConnectionTabPage.Controls.Add(this.timeOutLabel);
            this.realConnectionTabPage.Controls.Add(this.ipAddressTextBox);
            this.realConnectionTabPage.Controls.Add(this.ipAddressLabel);
            this.realConnectionTabPage.Location = new System.Drawing.Point(4, 24);
            this.realConnectionTabPage.Name = "realConnectionTabPage";
            this.realConnectionTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.realConnectionTabPage.Size = new System.Drawing.Size(376, 302);
            this.realConnectionTabPage.TabIndex = 1;
            this.realConnectionTabPage.Text = "Реальное соединение";
            this.realConnectionTabPage.UseVisualStyleBackColor = true;
            // 
            // saveButton
            // 
            this.saveButton.Location = new System.Drawing.Point(120, 99);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(75, 23);
            this.saveButton.TabIndex = 10;
            this.saveButton.Text = "Сохранить";
            this.saveButton.UseVisualStyleBackColor = true;
            this.saveButton.Click += new System.EventHandler(this.saveButton_Click);
            // 
            // messageHolderLabel
            // 
            this.messageHolderLabel.AutoSize = true;
            this.messageHolderLabel.Location = new System.Drawing.Point(120, 157);
            this.messageHolderLabel.Name = "messageHolderLabel";
            this.messageHolderLabel.Size = new System.Drawing.Size(133, 15);
            this.messageHolderLabel.TabIndex = 9;
            this.messageHolderLabel.Text = "сообщение от сервера";
            // 
            // resultHolderLabel
            // 
            this.resultHolderLabel.AutoSize = true;
            this.resultHolderLabel.Location = new System.Drawing.Point(120, 135);
            this.resultHolderLabel.Name = "resultHolderLabel";
            this.resultHolderLabel.Size = new System.Drawing.Size(58, 15);
            this.resultHolderLabel.TabIndex = 8;
            this.resultHolderLabel.Text = "state:  , z: ";
            // 
            // messageLabel
            // 
            this.messageLabel.AutoSize = true;
            this.messageLabel.Location = new System.Drawing.Point(30, 157);
            this.messageLabel.Name = "messageLabel";
            this.messageLabel.Size = new System.Drawing.Size(76, 15);
            this.messageLabel.TabIndex = 7;
            this.messageLabel.Text = "Сообщение:";
            // 
            // resultLabel
            // 
            this.resultLabel.AutoSize = true;
            this.resultLabel.Location = new System.Drawing.Point(30, 135);
            this.resultLabel.Name = "resultLabel";
            this.resultLabel.Size = new System.Drawing.Size(63, 15);
            this.resultLabel.TabIndex = 6;
            this.resultLabel.Text = "Результат:";
            // 
            // formNotRequiredCheckConnectionButton
            // 
            this.formNotRequiredCheckConnectionButton.Location = new System.Drawing.Point(191, 185);
            this.formNotRequiredCheckConnectionButton.Name = "formNotRequiredCheckConnectionButton";
            this.formNotRequiredCheckConnectionButton.Size = new System.Drawing.Size(155, 23);
            this.formNotRequiredCheckConnectionButton.TabIndex = 5;
            this.formNotRequiredCheckConnectionButton.Text = "Запросить без формы";
            this.formNotRequiredCheckConnectionButton.UseVisualStyleBackColor = true;
            this.formNotRequiredCheckConnectionButton.Click += new System.EventHandler(this.formNotRequiredCheckConnectionButton_Click);
            // 
            // formRequiredCheckConnectionButton
            // 
            this.formRequiredCheckConnectionButton.Location = new System.Drawing.Point(30, 185);
            this.formRequiredCheckConnectionButton.Name = "formRequiredCheckConnectionButton";
            this.formRequiredCheckConnectionButton.Size = new System.Drawing.Size(155, 23);
            this.formRequiredCheckConnectionButton.TabIndex = 4;
            this.formRequiredCheckConnectionButton.Text = "Запросить с формой";
            this.formRequiredCheckConnectionButton.UseVisualStyleBackColor = true;
            this.formRequiredCheckConnectionButton.Click += new System.EventHandler(this.formRequiredCheckConnectionButton_Click);
            // 
            // timeOutTextBox
            // 
            this.timeOutTextBox.Location = new System.Drawing.Point(120, 56);
            this.timeOutTextBox.Name = "timeOutTextBox";
            this.timeOutTextBox.Size = new System.Drawing.Size(146, 23);
            this.timeOutTextBox.TabIndex = 3;
            // 
            // timeOutLabel
            // 
            this.timeOutLabel.AutoSize = true;
            this.timeOutLabel.Location = new System.Drawing.Point(30, 59);
            this.timeOutLabel.Name = "timeOutLabel";
            this.timeOutLabel.Size = new System.Drawing.Size(72, 15);
            this.timeOutLabel.TabIndex = 2;
            this.timeOutLabel.Text = "Таймаут (с):";
            // 
            // ipAddressTextBox
            // 
            this.ipAddressTextBox.Location = new System.Drawing.Point(120, 27);
            this.ipAddressTextBox.Name = "ipAddressTextBox";
            this.ipAddressTextBox.Size = new System.Drawing.Size(146, 23);
            this.ipAddressTextBox.TabIndex = 1;
            // 
            // ipAddressLabel
            // 
            this.ipAddressLabel.AutoSize = true;
            this.ipAddressLabel.Location = new System.Drawing.Point(30, 30);
            this.ipAddressLabel.Name = "ipAddressLabel";
            this.ipAddressLabel.Size = new System.Drawing.Size(65, 15);
            this.ipAddressLabel.TabIndex = 0;
            this.ipAddressLabel.Text = "IP Address:";
            // 
            // DebugWindowForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(408, 354);
            this.Controls.Add(this.mainTabControl);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(424, 393);
            this.MinimumSize = new System.Drawing.Size(424, 393);
            this.Name = "DebugWindowForm";
            this.Text = "DebugForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.DebugWindowForm_FormClosing);
            this.mainTabControl.ResumeLayout(false);
            this.connectionEmulationTabPage.ResumeLayout(false);
            this.connectionEmulationTabPage.PerformLayout();
            this.realConnectionTabPage.ResumeLayout(false);
            this.realConnectionTabPage.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private TabControl mainTabControl;
        private TabPage connectionEmulationTabPage;
        private Button applyValueButton;
        private TextBox stateTextBox;
        private Label valueHolderLabel;
        private CheckBox valueRequiredCheckBox;
        private CheckBox messageBoxRequiredCheckBox;
        private Label statusHolderLabel;
        private Label statusLabel;
        private TabPage realConnectionTabPage;
        private Label messageHolderLabel;
        private Label resultHolderLabel;
        private Label messageLabel;
        private Label resultLabel;
        private Button formNotRequiredCheckConnectionButton;
        private Button formRequiredCheckConnectionButton;
        private TextBox timeOutTextBox;
        private Label timeOutLabel;
        private TextBox ipAddressTextBox;
        private Label ipAddressLabel;
        private Label zLabel;
        private Label stateLabel;
        private TextBox zTextBox;
        private Button fileRequiredButton;
        private Button printButton;
        private Button readyButton;
        private Button timeOutExceptionButton;
        private Button saveButton;
    }
}