using GCodeTranslator.Parsing.DTO;
using GCodeTranslator.Parsing.FileToObjectParsers;

namespace GCodeTranslator.Parsing.ObjectToRobotParser;


/*
 * Назначение - принимать в конструктор IToObjectParser, запускать его логику,
 * а зачем парсить полученный List<GCodePoint>
 *
 * 
 */

/// <summary>
/// Назначение - принимать в конструктор <see cref="IToObjectParser"/>, запускать его логику,
/// а зачем парсить полученный List of <see cref="GCodePoint"/>
/// <para>
/// Более подробное описание - над названием класса у наследников / методом <see cref="Parse"/>
/// </para>
/// </summary>
public interface IToRobotParser
{
    
    RequiredPropertiesForParsers GetRequiredProperties();
    /// <summary>
    /// Запускает логику парсинга
    /// </summary>
    void Parse();
}