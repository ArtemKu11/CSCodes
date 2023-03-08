namespace GCodeTranslator.Service.DTO;

/// <summary>
/// ДТОшка свойств из окна "Настройки". Используется для сериализации/десериализации в json и для получения актуальных настроек другими формами
/// </summary>
public class SettingsHolder
{
    public bool NextLayerTimerEnabled { get; set; }
    public string NextLayerTimerText { get; set; } = "";
    
    public bool CancelButtonTimeEnabled { get; set; }
    public string CancelButtonTimeText { get; set; } = "";

    public string DefaultRobotIp { get; set; } = "";

    public string MaxConnectionTime { get; set; } = "";
    
    public bool EnableLogs { get; set; }
    public string PythonPath { get; set; }
}