namespace GCodeTranslator.Parsing.DTO;


/// <summary>
/// Составляющая <see cref="GCodeLine"/>
/// </summary>
public struct GCodeLineParameter
{
    public readonly string ParamType;
    public readonly float Value;

    public GCodeLineParameter(string paramType, float value)
    {
        ParamType = paramType;
        Value = value;
    }
}