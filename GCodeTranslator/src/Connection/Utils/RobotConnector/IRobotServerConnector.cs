using GCodeTranslator.Connection.DTO;
using GCodeTranslator.Connection.Utils.CheckConnection;

namespace GCodeTranslator.Connection.Utils.RobotConnector;

/// <summary>
/// Основной класс, инкапсулирующий и осуществляющий фактическое соединение с роботом. Обертка над <see cref="ConnectionChecker"/>
/// + дополнительные методы отправки на робот
/// </summary>
public interface IRobotServerConnector
{
    
    /// <summary>
    /// Основная функция проверки соединения. Работает в main потоке
    /// </summary>
    /// <param name="requiredForm">true - проверка с формой, false - без</param>
    /// <param name="autoCloseIf0">Требуется ли автозакрытие формы после проверки соединения при ответе сервера = 0</param>
    /// <param name="autoCloseIf1">Требуется ли автозакрытие формы после проверки соединения при ответе сервера = 1</param>
    /// <param name="autoCloseIf2">Требуется ли автозакрытие формы после проверки соединения при ответе сервера = 2</param>
    /// <param name="autoCloseIfMinus1">Требуется ли автозакрытие формы после проверки соединения при ответе сервера = -1</param>
    /// <returns>Возвращает: Key - state, Value - z</returns>
    public KeyValuePair<string, string> CheckConnection(bool requiredForm = false,
        bool autoCloseIf0 = true, bool autoCloseIf1 = true, bool autoCloseIf2 = true, bool autoCloseIfMinus1 = false);

    /// <summary>
    /// Отправляет роботу .tp файл
    /// </summary>
    /// <param name="fileName">Полное имя файла</param>
    /// <param name="filePath">Полный путь до файла</param>
    public void SendFile(string fileName, string filePath);

    /// <summary>
    /// Отправляет роботу информацию о прежду отправленном файле. Как правило, выполняется сразу после <see cref="SendFile"/>
    /// </summary>
    /// <param name="sendFileInfo">ДТОшка информации о файле</param>
    /// <returns>Возвращает ответ сервера</returns>
    public string SendFileInfo(SendFileInfo sendFileInfo);

}