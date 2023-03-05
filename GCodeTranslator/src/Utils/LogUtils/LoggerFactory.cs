namespace GCodeTranslator.Utils.LogUtils;

public static class LoggerFactory
{
    private static readonly Dictionary<string, Logger> ExistingLoggers = new();
    private static bool _enabled;
    private static object _locker = new();

    public static bool Enabled
    {
        get => _enabled;
        set => _enabled = value;
    }
    
    public static Logger GetExistingOrCreateNewLogger(string name)
    {
        lock (_locker)
        {
            return ExistingLoggers.TryGetValue(name, out var logger) ? logger : GetNewLogger(name: name);
        }
    }
    
    private static Logger GetNewLogger(string name)
    {
        lock (_locker)
        {
            if (name.ToLowerInvariant().StartsWith("appendable"))
            {
                throw new Exception("Обычный логгер не может быть appendable. Используйте GetAppendableLogger()");
            }

            if (ExistingLoggers.TryGetValue(name, out var logger))
            {
                throw new Exception("Логгер с именем " + name + " уже существует");
            }

            logger = new Logger(name);
            ExistingLoggers.Add(name, logger);
            return logger;
        }
    }
    
    public static Logger GetAppendableLogger(string name)
    {
        lock (_locker)
        {
            if (ExistingLoggers.TryGetValue("appendable_" + name, out var logger))
            {
                return logger;
            }

            logger = new Logger("appendable_" + name);
            ExistingLoggers.Add("appendable_" + name, logger);
            return logger;
        }
    }
}