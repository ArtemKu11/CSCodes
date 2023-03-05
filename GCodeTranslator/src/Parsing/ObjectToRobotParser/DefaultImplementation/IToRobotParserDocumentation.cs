using GCodeTranslator.Forms.MainWindow;
using GCodeTranslator.Parsing.DTO;
using GCodeTranslator.Parsing.FileToObjectParsers;

namespace GCodeTranslator.Parsing.ObjectToRobotParser.DefaultImplementation;

/// <summary>
/// Второй парсер в цепочке из двух парсеров
/// <para>
/// Запускает логику <see cref="IToObjectParser"/> парсера, получает из него List of <see cref="GCodePoint"/>, парсит этот лист в .ls файл
/// </para>
/// <para>
/// Результат:
/// </para>
/// <para>
/// Если есть галочка "Autosplit layers" - сохраняет один .ls файл, запускает питон-слайсер.
///                                        Питон-слайсер скорей всего дробит по слоям этот файл в несколько .ls
/// </para>
/// <para>
/// Если нет галочки "Autosplit layers" - сохраняет несколько .ls файлов. Количество точек в каждом слое - значение поля "Split layers".
///                                      Питон-слайсер не запускает
/// </para>
/// </summary>
public interface IToRobotParserDocumentation : IToRobotParser
{
    /// <summary>
    /// <para>
    /// Представляет собой отрефракторенный оригинальный метод длиной 230 строк. Все, что написано ниже, да и в целом по классу - догадки
    /// </para>
    /// <para>
    /// Использует:
    /// </para>
    /// <para>
    /// Исходные данные:
    /// 
    /// List of <see cref="GCodePoint"/>, полученный из <see cref="IToObjectParser"/> парсера
    ///                  и свойства формы <see cref="MainWindowForm"/>, определенные выше в шапке класса в блоке "Required MainWindowForm properties"
    ///</para>
    /// <para>
    /// Вспомогательные переменные: определены выше в классе в шапке в блоке "Parser values"
    /// </para>
    /// <para>
    /// Возвращает:
    /// </para>
    /// <para>
    /// Если есть галочка "Autosplit layers" - сохраняет один .ls файл, запускает питон-слайсер.
    ///                                        Питон-слайсер скорей всего дробит по слоям этот файл в несколько .ls
    /// </para>
    /// <para>
    /// Если нет галочки "Autosplit layers" - сохраняет несколько .ls файлов. Количество точек в каждом слое - значение поля "Split layers".
    ///                                      Питон-слайсер не запускает
    /// </para>
    /// </summary>
    new void Parse();
}