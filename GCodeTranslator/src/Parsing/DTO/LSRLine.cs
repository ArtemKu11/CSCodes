using System.Globalization;
using GCodeTranslator.Parsing.FileToObjectParsers.PowerMillParser;

namespace GCodeTranslator.Parsing.DTO;


/// <summary>
/// ДТОшка строки .lsr файла
/// <para>
/// Вспомогательная вещь для <see cref="PowerMillToObjectParser"/>
/// </para>
/// </summary>
public readonly struct LsrLine
{
    private readonly string _power;
    private readonly string _arcX;
    private readonly string _arcY;
    private readonly string _arcZ;
    private readonly string _startX;
    private readonly string _startY;
    private readonly string _startZ;
    private readonly string _endX;
    private readonly string _endY;
    private readonly string _endZ;
    private readonly string _radius;
    private readonly string _speed;
    private readonly string _time;

    public float FloatPower
    {
        get => ToFloat(_power);
    }

    public float FloatArcX
    {
        get => ToFloat(_arcX);
    }

    public float FloatArcY
    {
        get => ToFloat(_arcY);
    }

    public float FloatArcZ
    {
        get => ToFloat(_arcZ);
    }

    public float FloatStartX
    {
        get => ToFloat(_startX);
    }

    public float FloatStartY
    {
        get => ToFloat(_startY);
    }

    public float FloatStartZ
    {
        get => ToFloat(_startZ);
    }

    public float FloatEndX
    {
        get => ToFloat(_endX);
    }

    public float FloatEndY
    {
        get => ToFloat(_endY);
    }

    public float FloatEndZ
    {
        get => ToFloat(_endZ);
    }

    public float FloatRadius
    {
        get => ToFloat(_radius);
    }

    public float FloatSpeed
    {
        get => ToFloat(_speed);
    }

    public float FloatTime
    {
        get => ToFloat(_time);
    }

    public LsrLine(string power, string arcX, string arcY, string arcZ, string startX, string startY, string startZ, string endX, string endY, string endZ, string radius, string speed, string time)
    {
        _power = power;
        _arcX = arcX;
        _arcY = arcY;
        _arcZ = arcZ;
        _startX = startX;
        _startY = startY;
        _startZ = startZ;
        _endX = endX;
        _endY = endY;
        _endZ = endZ;
        _radius = radius;
        _speed = speed;
        _time = time;
    }

    private float ToFloat(string value)
    {
        return float.Parse(value, CultureInfo.InvariantCulture.NumberFormat);
    }

    public override string ToString()
    {
        return
            $"{_power} {_arcX} {_arcY} {_arcZ} {_startX} {_startY} {_startZ} {_endX} {_endY} {_endZ} {_radius} {_speed} {_time}";
    }
}