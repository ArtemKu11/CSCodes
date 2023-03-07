using GCodeTranslator.Forms.MainWindow;
using GCodeTranslator.Parsing.DTO;
using GCodeTranslator.Parsing.ObjectToRobotParser;

namespace GCodeTranslator.Parsing.PostProcessors;


/// <summary>
/// Наследники - элементы цепочки парсинга, запускаемые, как правило, после парсеров.
/// <para>
/// Должен содержать два конструктора - один под <see cref="IToRobotParser"/>, второй под <see cref="IPostProcessor"/>
/// </para>
/// </summary>
public interface IPostProcessor
{
    /// <summary>
    /// Для получения следующим пост-процессором свойств с <see cref="MainWindowForm"/>
    /// <para>
    /// Важно сначала выполнить <see cref="PostProcess"/>, а только потом эту функцию, а иначе будет null
    /// </para>
    /// </summary>
    /// <returns>Свойства полей/галочек с <see cref="MainWindowForm"/></returns>
    RequiredPropertiesForParsers? GetRequiredProperties();
    
    /// <summary>
    /// Обычно алгоритм работы этой функции следующий:
    /// <para>
    /// 1. Запуск логики предыдущего парсера/пост-процессора
    /// </para>
    /// 2. Получение свойств из предыдущего парсера/пост-процессора
    /// <para>
    /// 3. Запуск логики текущего пост-процессора
    /// </para>
    /// </summary>
    void PostProcess();
}