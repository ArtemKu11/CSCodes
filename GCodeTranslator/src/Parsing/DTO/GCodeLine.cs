using GCodeTranslator.Parsing.FileToObjectParsers.SimplifyParser;

namespace GCodeTranslator.Parsing.DTO;


/// <summary>
/// Вспомогательная вещь для <see cref="SimplifyToObjectParser"/>
/// <para>
/// Первоначально (В <see cref="SimplifyToObjectParser"/>) каждая линия файла приводится к объекту этого типа для удобства парсинга
/// </para>
/// </summary>
public struct GCodeLine
{
    public List<GCodeLineParameter> Parameters;
    public int FeedRate;
    public string Command;
    public int CommandValue;
}