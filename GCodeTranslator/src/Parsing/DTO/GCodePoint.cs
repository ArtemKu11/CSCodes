using GCodeTranslator.Parsing.FileToObjectParsers;

namespace GCodeTranslator.Parsing.DTO;


/// <summary>
/// <para>
/// Каждая линия в файле в результате парсинга (В <see cref="IToObjectParser"/>) приводится к объекту этого типа.
/// </para>
/// Формируется лист этих элементов и отдается следующему парсеру
/// <para></para>
/// Содержит:
/// <para>
/// <see cref="bool"/> Movement
/// </para>
/// <see cref="LargeCoordinates"/> LargeCoordinates (X, Y, Z, E, A, B, C, FeedRate, State)
/// <para>
/// <see cref="Positioner"/> Positioner (J1, J2)
/// </para>
/// </summary>
public struct GCodePoint
{
    public bool Movement;
    public LargeCoordinates LargeCoordinates;
    public Positioner Positioner;  //TODO Возможная ошибка: очень странное использование позиционера по коду
    /*
     * С одной стороны в SimplifyParser сюда записываются параметры A и B команды M800, которая находится в блоке с командами на охлаждение
     * С другой стороны в ToRobotParser значения из Positioner записываются как deg и подразумеваются как углы.
     * Хотя вполне может быть, что закономерности с охлаждением нет
     *
     * Помимо этого в ToRobotParser Positioner всегда дефолт (0, 0) 
     */
}