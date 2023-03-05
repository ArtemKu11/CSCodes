using GCodeTranslator.Connection.DTO;

namespace GCodeTranslator.Service.DTO;

public struct RobotTableRow
{
    public string Id;
    public string Ip;
    public string CurrentRobotState;
    public RequiredPropertiesForConnection PropertiesForConnection;
}