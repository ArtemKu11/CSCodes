namespace GCodeTranslator.Utils.LogUtils;

/// <summary>
/// Factory для создания логгеров
/// <para></para>
/// Appendable - пишет всегда. Раз в 5-10 МБ очищает
/// <para>
/// Обычный - перезаписывается при запуске приложения
/// </para>
/// Для создания передать название .log файла без ".log" и без "appendabe_" в случае, если нужен AppendableLogger
/// </summary>
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
    
    /// <summary>
    /// Создает или возвращает существующий обычный логгер. Перезаписывается каждый раз при запуске приложения. Работает по галочке в настройках
    /// </summary>
    /// <param name="name">Имя .log файла без ".log"</param>
    /// <returns>Синглтон экземпляр логгера под соответсвующий .log файл</returns>
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
    
    /// <summary>
    /// Создает или возвращает существующий Appendable логгер. Перезаписывается раз 5-10 МБ. Работает вне зависимости от галочки в настройках
    /// </summary>
    /// <param name="name">Имя .log файла без ".log" и "appendable_"</param>
    /// <returns>Синглтон экземпляр логгера под соответсвующий .log файл</returns>
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