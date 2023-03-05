using GCodeTranslator.Parsing.DTO;
using GCodeTranslator.Utils.LogUtils;

namespace GCodeTranslator.Parsing.FileToObjectParsers.PowerMillParser;


/// <summary>
/// <para>Первый парсер в цепочке из двух парсеров</para>
/// <para>Парсит файл из PowerMill в лист <see cref="GCodePoint"/> для последующей его передачи следующему парсеру.</para>
/// <para>Представляет собой полностью скопированный и отрефрактроренный оригинальный парсер.</para>
///
/// <para>Для использования необходимо передать объект класса <see cref="RequiredPropertiesForParsers"/> в конструктор
/// и вызвать метод <see cref="Parse"/></para>
/// </summary>
public class PowerMillToObjectParser : IToObjectParser
{
    private readonly List<GCodePoint> _gCodePoints = new();  // Результат парсинга
    private LsrLine _currentLsrLine;  // DTO-шки на строку .lsr файла
    private LsrLine _previousLsrLine;
    private LargeCoordinates _currentLargeCoordinates;  // Координаты, которые пойдут в GCodePoint
    // private LargeCoordinates _previousLargeCoordinates;  // В оригинале зачем-то есть, но очевидно никакой пользы не несет
    private bool _movement;  // Какие-то костыли для работы логики
    private bool _firstLine = true;

    private readonly RequiredPropertiesForParsers _formProperties;
    private readonly string _filePath;  // Полный путь до файла
    private readonly bool _autoArcEnabledCheckBoxChecked;  // Значение галочки "Auto Arc Enabled By Extrusion"  
    private readonly string _checkingDistanceTextBoxText;  // Значение поля "Checking distance (mm)"
    private readonly bool _removeSmallStopStartCheckBoxChecked;  // Значение галочки "Remove Small Stop Start"
    
    private readonly Logger _logger = LoggerFactory.GetExistingOrCreateNewLogger("root_log");

    
    public PowerMillToObjectParser(RequiredPropertiesForParsers formProperties)
    {
        _formProperties = formProperties;
        _checkingDistanceTextBoxText = formProperties.CheckingDistanceTextBoxText;
        _removeSmallStopStartCheckBoxChecked = formProperties.RemoveSmallStopStartCheckBoxChecked;
        _filePath = formProperties.BrowsedFileProperties.FilePath;
        _autoArcEnabledCheckBoxChecked = formProperties.AutoArcByExtrusionCheckBoxChecked;
    }
    public RequiredPropertiesForParsers GetRequiredProperties()
    {
        return _formProperties;
    }

    
    /// <summary>
    /// Запускает логику парсинга .lsr файла, определенного в объекте RequiredPropertiesForParsers,
    /// который необходимо передать в конструктор
    /// </summary>
    /// <returns>Возвращает результат парсинга в виде C#-объектов - List of GCodePoint</returns>
    public List<GCodePoint> Parse()
    {
        _logger.LogWithTime("PowerMillToObjectParser Parse START");
        
        using(var sr = new StreamReader(_filePath))
        {
            while (!sr.EndOfStream)
            {

                var line = sr.ReadLine();

                if (line != null && !line.Contains("#"))
                {
                    try  // Все try catch сохранены как в оригинале, не смотря на то, что в catch какая-то дичь
                    {
                        ParseLine(line);
                    }
                    catch (Exception e)
                    {
                        MessageBox.Show(_gCodePoints.Count.ToString());
                        MessageBox.Show(_currentLsrLine.ToString());
                        MessageBox.Show(_previousLsrLine.ToString());
                        _currentLargeCoordinates.State = PrintStateEnum.Move;
                        _firstLine = true;
                        MessageBox.Show(e.ToString());
                    }
                }
            }
        }

        _logger.LogWithTime("PowerMillToObjectParser Parse END");
        _logger.Log($"Количество точек: {_gCodePoints.Count}");
        return _gCodePoints;
    }
    
    /// <summary>
    /// Парсит строку из .lsr в GCodePoint
    /// </summary>
    /// <param name="line">Строка из .lsr</param>
    private void ParseLine(string line)
    {
        ParseIntoObject(line);
        if (_firstLine)
        {
            FirstLineParsing();  // Тоже самое, что и AddDefaultPoint(), только ExchangeCoordinates() без try-catch. Почему? Зачем? Не знаю
            _firstLine = false;
        } else if (LineIsBullshit())  // Проверка, не попали ли мы в линию между контурами или в ту, что надо пропустить по галочке "Remove small stop-start"
        {
            AddTwoCustomPoints();  // Если попали, то добавить две точки - одну, которая допринтует предыдущую строку, вторая - которая передвинет к следующей точке
        }
        else
        {
            AddDefaultPoint();
        }
    }

