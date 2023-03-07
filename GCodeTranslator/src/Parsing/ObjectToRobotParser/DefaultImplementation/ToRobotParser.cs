using System.Globalization;
using System.Numerics;
using GCodeTranslator.CmdProcessRunner;
using GCodeTranslator.Parsing.DTO;
using GCodeTranslator.Parsing.FileToObjectParsers;
using GCodeTranslator.Parsing.ObjectToRobotParser.SomeLegacy;
using GCodeTranslator.Utils.LogUtils;

namespace GCodeTranslator.Parsing.ObjectToRobotParser.DefaultImplementation;


/// <summary>
/// <inheritdoc cref="IToRobotParserDocumentation"/>
/// </summary>
public class ToRobotParser : IToRobotParserDocumentation
{
    private List<GCodePoint> _points = new();  // То, что парсится. Приходит из предыдущего парсера
    private readonly IToObjectParser _toObjectParser;  // Предыдущий парсер
    /*
     * Parser values
     */
    private readonly List<string> _header = new();  // То, что записывается в файл первым
    private readonly List<string> _footer = new();  // То, что записывается в файл вторым
    
    private Positioner _positioner;  // Хранит J1, J2
    
    private LargeCoordinates _currentLargeCoord;  // Текущие координаты целиком
    private LargeCoordinates _previousLargeCoord;  // Предыдущие координаты целиком
    
    private SimpleCoordinates _coordinates;  // Облегченные координаты. Зачем их 3 объекта, а не два - я не знаю
    private SimpleCoordinates _previousSimpleCoord;
    private SimpleCoordinates _currentSimpleCoord;
    
    private int _pointCounter;  // Счетчик строк. ++ в конце каждой итерации foreach
    private int _layerNumber;  // Счетчик файлов. ++ в конце записи каждого файла
    
    private string _termination = "";  // Строка под "Normal movement" / "Welding movement"
    
    private bool _previousArcStart;  // Для работы логики обработки начала и конца Arc. Как именно в деталях - хз
    private bool _previousArcEnd;
    private int _arcEnabled;
    private float _prevX, _prevY = 0;  // хз что и зачем

    
    private CultureInfo _myCIintl = new("es-ES", true);  // Какие-то вспомогательные вещи
    private string _conv = "";
    private string Conv
    {
        get => _conv.ToString(_myCIintl);
        set
        {
            _conv = value;
            if (_conv.Contains(','))
                _conv = _conv.Replace(",", ".");
            if (!_conv.Contains('.'))
                _conv += ".0";
        }
    }

    /**/


