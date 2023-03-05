using GCodeTranslator.Service.DTO;
using GCodeTranslator.Service.SettingsWindow;

namespace GCodeTranslator.Forms.SettingsWindow
{
    /// <summary>
    /// <para>
    /// Окно "Настройки"
    /// </para>
    /// Хранит соответствующие листенеры
    /// </summary>
    public partial class SettingsWindowForm : Form
    {
        private readonly SettingsWindowFormService _settingsWindowFormService;
        public SettingsWindowForm()
        {
            InitializeComponent(); // Создание интерфейса. Сгенерировано автоматически
            CenterToScreen();
            _settingsWindowFormService = new SettingsWindowFormService(this);
            DisableButtons();
        }

        public SettingsHolder GetActualSettingsHolder()
        {
            return _settingsWindowFormService.CreateSettingsHolder();
        }


        /*
         * Клик по кнопке "Сохранить"
         */
        private void SaveButton_Click(object sender, EventArgs e)
        {
            var success = _settingsWindowFormService.SaveSettingsPropertiesOnDisk();
            if (success)
            {
                DisableButtons();
            }
        }


        /*
         * Клик по кнопке "Отмена"
         */
        private void ResetButton_Click(object sender, EventArgs e)
        {
            _settingsWindowFormService.SetPropertiesFromDisk();
            DisableButtons();
        }
        


        /*
        * Ивент галочки "Таймер следующего слоя"
        */
        private void nextLayerTimerCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            NextLayerTimerTextBox.Enabled = NextLayerTimerCheckBox.Checked;
            EnableButtons();
        }


        /*
        * Ивент смены текста поля "Таймер следующего слоя"
        */
        private void nextLayerTimerTextBox_TextChanged(object sender, EventArgs e)
        {
            EnableButtons();
        }


        /*
        * Ивент галочки "Время ожидания отмены нажатия кнопки"
        */
        // Название придумал не я, что это значит, понять я не смог))
        private void cancelTimeCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            CancelTimeTextBox.Enabled = CancelTimeCheckBox.Checked;
            EnableButtons();
        }


        /*
        * Ивент смены текста поля "Время ожидания отмены нажатия кнопки"
        */
        private void cancelTimeTextBox_TextChanged(object sender, EventArgs e)
        {
            EnableButtons();
        }


        /*
        * Ивент смены текста поля "Дефолт Ip робота"
        */
        private void defaultIpTextBox_TextChanged(object sender, EventArgs e)
        {
            EnableButtons();
        }


        /*
        * Ивент смены текста поля "Макс. время ожид. соединения (сек)"
        */
        private void maxConnectionTimeTextBox_TextChanged(object sender, EventArgs e)
        {
            EnableButtons();
        }


        /*
         * Ивент галочки "Включить логи"
         */
        private void enableLogsCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            EnableButtons();
        }

        private void EnableButtons()
        {
            cancelButton.Enabled = true;
            saveButton.Enabled = true;
        }

        private void DisableButtons()
        {
            cancelButton.Enabled = false;
            saveButton.Enabled = false;
        }
    }
}