    private void ParseIntoObject(string line)
    {
        var lineParameters = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);
        _currentLsrLine = new LsrLine(lineParameters[0], lineParameters[1], lineParameters[2], lineParameters[3], 
            lineParameters[4], lineParameters[5], lineParameters[6], 
            lineParameters[7], lineParameters[8], lineParameters[9], 
            lineParameters[10], lineParameters[11], lineParameters[12]);
    }

    private void FirstLineParsing()
    {
        ExchangeCoordinates();
        if (_autoArcEnabledCheckBoxChecked)
        {
            _currentLargeCoordinates.State = PrintStateEnum.Move;
        }
        CreateAndAddPoint();
        
    }

    private void ExchangeCoordinates()
    {
        // _previousLargeCoordinates = _currentLargeCoordinates;
        
        _movement = false;

        _currentLargeCoordinates.X = _currentLsrLine.FloatStartX;
        _currentLargeCoordinates.Y = _currentLsrLine.FloatStartY;
        _currentLargeCoordinates.Z = _currentLsrLine.FloatStartZ;
        _currentLargeCoordinates.A = _currentLsrLine.FloatArcX;
        _currentLargeCoordinates.B = _currentLsrLine.FloatArcY;
        _currentLargeCoordinates.C = _currentLsrLine.FloatArcZ;
        
        _movement = true;  // Оригинальная логика, ага

        _currentLargeCoordinates.FeedRate = (int)Math.Round(_currentLsrLine.FloatSpeed);
        
        _previousLsrLine = _currentLsrLine;

    }

    private void CreateAndAddPoint()
    {
        var gCodePoint = new GCodePoint();
        
        gCodePoint.LargeCoordinates = _currentLargeCoordinates;
        gCodePoint.Movement = _movement;
        gCodePoint.Positioner = new Positioner();
        
        _gCodePoints.Add(gCodePoint);
    }

    private bool LineIsBullshit()
    {
        if (Math.Abs(_previousLsrLine.FloatEndX - _currentLsrLine.FloatStartX) < 0.1 &&  // Т.к. мы скипаем #COMMENT, то нужно ловить линию с несовпадением from-to координат
            Math.Abs(_previousLsrLine.FloatEndY - _currentLsrLine.FloatStartY) < 0.1 &&
            Math.Abs(_previousLsrLine.FloatEndZ - _currentLsrLine.FloatStartZ) < 0.1)
        {
            return false;  //TODO Возможная ошибка: Ну пусть линия не переходная. Но ведь проверить ее на "Remove small start-stop" тоже надо
        }
        if (_removeSmallStopStartCheckBoxChecked && GetLineDistance() <= Int32.Parse(_checkingDistanceTextBoxText))
        {

            return false;  //TODO Возможная ошибка: Вот по логике условия тут должно быть true

        }

        return true;
    }

    private float GetLineDistance()
    {
        var distance = Math.Sqrt(Math.Pow(_previousLsrLine.FloatEndX - _currentLsrLine.FloatStartX, 2) + 
                                 Math.Pow(_previousLsrLine.FloatEndY - _previousLsrLine.FloatStartY, 2) + 
                                 Math.Pow(_previousLsrLine.FloatEndZ - _previousLsrLine.FloatStartZ, 2));
        return (float) distance;
    }

    private void AddTwoCustomPoints()
    {
        ExchangeCoordinatesForFirstCustomPoint();  // Принтуем до конечной координаты прошлой строки
        if (_autoArcEnabledCheckBoxChecked)
        {
            _currentLargeCoordinates.State = PrintStateEnum.Printing;
        }
        CreateAndAddPoint();
        
        ExchangeCoordinatesForSecondCustomPoint();  // А затем двигаемся к новой точке
        if (_autoArcEnabledCheckBoxChecked)
        {
            _currentLargeCoordinates.State = PrintStateEnum.Move;
        }
        CreateAndAddPoint();
        _previousLsrLine = _currentLsrLine;
    }

    private void ExchangeCoordinatesForFirstCustomPoint()
    {
        // _previousLargeCoordinates = _currentLargeCoordinates;
        
        _movement = false;  // Зачем, если через 2 строчки true - не знаю
        _currentLargeCoordinates.X = _previousLsrLine.FloatEndX;
        _currentLargeCoordinates.Y = _previousLsrLine.FloatEndY;
        _movement = true;
        _currentLargeCoordinates.Z = _previousLsrLine.FloatEndZ;
        _currentLargeCoordinates.A = _currentLsrLine.FloatArcX;
        _currentLargeCoordinates.B = _currentLsrLine.FloatArcY;
        _currentLargeCoordinates.C = _currentLsrLine.FloatArcZ;
        _currentLargeCoordinates.FeedRate = (int)Math.Round(_previousLsrLine.FloatSpeed);
    }

    private void ExchangeCoordinatesForSecondCustomPoint()
    {
        // _previousLargeCoordinates = _currentLargeCoordinates;

        _movement = false;  // Видимо, это костыль на try catch
        
        _currentLargeCoordinates.X = _currentLsrLine.FloatStartX;
        _currentLargeCoordinates.Y = _currentLsrLine.FloatStartY;
        _currentLargeCoordinates.Z = _currentLsrLine.FloatStartZ;
        _currentLargeCoordinates.A = _currentLsrLine.FloatArcX;
        _currentLargeCoordinates.B = _currentLsrLine.FloatArcY;
        _currentLargeCoordinates.C = _currentLsrLine.FloatArcZ;
        _movement = true;  // Ну или 1000 IQ ход
 
        _currentLargeCoordinates.FeedRate = (int)Math.Round(_currentLsrLine.FloatSpeed);
    }

    private void AddDefaultPoint()
    {
        try
        {
            ExchangeCoordinates();
        }
        catch (Exception e)
        {
            MessageBox.Show(e.ToString());
        }
        if (_autoArcEnabledCheckBoxChecked)
        {
            _currentLargeCoordinates.State = PrintStateEnum.Printing;
        }
        CreateAndAddPoint();
    }
}