    /*
     * Required MainWindowForm properties. В скобках старые названия из оригинального транслятора (для выявления ошибок)
     */
    private RequiredPropertiesForParsers _propertiesForParsers;
    private string _splitLayerText = ""; // Значение поля "Split layers"  (esplit)
    private bool _autoSplitLayersCheckBoxChecked; // Значение галочки "Autosplit layers"  (CheckLayers)
    private string _fileNameWithoutExtension = ""; // Имя файла без расширения  (fname)
    private string _outputDirectory = ""; // Полный путь до директории под результат  (OutFile)
    private string _normalMovementTextBoxText = ""; // Значение поля "Normal movement"  (Tn)
    private string _weldingMovementTextBoxText = ""; // Значение поля "Welding movement"  (Tw)
    private bool _autoArcByExtrusionCheckBoxChecked; // Значение галочки "Auto Arc Enabled By Extrusion"  (AutoArc)
    private bool _runWithoutArcCheckBoxChecked; // Значение галочки "Run Without Arc"  (noArc)
    private bool _roEnableCheckBoxChecked; // Значение галочки "RO enable";  (GetRO))
    private bool _waveEnableCheckBoxChecked; // Значение галочки "Wave enable";  (GetWave)
    private string _waveEnableTextBoxText = ""; // Значение поля "Wave enable";  (WaveIndex)
    private string _weldShieldTextBoxText = "";  // Значение поля "Weld Shield";  (WS)
    private bool _weldShieldCheckBoxChecked;  // Значение галочки "Weld Shield";  (WSE)
    private bool _useWeldSpeedCheckBoxChecked; // Значение галочки "Use WELD_SPEED instead of feedrate"  (WeldSpeed)
    private string _robotUfTextBoxText = ""; // Значение поля "UF" в Robot Frame  (Uf)
    private string _robotUtTextBoxText = ""; // Значение поля "UT" в Robot Frame  (Ut)
    private string _positionerUfTextBoxTex = "";  // Значение поля "UF" в Positioner Frame  (PUF)
    private string _positionerUtTextBoxTex = "";  // Значение поля "UT" в Positioner Frame  (PUT)
    private string _xTextBoxText = ""; // Значение поля "X"  (X)
    private string _yTextBoxText = ""; // Значение поля "Y"  (Y)
    private string _zTextBoxText = ""; // Значение поля "Z"  (Z)
    private string _wTextBoxText = ""; // Значение поля "W"  (W)
    private string _pTextBoxText = ""; // Значение поля "P"  (P)
    private string _rTextBoxText = ""; // Значение поля "R"  (R)
    private bool _laserPassCheckBoxChecked; // Значение галочки "Laser pass"  (LaserPass)
    private bool _removeSmallStopStartCheckBoxChecked; // Значение галочки "Remove Small Stop Start"  (Checks)
    private string _checkingDistanceTextBoxText = ""; // Значение поля "Checking distance (mm)"  (CheckDist)
    private string _shortWristComboBoxText = ""; // Короткое значение комбо-бокса "Wrist" "F"/"N"  (W)
    private string _shortArmComboBoxText = "";  // Короткое значение комбо-бокса "Arm" "U"/"D"  (A)
    private string _shortBaseComboBoxText = "";  // Короткое значение комбо-бокса "Base" "T"/"B"  (B)
    private string _turnsJ1TextBox = "";  // Значение поля "Turns J1"  (j1)
    private string _turnsJ4TextBox = "";  // Значение поля "Turns J4"  (j4)
    private string _turnsJ6TextBox = "";  // Значение поля "Turns J6"  (j6)
    private string _j1OffsetTextBoxText = "";  // Значение поля "A(j1)  (j1o)
    private string _j2OffsetTextBoxText = "";  // Значение поля "B(j2)  (j2o)



    /**/
    private readonly Logger _logger = LoggerFactory.GetExistingOrCreateNewLogger("root_log");


    public ToRobotParser(IToObjectParser toObjectParser)
    {
        _toObjectParser = toObjectParser;
    }

    // Не очень красиво, но зато удобно и понятно где и что используется
    private void DecapsulateFormProperties()
    {
        _splitLayerText = _propertiesForParsers.SplitLayersTextBoxText;
        _autoSplitLayersCheckBoxChecked = _propertiesForParsers.AutoSplitLayersCheckBoxChecked;
        _fileNameWithoutExtension = _propertiesForParsers.BrowsedFileProperties.FileNameWithoutExtension;
        _outputDirectory = _propertiesForParsers.BrowsedFileProperties.OutputDirectory;
        _normalMovementTextBoxText = _propertiesForParsers.NormalMovementTextBoxText;
        _weldingMovementTextBoxText = _propertiesForParsers.WeldingMovementTextBoxText;
        _autoArcByExtrusionCheckBoxChecked = _propertiesForParsers.AutoArcByExtrusionCheckBoxChecked;
        _runWithoutArcCheckBoxChecked = _propertiesForParsers.RunWithoutArcCheckBoxChecked;
        _roEnableCheckBoxChecked = _propertiesForParsers.RoEnableCheckBoxChecked;
        _waveEnableCheckBoxChecked = _propertiesForParsers.WaveEnableCheckBoxChecked;
        _waveEnableTextBoxText = _propertiesForParsers.WaveEnableTextBoxText;
        _weldShieldTextBoxText = _propertiesForParsers.WeldShieldTextBoxText;
        _weldShieldCheckBoxChecked = _propertiesForParsers.WeldShieldCheckBoxChecked;
        _useWeldSpeedCheckBoxChecked = _propertiesForParsers.UseWeldSpeedCheckBoxChecked;
        _robotUfTextBoxText = _propertiesForParsers.RobotUfTextBoxText;
        _robotUtTextBoxText = _propertiesForParsers.RobotUtTextBoxText;
        _positionerUfTextBoxTex = _propertiesForParsers.PositionerUfTextBoxTex;
        _positionerUtTextBoxTex = _propertiesForParsers.PositionerUtTextBoxTex;
        _xTextBoxText = _propertiesForParsers.XTextBoxText;
        _yTextBoxText = _propertiesForParsers.YTextBoxText;
        _zTextBoxText = _propertiesForParsers.ZTextBoxText;
        _wTextBoxText = _propertiesForParsers.WTextBoxText;
        _pTextBoxText = _propertiesForParsers.PTextBoxText;
        _rTextBoxText = _propertiesForParsers.RTextBoxText;
        _laserPassCheckBoxChecked = _propertiesForParsers.LaserPassCheckBoxChecked;
        _removeSmallStopStartCheckBoxChecked = _propertiesForParsers.RemoveSmallStopStartCheckBoxChecked;
        _checkingDistanceTextBoxText = _propertiesForParsers.CheckingDistanceTextBoxText;
        _shortWristComboBoxText = _propertiesForParsers.ShortWristComboBoxText;
        _shortArmComboBoxText = _propertiesForParsers.ShortArmComboBoxText;
        _shortBaseComboBoxText = _propertiesForParsers.ShortBaseComboBoxText;
        _turnsJ1TextBox = _propertiesForParsers.TurnsJ1TextBox;
        _turnsJ4TextBox = _propertiesForParsers.TurnsJ4TextBox;
        _turnsJ6TextBox = _propertiesForParsers.TurnsJ6TextBox;
        _j1OffsetTextBoxText = _propertiesForParsers.J1OffsetTextBoxText;
        _j2OffsetTextBoxText = _propertiesForParsers.J2OffsetTextBoxText;
    }

