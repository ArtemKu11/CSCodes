namespace GCodeTranslator.Forms.SettingsWindow
{
    /*
     * Часть класса, инициализирующая компоненты интерфейса окна "Настройки".
     * Сгенерирована автоматически Windows Form Designer в Visual Studio
     * 
     * Определяет параметры, расположение, листенеры для каждого элемента интерфейса
     * Добавлять дополнительные элементы интерфеса можно через Windows Form Designer в Visual Studio,
     * либо по аналогии с остальными вручную
     */
    partial class SettingsWindowForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SettingsWindowForm));
            this.nextLayerTimerCheckBox = new System.Windows.Forms.CheckBox();
            this.mainSettingsPanel = new System.Windows.Forms.Panel();
            this.maxConnectionTimeTextBox = new System.Windows.Forms.TextBox();
            this.maxConnectionTimeLabel = new System.Windows.Forms.Label();
            this.defaultIpTextBox = new System.Windows.Forms.TextBox();
            this.defaultIpLabel = new System.Windows.Forms.Label();
            this.cancelButton = new System.Windows.Forms.Button();
            this.saveButton = new System.Windows.Forms.Button();
            this.enableLogsCheckBox = new System.Windows.Forms.CheckBox();
            this.cancelTimeTextBox = new System.Windows.Forms.TextBox();
            this.cancelTimeCheckBox = new System.Windows.Forms.CheckBox();
            this.nextLayerTimerTextBox = new System.Windows.Forms.TextBox();
            this.mainSettingsPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // nextLayerTimerCheckBox
            // 
            this.nextLayerTimerCheckBox.AutoSize = true;
            this.nextLayerTimerCheckBox.Location = new System.Drawing.Point(20, 30);
            this.nextLayerTimerCheckBox.Name = "nextLayerTimerCheckBox";
            this.nextLayerTimerCheckBox.Size = new System.Drawing.Size(172, 19);
            this.nextLayerTimerCheckBox.TabIndex = 0;
            this.nextLayerTimerCheckBox.Text = "Таймер следующего слоя:";
            this.nextLayerTimerCheckBox.UseVisualStyleBackColor = true;
            this.nextLayerTimerCheckBox.CheckedChanged += new System.EventHandler(this.nextLayerTimerCheckBox_CheckedChanged);
            // 
            // mainSettingsPanel
            // 
            this.mainSettingsPanel.Controls.Add(this.maxConnectionTimeTextBox);
            this.mainSettingsPanel.Controls.Add(this.maxConnectionTimeLabel);
            this.mainSettingsPanel.Controls.Add(this.defaultIpTextBox);
            this.mainSettingsPanel.Controls.Add(this.defaultIpLabel);
            this.mainSettingsPanel.Controls.Add(this.cancelButton);
            this.mainSettingsPanel.Controls.Add(this.saveButton);
            this.mainSettingsPanel.Controls.Add(this.enableLogsCheckBox);
            this.mainSettingsPanel.Controls.Add(this.cancelTimeTextBox);
            this.mainSettingsPanel.Controls.Add(this.cancelTimeCheckBox);
            this.mainSettingsPanel.Controls.Add(this.nextLayerTimerTextBox);
            this.mainSettingsPanel.Controls.Add(this.nextLayerTimerCheckBox);
            this.mainSettingsPanel.Location = new System.Drawing.Point(12, 12);
            this.mainSettingsPanel.Name = "mainSettingsPanel";
            this.mainSettingsPanel.Size = new System.Drawing.Size(310, 237);
            this.mainSettingsPanel.TabIndex = 1;
            // 
            // maxConnectionTimeTextBox
            // 
            this.maxConnectionTimeTextBox.Location = new System.Drawing.Point(234, 113);
            this.maxConnectionTimeTextBox.Name = "maxConnectionTimeTextBox";
            this.maxConnectionTimeTextBox.Size = new System.Drawing.Size(56, 23);
            this.maxConnectionTimeTextBox.TabIndex = 11;
            this.maxConnectionTimeTextBox.TextChanged += new System.EventHandler(this.maxConnectionTimeTextBox_TextChanged);
            // 
            // maxConnectionTimeLabel
            // 
            this.maxConnectionTimeLabel.AutoSize = true;
            this.maxConnectionTimeLabel.Location = new System.Drawing.Point(20, 116);
            this.maxConnectionTimeLabel.Name = "maxConnectionTimeLabel";
            this.maxConnectionTimeLabel.Size = new System.Drawing.Size(208, 15);
            this.maxConnectionTimeLabel.TabIndex = 10;
            this.maxConnectionTimeLabel.Text = "Макс. время ожид. соединения (сек)";
            // 
            // defaultIpTextBox
            // 
            this.defaultIpTextBox.Location = new System.Drawing.Point(133, 84);
            this.defaultIpTextBox.Name = "defaultIpTextBox";
            this.defaultIpTextBox.Size = new System.Drawing.Size(157, 23);
            this.defaultIpTextBox.TabIndex = 9;
            this.defaultIpTextBox.TextChanged += new System.EventHandler(this.defaultIpTextBox_TextChanged);
            // 
            // defaultIpLabel
            // 
            this.defaultIpLabel.AutoSize = true;
            this.defaultIpLabel.Location = new System.Drawing.Point(20, 87);
            this.defaultIpLabel.Name = "defaultIpLabel";
            this.defaultIpLabel.Size = new System.Drawing.Size(107, 15);
            this.defaultIpLabel.TabIndex = 8;
            this.defaultIpLabel.Text = "Дефолт IP робота:";
            // 
            // cancelButton
            // 
            this.cancelButton.Enabled = false;
            this.cancelButton.Location = new System.Drawing.Point(166, 189);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 6;
            this.cancelButton.Text = "Отмена";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.ResetButton_Click);
            // 
            // saveButton
            // 
            this.saveButton.Enabled = false;
            this.saveButton.Location = new System.Drawing.Point(69, 189);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(75, 23);
            this.saveButton.TabIndex = 5;
            this.saveButton.Text = "Сохранить";
            this.saveButton.UseVisualStyleBackColor = true;
            this.saveButton.Click += new System.EventHandler(this.SaveButton_Click);
            // 
            // enableLogsCheckBox
            // 
            this.enableLogsCheckBox.AutoSize = true;
            this.enableLogsCheckBox.Location = new System.Drawing.Point(20, 142);
            this.enableLogsCheckBox.Name = "enableLogsCheckBox";
            this.enableLogsCheckBox.Size = new System.Drawing.Size(110, 19);
            this.enableLogsCheckBox.TabIndex = 4;
            this.enableLogsCheckBox.Text = "Включить логи";
            this.enableLogsCheckBox.UseVisualStyleBackColor = true;
            this.enableLogsCheckBox.CheckedChanged += new System.EventHandler(this.enableLogsCheckBox_CheckedChanged);
            // 
            // cancelTimeTextBox
            // 
            this.cancelTimeTextBox.Location = new System.Drawing.Point(244, 55);
            this.cancelTimeTextBox.Name = "cancelTimeTextBox";
            this.cancelTimeTextBox.Size = new System.Drawing.Size(46, 23);
            this.cancelTimeTextBox.TabIndex = 3;
            this.cancelTimeTextBox.Text = "5";
            this.cancelTimeTextBox.TextChanged += new System.EventHandler(this.cancelTimeTextBox_TextChanged);
            // 
            // cancelTimeCheckBox
            // 
            this.cancelTimeCheckBox.AutoSize = true;
            this.cancelTimeCheckBox.Location = new System.Drawing.Point(20, 55);
            this.cancelTimeCheckBox.Name = "cancelTimeCheckBox";
            this.cancelTimeCheckBox.Size = new System.Drawing.Size(202, 19);
            this.cancelTimeCheckBox.TabIndex = 2;
            this.cancelTimeCheckBox.Text = "Время отмены нажатия кнопки:";
            this.cancelTimeCheckBox.UseVisualStyleBackColor = true;
            this.cancelTimeCheckBox.CheckedChanged += new System.EventHandler(this.cancelTimeCheckBox_CheckedChanged);
            // 
            // nextLayerTimerTextBox
            // 
            this.nextLayerTimerTextBox.Location = new System.Drawing.Point(244, 26);
            this.nextLayerTimerTextBox.Name = "nextLayerTimerTextBox";
            this.nextLayerTimerTextBox.Size = new System.Drawing.Size(46, 23);
            this.nextLayerTimerTextBox.TabIndex = 1;
            this.nextLayerTimerTextBox.Text = "60";
            this.nextLayerTimerTextBox.TextChanged += new System.EventHandler(this.nextLayerTimerTextBox_TextChanged);
            // 
            // SettingsWindowForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(334, 261);
            this.Controls.Add(this.mainSettingsPanel);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(350, 300);
            this.MinimumSize = new System.Drawing.Size(350, 300);
            this.Name = "SettingsWindowForm";
            this.Text = "Настройки";
            this.mainSettingsPanel.ResumeLayout(false);
            this.mainSettingsPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Panel mainSettingsPanel;
        private Button cancelButton;
        private Button saveButton;
        private Label maxConnectionTimeLabel;
        private Label defaultIpLabel;
        private CheckBox nextLayerTimerCheckBox;
        private CheckBox cancelTimeCheckBox;
        private TextBox nextLayerTimerTextBox;
        private TextBox cancelTimeTextBox;
        private CheckBox enableLogsCheckBox;
        private TextBox maxConnectionTimeTextBox;
        private TextBox defaultIpTextBox;
    }
}