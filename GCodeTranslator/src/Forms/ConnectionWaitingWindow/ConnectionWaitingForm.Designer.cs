namespace GCodeTranslator.Forms.ConnectionWaitingWindow
{
    partial class CheckConnectionForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CheckConnectionForm));
            this.checkConnectionMainPanel = new System.Windows.Forms.Panel();
            this.errorClassLabel = new System.Windows.Forms.Label();
            this.errorTypeLabel = new System.Windows.Forms.Label();
            this.connectionWaitingLabel = new System.Windows.Forms.Label();
            this.checkConnectionMainPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // checkConnectionMainPanel
            // 
            this.checkConnectionMainPanel.Controls.Add(this.errorClassLabel);
            this.checkConnectionMainPanel.Controls.Add(this.errorTypeLabel);
            this.checkConnectionMainPanel.Controls.Add(this.connectionWaitingLabel);
            this.checkConnectionMainPanel.Location = new System.Drawing.Point(12, 12);
            this.checkConnectionMainPanel.Name = "checkConnectionMainPanel";
            this.checkConnectionMainPanel.Size = new System.Drawing.Size(776, 426);
            this.checkConnectionMainPanel.TabIndex = 0;
            // 
            // errorClassLabel
            // 
            this.errorClassLabel.AutoSize = true;
            this.errorClassLabel.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.errorClassLabel.Location = new System.Drawing.Point(380, 279);
            this.errorClassLabel.Name = "errorClassLabel";
            this.errorClassLabel.Size = new System.Drawing.Size(0, 19);
            this.errorClassLabel.TabIndex = 2;
            // 
            // errorTypeLabel
            // 
            this.errorTypeLabel.AutoSize = true;
            this.errorTypeLabel.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.errorTypeLabel.Location = new System.Drawing.Point(285, 279);
            this.errorTypeLabel.Name = "errorTypeLabel";
            this.errorTypeLabel.Size = new System.Drawing.Size(0, 19);
            this.errorTypeLabel.TabIndex = 1;
            // 
            // connectionWaitingLabel
            // 
            this.connectionWaitingLabel.AutoSize = true;
            this.connectionWaitingLabel.Font = new System.Drawing.Font("Segoe UI", 19F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.connectionWaitingLabel.Location = new System.Drawing.Point(307, 195);
            this.connectionWaitingLabel.Name = "connectionWaitingLabel";
            this.connectionWaitingLabel.Size = new System.Drawing.Size(162, 36);
            this.connectionWaitingLabel.TabIndex = 0;
            this.connectionWaitingLabel.Text = "Соединение";
            // 
            // CheckConnectionForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.checkConnectionMainPanel);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(816, 489);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(816, 489);
            this.Name = "CheckConnectionForm";
            this.Text = "Проверка соединения";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.CheckConnectionForm_FormClosing);
            this.checkConnectionMainPanel.ResumeLayout(false);
            this.checkConnectionMainPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Panel checkConnectionMainPanel;
        private Label connectionWaitingLabel;
        private Label errorClassLabel;
        private Label errorTypeLabel;
    }
}