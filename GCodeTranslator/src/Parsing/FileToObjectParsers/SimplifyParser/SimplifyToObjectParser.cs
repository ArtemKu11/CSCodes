using System.Globalization;
using System.Text.RegularExpressions;
using GCodeTranslator.Parsing.DTO;
using GCodeTranslator.Utils.LogUtils;

namespace GCodeTranslator.Parsing.FileToObjectParsers.SimplifyParser;

/// <summary>
/// <para>
/// Первый парсер в цепочке из двух парсеров
/// </para>
/// <para>
/// Парсит файл из Симплифая в лист <see cref="GCodePoint"/> для последующей его передачи следующему парсеру.
/// </para>
/// <para>
/// Для использования необходимо передать объект класса <see cref="RequiredPropertiesForParsers"/> в конструктор и вызвать метод <see cref="Parse"/>
/// </para>
/// <para>
/// Алгоритм примерно следующий (но это не точно):
/// </para>
/// <para>
/// 1. Разом регулярными выражениями читается весь файл. Формируется MatchCollection со всеми строками
/// </para>
/// 2. Запускается forEach по строкам
/// <para>
/// 3. Из каждой строки получают массив
/// </para>
/// 4. Массив приводят к объекту <see cref="GCodeLine"/> (Command, CommandValue, FeedRate, Parameters)
/// <para>
/// 5. Используя объект <see cref="GCodeLine"/>, формируются объекты <see cref="LargeCoordinates"/> (x, y, z, a, b, c, FeedRate, States (Printing/Move)),
/// </para>
/// 6. Из <see cref="LargeCoordinates"/>, <see cref="Positioner"/>, <see cref="_movement"/> формируется GCodePoint и добавляется в массив
/// <para>
/// 7. п.3 - п.6 повторяются для каждой строки
/// </para>
/// 8. List of <see cref="GCodeLine"/> отдается другому парсеру
/// </summary>
public class SimplifyToObjectParser : IToObjectParser
{
    private readonly RequiredPropertiesForParsers _formProperties;
    private readonly string _filePath;  // Файл, который парсится
    private readonly List<GCodePoint> _gCodePoints = new();  // Тут хранятся G-коды в виде объектов. Результат парсинга
    private readonly bool _autoArcEnabledCheckBoxChecked;  // Значение с галочки "Auto Arc Enable By Extrusion"
    private int _arcEnabled;  // Хз что это, просто скопировал
    private Positioner _positioner;  // Хз что это, просто скопировал
    private LargeCoordinates _current, _previous;  // Хз что это, просто скопировал
    private bool _movement;  // Хз что это, просто скопировал
    
    private readonly Logger _logger = LoggerFactory.GetExistingOrCreateNewLogger("root_log");

    
    public SimplifyToObjectParser(RequiredPropertiesForParsers formProperties)
    {
        _formProperties = formProperties;
        _filePath = formProperties.BrowsedFileProperties.FilePath;
        _autoArcEnabledCheckBoxChecked = formProperties.AutoArcByExtrusionCheckBoxChecked;
    }
    
    /*
     * Для получения свойств формы следующим парсером
     */
    public RequiredPropertiesForParsers GetRequiredProperties()
    {
        return _formProperties;
    }
    
    
    /// <summary>
    /// Метод является зарефракторенной копией огромного метода из оригинальной версии.
    /// Что тут происходит - не понятно. Все, что написано в комментариях - догадки
    /// <para>
    /// Запускает логику парсинга файла Симплифай формата, определенного в <see cref="RequiredPropertiesForParsers"/> в конструкторе
    /// </para>
    /// <para>
    /// Парсит в List of <see cref="GCodePoint"/> и отдает следующему парсеру
    /// </para>
    /// </summary>
    /// <returns>Возвращает результат парсинга в виде C#-объектов - List of <see cref="GCodePoint"/></returns>
    public List<GCodePoint> Parse()
    {
        _logger.LogWithTime("SimplifyToObjectParser Parse START");
        
        var matches = GetAllGCodeMatches();  // 1. Получить вообще все G-коды
        
        foreach (Match match in matches)
        {
            if (match.Value[0] == ';')  // 2. Отсеять какие-то левые
            {
                continue;
            }
            
            var line = match.Groups.Values.Where(c => c.Name != "0").ToArray();  // 3. Получить каждую строку в виде массива
            ParseLine(line);  // 4. Распарсить каждую строку
        }

        _logger.LogWithTime("SimplifyToObjectParser Parse END");
        _logger.Log($"Обработано точек: {_gCodePoints}");
        return _gCodePoints;
    }