    public RequiredPropertiesForParsers GetRequiredProperties()
    {
        return _propertiesForParsers;
    }

    /// <inheritdoc cref="IToRobotParserDocumentation.Parse()"/>
    public void Parse()
    {
        _logger.LogWithTime("ToRobotParser Parse START");
        
        RunPreviousParser();  // 1. Запуск логики предыдущего парсера
        
        _header.Clear();
        _footer.Clear();
        
        // InsertZeroPoint();  // 2. Видимо какой-то костыль, чтоб это все работало. //TODO Было в Симплифай трансляторе, убрано в ПоверМилл
        _currentLargeCoord.State = PrintStateEnum.Move;

        RunForEach();  // 3. Проход по всем точкам List<GCodePoint> _points и их парсинг
        
        _header.Add(": Arc End[1];");  // 4. Добавление в самый конец окончания Arc
        WriteLayerFile();  // 5. Запись последнего файла, если нет галочки "Autosplit layers" / единственного файла целиком, если есть галочка
        
        _logger.LogWithTime("ToRobotParser Parse END");
    }

    private void RunPreviousParser()
    {
        _points = _toObjectParser.Parse();  // Получение листа си-шарп объектов из предыдущего парсера
        _propertiesForParsers = _toObjectParser.GetRequiredProperties();
        DecapsulateFormProperties();  // Получение всех необходимых значений полей формы MainWindowForm 
    }
    
    private void InsertZeroPoint()
    {
        var zeroPoint = new GCodePoint();

        _positioner.J1 = 0;
        _positioner.J2 = 0;
        zeroPoint.Positioner = _positioner;
        _points.Insert(0, zeroPoint);
    }

    private void RunForEach()
    {
        foreach (var point in _points)
        {
            ResolveLayerFinishBySplitLayerTextBox(); // Если нет галочки "Autosplit layers", сохраняет слой по достижении _pointCounter == Split layers Text 

            _previousLargeCoord = _currentLargeCoord;  // Вытаскивание следующих координат из point
            _currentLargeCoord = point.LargeCoordinates;
        
            ResolveArc(); // Логика, если есть галочка "Auto arc enable by extrusion". Добавление начала и конца Arc в _header
            SomeAngleShit();  // Какая-то дикая логика обработки галочки WeldShield. По моим прикидкам, может иногда добавлять "Arc start[2]"
            
            ++_pointCounter;
            
            if (point.Movement)
            {
                ExchangeCoordinates(); // Смена предыдущих и текущих координат
                AddInfoToHeader();  // Заполнение _header (WELD_SPEED / FeedRate, Normal Movement / Welding Movement)
                
                _previousArcEnd = false;  // Можно попробовать отследить логику этих штук через ResolveArc() и AddInfoToHeader()
                _previousArcStart = false;
                
                CoordRotate(_positioner.J1, 0, _positioner.J2); // Якобы не работает
                AddInfoToFooter();  // Заполнение _footer (UF, UT, X, Y, Z, W, P, R, J1, J2)
            }

        }
    }

