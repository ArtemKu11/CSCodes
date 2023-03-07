using GCodeTranslator.Connection.GCodeSender;
using GCodeTranslator.Connection.Utils.InfoTextBoxChangeProcessor;
using GCodeTranslator.Forms.RobotConnectionWindow;

namespace GCodeTranslator.Connection.Utils.ZHandler;


/// <summary>
/// Очередная тумманная оригинальная логика.
/// <para>
/// Суть - принтовать в _infoTextBox в <see cref="RobotConnectionForm"/>
/// изменение координаты z.
/// </para>
/// Этот класс не занимается принтованием, а лишь готовит строку для принта. Принт запускается явно в <see cref="ToRobotSender"/>
/// при помощи <see cref="InfoTextBoxProcessor"/>
/// <para>
/// Ньюанс заключается в том, что сервер отвечает всегда с константной z, а потому и изменение никогда не принтуется
/// </para>
/// </summary>
public class ZCoordinateHandler
{
    private float _currentZCoord;
    private float _maxZCoord;
    private float _minZCoord;
    private float _avgZCoord;

    private bool _firstCoordinateFlag = true;

    public void ResolveZCoordinate(string zCoord)
    {
        try
        {
            RefreshAll(zCoord);
        }
        catch (FormatException)
        {
        }
    }

    private void RefreshAll(string zCoord)
    {
        _currentZCoord = float.Parse(zCoord);
        if (_firstCoordinateFlag)
        {
            _maxZCoord = _currentZCoord;
            _minZCoord = _currentZCoord;
            _avgZCoord = _currentZCoord;
            _firstCoordinateFlag = false;
        }
        else
        {
            ResolveMinMaxAvg();
        }
    }

    private void ResolveMinMaxAvg()
    {
        if (_currentZCoord < _minZCoord)
        {
            _minZCoord = _currentZCoord;
        } else if (_currentZCoord > _maxZCoord)
        {
            _maxZCoord = _currentZCoord;
        }

        _avgZCoord = (_maxZCoord + _minZCoord) / 2;
    }

    public string? GetAsString()
    {
        if (_firstCoordinateFlag)
        {
            return null;
        }
        return $"Текущее Z = {_currentZCoord}\n" + $" Минимальное Z = {_minZCoord}\n" + $" Максимальное Z = {_maxZCoord}\n" + $" Среднее Z = {_avgZCoord}";
    }
}