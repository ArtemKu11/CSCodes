namespace GCodeTranslator.Utils.DebugUtils.MessageBoxWithTextBox
{
    partial class TextMessageBox
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
            this.mainPanel = new System.Windows.Forms.Panel();
            this.stateLabel = new System.Windows.Forms.Label();
            this.zLabel = new System.Windows.Forms.Label();
            this.stateTextBox = new System.Windows.Forms.TextBox();
            this.zTextBox = new System.Windows.Forms.TextBox();
            this.okButton = new System.Windows.Forms.Button();
            this.mainPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainPanel
            // 
            this.mainPanel.Controls.Add(this.okButton);
            this.mainPanel.Controls.Add(this.zTextBox);
            this.mainPanel.Controls.Add(this.stateTextBox);
            this.mainPanel.Controls.Add(this.zLabel);
            this.mainPanel.Controls.Add(this.stateLabel);
            this.mainPanel.Location = new System.Drawing.Point(12, 12);
            this.mainPanel.Name = "mainPanel";
            this.mainPanel.Size = new System.Drawing.Size(249, 148);
            this.mainPanel.TabIndex = 0;
            // 
            // stateLabel
            // 
            this.stateLabel.AutoSize = true;
            this.stateLabel.Location = new System.Drawing.Point(54, 29);
            this.stateLabel.Name = "stateLabel";
            this.stateLabel.Size = new System.Drawing.Size(35, 15);
            this.stateLabel.TabIndex = 0;
            this.stateLabel.Text = "state:";
            // 
            // zLabel
            // 
            this.zLabel.AutoSize = true;
            this.zLabel.Location = new System.Drawing.Point(54, 58);
            this.zLabel.Name = "zLabel";
            this.zLabel.Size = new System.Drawing.Size(15, 15);
            this.zLabel.TabIndex = 1;
            this.zLabel.Text = "z:";
            // 
            // stateTextBox
            // 
            this.stateTextBox.Location = new System.Drawing.Point(104, 26);
            this.stateTextBox.Name = "stateTextBox";
            this.stateTextBox.Size = new System.Drawing.Size(92, 23);
            this.stateTextBox.TabIndex = 2;
            // 
            // zTextBox
            // 
            this.zTextBox.Location = new System.Drawing.Point(104, 55);
            this.zTextBox.Name = "zTextBox";
            this.zTextBox.Size = new System.Drawing.Size(92, 23);
            this.zTextBox.TabIndex = 3;
            // 
            // okButton
            // 
            this.okButton.Location = new System.Drawing.Point(87, 108);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(75, 23);
            this.okButton.TabIndex = 4;
            this.okButton.Text = "ОК";
            this.okButton.UseVisualStyleBackColor = true;
            this.okButton.Click += new System.EventHandler(this.okButton_Click);
            // 
            // TextMessageBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(273, 172);
            this.Controls.Add(this.mainPanel);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(289, 211);
            this.MinimumSize = new System.Drawing.Size(289, 211);
            this.Name = "TextMessageBox";
            this.Text = "TextMessageBox";
            this.mainPanel.ResumeLayout(false);
            this.mainPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Panel mainPanel;
        private Button okButton;
        private TextBox zTextBox;
        private TextBox stateTextBox;
        private Label zLabel;
        private Label stateLabel;
    }
}