    private void ResolveLayerFinishBySplitLayerTextBox()
    {
        if (_pointCounter >= Convert.ToInt32(_splitLayerText) && !_autoSplitLayersCheckBoxChecked)
        {
            _header.Add(": Arc End[1];");
            WriteLayerFile();
            _pointCounter = 0;
        }
    }

    private void ResolveArc()
    {
        if (_runWithoutArcCheckBoxChecked) return; // Если нет галочки "Run Without Arc"
        if (_autoArcByExtrusionCheckBoxChecked) // Если есть галочка "Auto arc enable by extrusion"
        {
            if (_currentLargeCoord.State == PrintStateEnum.Move &&
                _previousLargeCoord.State == PrintStateEnum.Printing) // При смене состояния добавить соответствующие строки
            {
                AddArcEnd();
                _previousArcEnd = true;
            }

            if (_currentLargeCoord.State == PrintStateEnum.Printing && _previousLargeCoord.State == PrintStateEnum.Move)
            {
                AddArcStart();
                _previousArcStart = true;
            }
        }

        ParseArcEnabled(); //  По идее, тело этого метода никогда не используется
    }

    private void AddArcEnd()
    {
        _header.Add(": Arc End[1];");
        if (_roEnableCheckBoxChecked)
            _header.Add(": RO[1]=OFF;");
        if (_waveEnableCheckBoxChecked)
            _header.Add($": Weave End[{_waveEnableTextBoxText}];");
        
    }

    private void AddArcStart()
    {
        _header.Add(": Arc Start[1];");
        if (_roEnableCheckBoxChecked)
            _header.Add(": RO[1]=ON;");
        if (_waveEnableCheckBoxChecked)
            _header.Add($": Weave Sine[{_waveEnableTextBoxText}];");
    }

    private void ParseArcEnabled()
    {
        if (_arcEnabled == 1) //TODO Возможная ошибка: _arcEnabled изначально 0, нигде более значение не меняется, поэтому логика этого метода скорей всего никогда не запускается
        {
            _header.Add(": Arc Start[1];");
            if (_waveEnableCheckBoxChecked) //TODO Возможная ошибка: Тут скорей всего должна быть RO галочка. Но я сохранил оригинальную логику
                _header.Add(": RO[1]=ON;");
            if (_waveEnableCheckBoxChecked)
                _header.Add(": Weave Sine[2];");
            _arcEnabled = 0;
            _previousArcStart = true;
        }

        if (_arcEnabled == 2)
        {
            _header.Add(": Arc End[1];");
            if (_waveEnableCheckBoxChecked) //TODO Возможная ошибка: Тут скорей всего должна быть RO галочка. Но я сохранил оригинальную логику
                _header.Add(": RO[1]=OFF;");
            if (_waveEnableCheckBoxChecked)
                _header.Add(": Weave End[2];");

            _arcEnabled = 0;
            _previousArcEnd = true;
        }
    }

    private void SomeAngleShit()  //TODO Возможная ошибка: тут творится какая-то дичь
    // В симплифай версии транслятора этого не было кстати
    {
        if (!_weldShieldCheckBoxChecked) return;
        var (x, y) = ResolveXY();

        var formWeldShieldValue = int.Parse(_weldShieldTextBoxText);  // Значение с поля Weld Shield
        
        bool _isNeed = false;
        if ((Math.Abs(_prevY - y) > formWeldShieldValue) || Math.Abs(_prevX - x) > formWeldShieldValue)  // Да, prevX prevY в оригинальной логике всегда ноль
        {
            _header.Add(": Arc start [2];"); // Это все, что максимум может запуститься
            _isNeed = true;  // И это тоже из оригинальной логики
        }
        else if (_isNeed )  // Эта штука точно никогда не должна запуститься
        {
            _header.Add(": Arc Start[1];");
            if (_waveEnableCheckBoxChecked)
                _header.Add(": RO[1]=ON;");
            if (_waveEnableCheckBoxChecked)
                _header.Add($": Weave Sine[{_waveEnableTextBoxText}];");
            _arcEnabled = 0;
            _previousArcStart = true;
        }
    }

