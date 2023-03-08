using GCodeTranslator.Connection.Utils.RobotConnector;
using GCodeTranslator.Connection.Utils.RobotStateChangeProcessor;
using GCodeTranslator.Forms.MainWindow;
using GCodeTranslator.Service.DTO;

namespace GCodeTranslator.Connection.DTO;

/// <summary>
/// Свойства с <see cref="MainWindowForm"/>, используемые при соединении с роботом
/// </summary>
public struct RequiredPropertiesForConnection
{
    public SettingsHolder SettingsHolder;  // Актуальные на момент открытия формы значения окна "Настройки"
    public string IpAddress;  // Значение поля "Enter Robot Address"
    public RobotStateLabelProcessor RobotStateLabelProcessor;  // Класс, меняющий значение ячейки "Current Robot State" в MainWindowForm и _stateLabel в RobotConnectionForm
    public RobotServerConnector RobotServerConnector;  // Класс, отвечающий за соединение с роботом
    public string StateBeforeOpenForm;  // Состояние перед открытием формы. Сетается в форму сразу же после открытия
    public Color ColorOfStateBeforeOpenForm; 
}