using GCodeTranslator.Connection.Utils.RobotConnector;
using GCodeTranslator.Connection.Utils.RobotStateChangeProcessor;
using GCodeTranslator.Service.DTO;

namespace GCodeTranslator.Connection.DTO;

public struct RequiredPropertiesForConnection
{
    public SettingsHolder SettingsHolder;  // Актуальные на момент открытия формы значения окна "Настройки"
    public string IpAddress;  // Значение поля "Enter Robot Address"
    public RobotStateProcessor RobotStateProcessor;  // Класс, меняющий значение ячейки "Current Robot State" в MainWindowForm и _stateLabel в RobotConnectionForm
    public RobotServerConnector RobotServerConnector;
    public string StateBeforeOpenForm;
    public Color ColorOfStateBeforeOpenForm;
}