    private KeyValuePair<float, float> ResolveXY()
    {
        float y = 180f + (GetAngle(_coordinates.R, _coordinates.P));
        float x = (GetAngle(_coordinates.W, _coordinates.R) + 90);
        if (y > 180)
            y -= 360;
        else if (y < -180)
            y += 360;
        if (x > 180)
            x -= 360;
        else if (x < -180)
            x += 360;
        return new KeyValuePair<float, float>(x, y);
    }
    
    private float GetAngle(float x, float y)
    {
        var a = Math.Atan2(y, x);
        a = a * 180 / Math.PI;
        return (float)(a);
    }

    private void ExchangeCoordinates()
    {
        _coordinates.X = _currentLargeCoord.X;
        _coordinates.Y = _currentLargeCoord.Y;
        _coordinates.Z = _currentLargeCoord.Z;
        _coordinates.W = _currentLargeCoord.A;
        _coordinates.P = _currentLargeCoord.B;
        _coordinates.R = _currentLargeCoord.C;

        _previousSimpleCoord = _currentSimpleCoord;
        _currentSimpleCoord = _coordinates;
    }

    private void AddInfoToHeader()
    {
        ResolveFeedRate(); // Обработка галочки "Use WELD_SPEED instead of feedrate". Добавление скорости/feedrate в _header
        RemoveSmallStopStartCheckBoxProcessing(); // Обработка галочки "Remove small stop-start". Удаление некоторых строк из _header
        
    }

    private void ResolveFeedRate()
    {
        var prefixLine = CreatePrefixLine();  // Начало строки
        ResolveTermination(); // В _termination сетается либо значение поля "Normal movement", либо "Welding movement"

        prefixLine += _termination;
        prefixLine += " ;";
        _header.Add(prefixLine);
    }

    private string CreatePrefixLine()
    {
        var line = _pointCounter + ": L P[" + _pointCounter + "] ";

        var value = _currentLargeCoord.FeedRate / 10 * (float)Convert.ToDouble(1);
        int feed = (int)Math.Round(value);

        if (_useWeldSpeedCheckBoxChecked &&
            ( /*_current.states.Contains("A") ||*/ _currentLargeCoord.State == PrintStateEnum.Printing))
        {
            line += "WELD_SPEED ";
        }
        else
        {
            line += feed;
            line += "cm/min";
            line += " ";
        }

        return line;
    }

    private void ResolveTermination()
    {
        if (_currentLargeCoord.State == PrintStateEnum.Move)
            _termination = _normalMovementTextBoxText; // Значение поля "Normal movement"
        if (_currentLargeCoord.State == PrintStateEnum.Printing)
            _termination = _weldingMovementTextBoxText; // Значение поля "Welding movement"
    }

    private void RemoveSmallStopStartCheckBoxProcessing()
    {
        if (_previousArcEnd && _previousArcStart)
        {
            if (CheckStartStop()) // Обработка галочки "Remove small stop-start"
            {
                RemoveSomeLines(); // Результат обработки
            }
        }
    }

    private bool CheckStartStop()
    {
        if (_removeSmallStopStartCheckBoxChecked)
        {
            var dist = Math.Pow((_currentSimpleCoord.X - _previousSimpleCoord.X), 2);
            dist += Math.Pow((_currentSimpleCoord.Y - _previousSimpleCoord.Y), 2);
            dist += Math.Pow((_currentSimpleCoord.Z - _previousSimpleCoord.Z), 2);
            dist = Math.Sqrt(dist);
            if (dist <= float.Parse(_checkingDistanceTextBoxText, _myCIintl))
                return true;
        }

        return false;
    }

