using GCodeTranslator.Connection.Utils.RobotConnector;

namespace GCodeTranslator.Connection.Utils.CheckConnection;

/// <summary>
/// Обертка над <see cref="CheckConnectionWrapper"/>
/// Преполагается использование через <see cref="RobotServerConnector"/>
/// <para>
/// Предоставляет возможность гибко формировать проверку соединения с сервером
/// </para>
/// Ответы сервера:
/// <para>
/// 0 - готов
/// </para>
/// 1 - печатает
/// <para>
/// 2 - требуется файл
/// </para>
/// 3 - TimeoutException
/// </summary>
public interface IConnectionChecker
{
    
    /// <summary>
    /// Основная функция проверки соединения. Работает в main потоке
    /// </summary>
    /// <param name="ipAddress">Адрес робота</param>
    /// <param name="timeOutInSec">Таймаут соединения</param>
    /// <param name="requiredForm">true - проверка с формой, false - без</param>
    /// <param name="autoCloseIf0">Требуется ли автозакрытие формы после проверки соединения при ответе сервера = 0</param>
    /// <param name="autoCloseIf1">Требуется ли автозакрытие формы после проверки соединения при ответе сервера = 1</param>
    /// <param name="autoCloseIf2">Требуется ли автозакрытие формы после проверки соединения при ответе сервера = 2</param>
    /// <param name="autoCloseIfMinus1">Требуется ли автозакрытие формы после проверки соединения при ответе сервера = -1</param>
    /// <returns>Возвращает: Key - state, Value - z</returns>
    public KeyValuePair<string, string> CheckConnection(string ipAddress, int timeOutInSec, bool requiredForm,
        bool autoCloseIf0 = true, bool autoCloseIf1 = false, bool autoCloseIf2 = false, bool autoCloseIfMinus1 = false);

    /// <summary>
    /// Явная проверка соединения с формой ожидания. Работает в main потоке
    /// </summary>
    /// <param name="ipAddress">Адрес робота</param>
    /// <param name="timeOutInSec">Таймаут соединения</param>
    /// <param name="requiredForm">true - проверка с формой, false - без</param>
    /// <param name="autoCloseIf0">Требуется ли автозакрытие формы после проверки соединения при ответе сервера = 0</param>
    /// <param name="autoCloseIf1">Требуется ли автозакрытие формы после проверки соединения при ответе сервера = 1</param>
    /// <param name="autoCloseIf2">Требуется ли автозакрытие формы после проверки соединения при ответе сервера = 2</param>
    /// <param name="autoCloseIfMinus1">Требуется ли автозакрытие формы после проверки соединения при ответе сервера = -1</param>
    /// <returns>Возвращает: Key - state, Value - z</returns>
    public KeyValuePair<string, string> CheckConnectionWithForm(string ipAddress, int timeOutInSec,
        bool autoCloseIf0 = true, bool autoCloseIf1 = false, bool autoCloseIf2 = false, bool autoCloseIfMinus1 = false);

    /// <summary>
    /// Явная проверка соединения без формы ожидания. Работает в main потоке
    /// </summary>
    /// <param name="ipAddress">Адрес робота</param>
    /// <param name="timeOutInSec">Таймаут соединения</param>
    /// <returns>Возвращает: Key - state, Value - z</returns>
    public KeyValuePair<string, string> CheckConnectionWithoutForm(string ipAddress, int timeOutInSec);
}