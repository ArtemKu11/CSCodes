namespace GCodeTranslator.Connection.Utils.ZHandler;

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