    private void RemoveSomeLines()
    {
        _header[_pointCounter - 2] = "";
        _header[_pointCounter - 3] = "";
    }

    // не работает
    // Комментарий выше - оригинальный. Метод целиком скопирован на всякий случай.
    // Без каких-либо изменений
    private void CoordRotate(float angleX, float angleY, float angleZ)
    {
        var p = new Vector3(_coordinates.X, _coordinates.Y, _coordinates.Z);
        var a = new Vector3((float)0.0, (float)0.0, (float)-150.0);
        var m = new Matrix4X4();

        p -= a;
        m.Rotate(angleZ, new Vector3(0, 0, 1));
        m.Rotate(-angleX, new Vector3(1, 0, 0));
        m.Rotate(-angleY, new Vector3(0, 1, 0));
        p = TransformNormal(p, m);
        p += a;

        _coordinates.X = p.X;
        _coordinates.Y = p.Y;
        _coordinates.Z = p.Z;

        _coordinates.W += angleX;
        _coordinates.P += angleY;
        _coordinates.R += -angleZ;
    }

    // Скопировано без изменений
    private static Vector3 TransformNormal(Vector3 normal, Matrix4X4 matrix)
    {
        return new Vector3
        {
            X = normal.X * matrix[1, 1] + normal.Y * matrix[2, 1] + normal.Z * matrix[3, 1],
            Y = normal.X * matrix[1, 2] + normal.Y * matrix[2, 2] + normal.Z * matrix[3, 2],
            Z = normal.X * matrix[1, 3] + normal.Y * matrix[2, 3] + normal.Z * matrix[3, 3]
        };
    }

    private void AddInfoToFooter()
    {
        AddUfUt(); // Добавление UF, UT полей в _footer
        ParseAndAddXyzToFooter(); // Обработка полей "X", "Y", "Z". Добавление соответствующих строк в _footer
        ParseAndAddWprToFooter(); // Обработка полей "W", "P", "R". Добавление соответствующих строк в _footer
        _footer.Add("   GP2:");
        _footer.Add($"       UF : {_positionerUfTextBoxTex}, UT : {_positionerUtTextBoxTex},");
        ParseAndAddJ1J2ToFooter(); // Запись параметров J1, J2 в _footer. (Вроде как параметры типа А и В G-code команды "M")
        _footer.Add("};");
    }

    private void AddUfUt()
    {
        _footer.Add("P[" + _pointCounter + "] {");
        _footer.Add("   GP1:");
        var line = "       UF : " + _robotUfTextBoxText + ", UT : " + _robotUtTextBoxText +
                   $",     CONFIG: '{_shortWristComboBoxText} {_shortArmComboBoxText} {_shortBaseComboBoxText}, " +
                   $"{_turnsJ1TextBox}, {_turnsJ4TextBox}, {_turnsJ6TextBox}',";
        _footer.Add(line);
    }

    private void ParseAndAddXyzToFooter()
    {
        var value = float.Parse(_xTextBoxText, CultureInfo.InvariantCulture.NumberFormat) + _coordinates.X;
        Conv = Math.Round(value, 1).ToString(_myCIintl);
        var line = "      X = " + Conv + " mm, ";

        value = float.Parse(_yTextBoxText, CultureInfo.InvariantCulture.NumberFormat) + _coordinates.Y;
        Conv = Math.Round(value, 1).ToString(_myCIintl);
        line += "Y = " + Conv + " mm, ";

        value = float.Parse(_zTextBoxText, CultureInfo.InvariantCulture.NumberFormat) + _coordinates.Z;
        Conv = Math.Round(value, 1).ToString(_myCIintl);
        line += "Z = " + Conv + " mm,";

        _footer.Add(line);
    }

    private void ParseAndAddWprToFooter()
    {
        var value = float.Parse(_wTextBoxText, CultureInfo.InvariantCulture.NumberFormat) + _coordinates.W;
        Conv = Math.Round(value, 1).ToString(_myCIintl);
        var line = "      W = " + Conv + " deg, ";

        value = float.Parse(_pTextBoxText, CultureInfo.InvariantCulture.NumberFormat) + _coordinates.P;
        Conv = Math.Round(value, 1).ToString(_myCIintl);
        line += "P = " + Conv + " deg, ";

        value = float.Parse(_rTextBoxText, CultureInfo.InvariantCulture.NumberFormat) + _coordinates.R;
        Conv = Math.Round(value, 1).ToString(_myCIintl);
        line += "R = " + Conv + " deg";

        _footer.Add(line);
    }