    private MatchCollection GetAllGCodeMatches()
    {
        var sr = new StreamReader(_filePath);
        var matches = Regex.Matches(sr.ReadToEnd(), @"(?!; *.+)(G|M|T|g|m|t)(\d+)(([ \t]*(?!G|M|g|m)\w('.*'|([-\d\.]*)))*)[ \t]*(;[ \t]*(.*))?|;[ \t]*(.+)");
        sr.Close();
        return matches;
    }

    private void ParseLine(Group[] line)
    {
        var gCodeLine = GetGCodeLineObject(line);  // 1. Получить строку в виде объекта с полями: Command, CommandValue, FeedRate, Parameters
        ParseMCommand(gCodeLine);  // 2. Действия, если команда "М"
        ParseGCommand(gCodeLine);  // 3. Действия, если команда "G"
        
    }

    private GCodeLine GetGCodeLineObject(Group[] line)
    {
        var lineCommand = line[0].Value;
        var lineCommandValue = int.Parse(line[1].Value);
        var lineParameters = ParseLineParameters(line);
        
        var gCodeLine = new GCodeLine
        {
            Command = lineCommand,
            CommandValue = lineCommandValue,
            Parameters = lineParameters
        };
        
        return gCodeLine;
    }

    private static List<GCodeLineParameter> ParseLineParameters(Group[] line)
    {
        var matches = Regex.Matches(line[2].Value,@"((?!\d)\w+?)('.*'|(\d+\.?)+|-?\d*\.?\d*)");  // 1. Получить все параметры в строке
        var parameters = new List<GCodeLineParameter>();
        
        foreach (Match match in matches)
        {
            var parameter = match.Groups.Values.Where(c => c.Name != "0").ToArray();  // 2. Для каждого совпадения получить массив <type, value>
            var type = parameter[0].Value.ToUpper();
            var value = parameter[1].Value;
            parameters.Add(new GCodeLineParameter(type, float.Parse(value, CultureInfo.InvariantCulture.NumberFormat)));  // 3. Добавить в лист
        }

        return parameters;
    }

    private void ParseMCommand(GCodeLine gCodeLine)
    {
        if (gCodeLine.Command == "M" && (gCodeLine.CommandValue == 7))
        {
            _arcEnabled = 1;
        } //доп охлаждение

        if (gCodeLine.Command == "M" && (gCodeLine.CommandValue == 8))
        {
            _arcEnabled = 2;
        } //осн охлаждение

        if (gCodeLine.Command == "M" && (gCodeLine.CommandValue == 800))
        {
            foreach (var param in gCodeLine.Parameters)
            {
                switch (param.ParamType)
                {
                    case "A":
                        _positioner.J1 = param.Value;
                        break;
                    case "B":
                        _positioner.J2 = param.Value;
                        break;
                }
            }
        }
    }

    private void ParseGCommand(GCodeLine gCodeLine)
    {
        if (gCodeLine.Command == "G")
        {
            // Linear move
            _previous = _current;
            _movement = false;

            ParseParametersForGCommand(gCodeLine);  // 1. Распарсить все параметры (x, y, z, a, b, c, f, e)
            ResolvePointCreation(gCodeLine);  //  2. Создать объект в лист G-кодов (_gCodePoints)
        }
    }

    private void ParseParametersForGCommand(GCodeLine gCodeLine)
    {
        foreach (var param in gCodeLine.Parameters)
        {
            switch (param.ParamType) // Видимо, это парсинг каждого параметра в строке, которая начинается на "G"
            {

                case "X":
                    _current.X = param.Value;
                    _movement = true;
                    break;
                case "Y":
                    _current.Y = param.Value;
                    _movement = true;
                    break;
                case "Z":
                    _current.Z = param.Value;
                    _movement = true;
                    break;
                case "A":
                    _current.A = param.Value;
                    _movement = true;
                    break;
                case "B":
                    _current.B = param.Value;
                    _movement = true;
                    break;
                case "C":
                    _current.C = param.Value;
                    _movement = true;
                    break;
                case "F":
                    _current.FeedRate = (int)param.Value;
                    break;
                case "E":
                    _current.E = param.Value;
                    break;
            }
        }
    }

    private void ResolvePointCreation(GCodeLine gCodeLine)
    {
        if (gCodeLine.CommandValue == 1 || gCodeLine.CommandValue == 0)
        {
            // Linear move

            if (_autoArcEnabledCheckBoxChecked)
            {
                if (_current.E - _previous.E > 0) _current.State = PrintStateEnum.Printing;
                else _current.State = PrintStateEnum.Move;
            }
            GCodePoint point;
            point.LargeCoordinates = _current;
            point.Movement = _movement;
            point.Positioner = _positioner;
            _gCodePoints.Add(point);
        }
    }
}