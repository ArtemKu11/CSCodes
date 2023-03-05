namespace GCodeTranslator.Utils.LogUtils;

public class Logger
{
    private readonly string _loggerPath;
    private readonly bool _isAppendable;
    private readonly object _locker = new();

    public Logger(string name)
    {
        _loggerPath = "logs/" + name + ".log";
        if (!Directory.Exists("logs/"))
        {
            Directory.CreateDirectory("logs/");
        }

        if (name.StartsWith("appendable"))
        {
            _isAppendable = true;
            CheckLogFileExist(_loggerPath);
            CheckFileSizeLimit(_loggerPath);
        }
        else
        {
            File.Create(_loggerPath).Close();
        }
    }
    
    private void CheckLogFileExist(string path)
    {
        if (!File.Exists(path))
        {
            File.Create(path).Close();
        }
    }

    private void CheckFileSizeLimit(string path)
    {
        FileInfo info = new FileInfo(path);
        if (info.Length > 5000000)
        {
            File.Create(path).Close();
        }
    }
    
    public void LogWithTime(string logText)
    {
        if (!_isAppendable && !LoggerFactory.Enabled) return;
        lock (_locker)
        {
            var dateTimeNow = DateTime.Now;
            string[] logLines = { dateTimeNow + " " + logText };
            File.AppendAllLines(_loggerPath, logLines);
        }
    }
    
    public void LogException(Exception e)
    {
        if (!_isAppendable && !LoggerFactory.Enabled) return;
        lock (_locker)
        {
            DateTime dateTime = DateTime.Now;
            Log("\n" + dateTime + "\n" + e);
        }
    }
    
    public void Log(string logText)
    {
        if (!_isAppendable && !LoggerFactory.Enabled) return;
        lock (_locker)
        {
            string[] logLines = { logText };
            File.AppendAllLines(_loggerPath, logLines);
        }
    }

    /// <summary>
    /// Null-Safety To String
    /// </summary>
    /// <param name="logObject">null-able object</param>
    public string Nst(object? logObject)
    {
        if (logObject is null)
        {
            return "null";
        }
        else
        {
            return logObject.ToString() ?? throw new InvalidOperationException();
        }
    }
}