    private void ParseAndAddJ1J2ToFooter()
    {
        var value = _positioner.J1 + int.Parse(_j1OffsetTextBoxText);
        Conv = Math.Round(value, 1).ToString(_myCIintl);
        if (!Conv.Contains(".")) Conv += ".0";
        var line = "      J1 = " + Conv + " deg, ";

        value = _positioner.J2 + int.Parse(_j1OffsetTextBoxText);  //TODO Возможная ошибка: Да, в оригинале тоже прибавлялось значение j1, а не j2
        Conv = Math.Round(value, 1).ToString(_myCIintl);
        if (!Conv.Contains(".")) Conv += ".0";
        line += "J2 = " + Conv + " deg ";

        _footer.Add(line);
    }


    /*
     * Записывает файл. Используется либо для записи каждого слоя, если нет галочки "Autosplit layers",
     * либо для записи всего файла целиком, если есть галочка "Autosplit layers"
     */
    private void WriteLayerFile()
    {
        ResolveDirectoryExist(); // 1. Создание директории под файл
        
        var starter = CreateStarter(); // 2. Что-то, что записывается в начало файла
        WriteEverything(starter); // 3. Запись всего, что есть 

        _header.Clear(); // 4. Очистка под новый слой, если не Autosplit layers.
        _footer.Clear();

        _layerNumber++;
    }

    private List<string> CreateStarter()
    {
        var starter = new List<string>();

        var line = "/PROG " + _fileNameWithoutExtension;

        if (_layerNumber > 0) line += "_" + _layerNumber;
        starter.Add(line);
        starter.Add("/ATTR");
        starter.Add("OWNER       = MNEDITOR;");
        starter.Add("CREATE      = DATE 100-11-20  TIME 09:43:21;");
        starter.Add("MODIFIED    = DATE 100-12-05  TIME 05:26:29;");
        line = "LINE_COUNT = " + _pointCounter + ";";
        starter.Add(line);
        starter.Add("PROTECT     = READ_WRITE;");
        starter.Add("TCD:  STACK_SIZE    = 0,");
        starter.Add("      TASK_PRIORITY = 50,");
        starter.Add("      TIME_SLICE    = 0,");
        starter.Add("      BUSY_LAMP_OFF = 0,");
        starter.Add("      ABORT_REQUEST = 0,");
        starter.Add("      PAUSE_REQUEST = 0;");
        starter.Add("DEFAULT_GROUP   = 1,1,*,*,*;");
        starter.Add("CONTROL_CODE    = 00000000 00000000;");
        starter.Add("/MN");

        return starter;
    }

    private void WriteEverything(List<string> starter)
    {
        
        
        var outputFilename = _fileNameWithoutExtension + "_" + _layerNumber + ".ls"; // Имя файла со слоем
        
        _logger.LogWithTime($"ToRobotParser Writing file: {_outputDirectory + outputFilename}");
        _logger.LogWithTime($"Обработано точек: {_pointCounter}/{_points.Count}");
        
        var sw = new StreamWriter(_outputDirectory + outputFilename);
        
        WriteStarter(sw, starter);
        WriteHeader(sw);

        sw.WriteLine("/POS");

        WriteFooter(sw);

        sw.WriteLine("/END");
        sw.Close();
    }

    private void ResolveDirectoryExist()
    {
        if (!Directory.Exists(_outputDirectory))
        {
            Directory.CreateDirectory(_outputDirectory);
        }
    }

    private static void WriteStarter(TextWriter sw, List<string> starter)
    {
        foreach (var line in starter)
            sw.WriteLine(line);
    }

    private void WriteHeader(TextWriter sw)
    {
        foreach (var line in _header)
            sw.WriteLine(line);
    }

    private void WriteFooter(TextWriter sw)
    {
        foreach (var line in _footer)
            sw.WriteLine(line);
    }
}