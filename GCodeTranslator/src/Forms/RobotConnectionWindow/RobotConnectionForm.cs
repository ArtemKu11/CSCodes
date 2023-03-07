using GCodeTranslator.Connection.DTO;
using GCodeTranslator.Service.RobotConnectionWindow;
using GCodeTranslator.Utils.LogUtils;

namespace GCodeTranslator.Forms.RobotConnectionWindow
{
    /// <summary>
    /// <para>
    /// Окно "Соединение с роботом"
    /// </para>
    /// Хранит соответствующие листенеры
    /// <para>
    /// Начало логики соединения и отправки кодов роботу искать тут
    /// </para>
    /// </summary>
    public sealed partial class RobotConnectionForm : Form
    {
        private readonly RobotConnectionFormService _robotConnectionFormService;
        private readonly Logger _logger = LoggerFactory.GetExistingOrCreateNewLogger("root_log");

        public RobotConnectionForm(RequiredPropertiesForConnection propertiesForConnection)
        {
            _logger.LogWithTime("RobotConnectionForm CONSTR START");
            
            InitializeComponent(); // Создание интерфейса. Сгененировано автоматически
            CenterToScreen();
            _robotConnectionFormService = new RobotConnectionFormService(this, propertiesForConnection);
            
            _logger.LogWithTime("RobotConnectionForm CONSTR END");
        }

        // Ивент кнопочки "<<"
        private void PrevButton_Click(object sender, EventArgs e)
        {
            _robotConnectionFormService.SelectPrevLayer();
        }

        // Ивент кнопочки "Повтор"
        private void RepeatButton_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Хз че должна делать эта кнопка", caption: "Ну типа Повтор");
        }

        // Ивент кнопочки ">>"
        private void NextButton_Click(object sender, EventArgs e)
        {
            _robotConnectionFormService.SelectNextLayer();
        }

        // Ивент кнопочки "Сброс"
        private void ResetButton_Click(object sender, EventArgs e)
        {
            _robotConnectionFormService.Reset();
        }

        // Ивент кнопочки "Начать печать"
        private void StartPrintingButton_Click(object sender, EventArgs e)
        {
            _logger.LogWithTime("RobotConnectionForm StartPrintingButton_Click START");
            
            _robotConnectionFormService.StartPrinting();
            
            _logger.LogWithTime("RobotConnectionForm StartPrintingButton_Click END");
        }

        // Ивент кнопочки "Экспорт в TP"
        private void ExportToTpButton_Click(object sender, EventArgs e)
        {
            _robotConnectionFormService.HandleExportToTp();
        }

        // Ивент кнопочки "Выбрать папку"
        private void BrowseFolderButton_Click(object sender, EventArgs e)
        {
            _robotConnectionFormService.SelectFolder();
        }

        // Ивент кнопочки "Обновить"
        private void refreshStateButton_Click(object sender, EventArgs e)
        {
            _robotConnectionFormService.RefreshRobotState();
        }

        // Ивент закрытия формы
        private void RobotConnectionForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            _robotConnectionFormService.EliminateEverything();
        }

        private void RobotConnectionForm_Resize(object sender, EventArgs e)
        {
            var control = (Control)sender;
            _robotConnectionFormService.ResolveNewSizeAndLocation(control.Size.Width, control.Size.Height);
        }
    }
}