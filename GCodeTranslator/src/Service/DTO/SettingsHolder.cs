namespace GCodeTranslator.Service.DTO;

public class SettingsHolder
{
    public bool NextLayerTimerEnabled { get; set; }
    public string NextLayerTimerText { get; set; } = "";
    
    public bool CancelButtonTimeEnabled { get; set; }
    public string CancelButtonTimeText { get; set; } = "";

    public string DefaultRobotIp { get; set; } = "";

    public string MaxConnectionTime { get; set; } = "";
    
    public bool EnableLogs { get; set; }
}