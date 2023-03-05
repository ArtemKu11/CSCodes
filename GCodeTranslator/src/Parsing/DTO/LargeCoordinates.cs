namespace GCodeTranslator.Parsing.DTO;


/// <summary>
/// Составляющая <see cref="GCodePoint"/>
/// </summary>
public struct LargeCoordinates
{
    public float X, Y, Z, E, A, B, C;
    public int FeedRate;
    public PrintStateEnum State;
}
