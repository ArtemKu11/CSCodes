using GCodeTranslator.Parsing.DTO;

namespace GCodeTranslator.Parsing.FileToObjectParsers;


/// <summary>
/// <para>
/// Основное назначение наследников этого интерфейса - парсить файл G-кодов соответствующего формата в List of <see cref="GCodePoint"/>,
/// соблюдая некоторые правила, которые можно узнать, изучив наследников.
/// </para>
/// <para>
/// List of <see cref="GCodePoint"/>, в свою очередь, затем отдается следующему парсеру, который уже делает .ls файл/файлы
/// </para>
/// <para>
/// У наследников над названием класса / методом Parse() должны быть более-менее адекватные разъяснения, как работает конкретная имплементация
/// </para>
/// </summary>
public interface IToObjectParser
{

    RequiredPropertiesForParsers GetRequiredProperties();
    
    /*
     * Запускает логику парсинга файла
     * Парсит в List<GCodePoint> и возвращает результат
     */
    List<GCodePoint> Parse();
    
}