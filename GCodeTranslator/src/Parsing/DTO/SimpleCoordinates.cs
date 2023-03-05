using GCodeTranslator.Parsing.ObjectToRobotParser;

namespace GCodeTranslator.Parsing.DTO;

/// <summary>
/// Вспомогательная вещь для <see cref="IToRobotParser"/>
/// </summary>
public struct SimpleCoordinates
{
    public float X, Y, Z, W, P, R;
}