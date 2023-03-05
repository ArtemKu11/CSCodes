using GCodeTranslator.Utils.DebugUtils.DebugService;

namespace GCodeTranslator.Utils.DebugUtils.DebugWindow
{
    public partial class DebugWindowForm : Form
    {
        private DebugWindowFormService _debugWindowFormService;
        public DebugWindowForm()
        {
            InitializeComponent();
            CenterToScreen();
            _debugWindowFormService = new DebugWindowFormService(this);
            MessageBox.Show("Режим разработчика включен");
        }

        private void DebugWindowForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            _debugWindowFormService.DisableDebugMode();
        }
        
        private void messageBoxRequiredCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            valueRequiredCheckBox.Checked = !messageBoxRequiredCheckBox.Checked;
            _debugWindowFormService.SetRequirements(messageBoxRequiredCheckBox.Checked);
        }

        private void valueRequiredCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            messageBoxRequiredCheckBox.Checked = !valueRequiredCheckBox.Checked;
            _debugWindowFormService.SetRequirements(messageBoxRequiredCheckBox.Checked);
        }

        private void applyValueButton_Click(object sender, EventArgs e)
        {
            _debugWindowFormService.ChangeNextReturnedValue();
        }

        private void timeOutExceptionButton_Click(object sender, EventArgs e)
        {
            _debugWindowFormService.SetValues("-1", null);
        }

        private void readyButton_Click(object sender, EventArgs e)
        {
            _debugWindowFormService.SetValues("0", null);
        }

        private void printButton_Click(object sender, EventArgs e)
        {
            _debugWindowFormService.SetValues("1", null);
        }

        private void fileRequiredButton_Click(object sender, EventArgs e)
        {
            _debugWindowFormService.SetValues("2", null);
        }

        private void formRequiredCheckConnectionButton_Click(object sender, EventArgs e)
        {
            _debugWindowFormService.CheckConnection(true);
        }

        private void formNotRequiredCheckConnectionButton_Click(object sender, EventArgs e)
        {
            _debugWindowFormService.CheckConnection(false);
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            _debugWindowFormService.SaveNewSettings();
        }
    }
}