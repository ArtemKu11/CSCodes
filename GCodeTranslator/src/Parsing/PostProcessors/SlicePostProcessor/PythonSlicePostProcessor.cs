using GCodeTranslator.CmdProcessRunner;
using GCodeTranslator.Parsing.DTO;
using GCodeTranslator.Parsing.ObjectToRobotParser;
using GCodeTranslator.Utils.LogUtils;

namespace GCodeTranslator.Parsing.PostProcessors.SlicePostProcessor;

/// <summary>
/// Запускается на директории с .ls файлом после парсинга. Запускает питон слайсер. Бьет .ls по слоям.
/// <para>
/// Работает по галочке "Autosplit layers"
/// </para>
/// </summary>
public class PythonSlicePostProcessor : IPostProcessor
{
    private readonly IToRobotParser? _previousParser;
    private readonly IPostProcessor? _previousPostProcessor;
    private RequiredPropertiesForParsers? _propertiesForParsers;
    private readonly Logger _logger = LoggerFactory.GetExistingOrCreateNewLogger("root_log");


    public PythonSlicePostProcessor(IToRobotParser toRobotParser)
    {
        _previousParser = toRobotParser;
    }

    public PythonSlicePostProcessor(IPostProcessor previousPostProcessor)
    {
        _previousPostProcessor = previousPostProcessor;
    }

    public RequiredPropertiesForParsers? GetRequiredProperties()
    {
        return _propertiesForParsers;
    }
    
    /// <summary>
    /// <para>Запускает предыдущую логику</para>
    /// <inheritdoc cref="PythonSlicePostProcessor"/>
    /// </summary>
    public void PostProcess()
    {
        RunPreviousLogic();
        GetPropertiesFromPrevious();
        var autoSplitLayersCheckBoxChecked = _propertiesForParsers?.AutoSplitLayersCheckBoxChecked;
        if (autoSplitLayersCheckBoxChecked is true)
        {
            RunSlicer();
        }
    }
    
    private void GetPropertiesFromPrevious()
    {
        _propertiesForParsers = _previousParser?.GetRequiredProperties();
        if (_propertiesForParsers != null)
        {
            return;
        }
        _propertiesForParsers = _previousPostProcessor?.GetRequiredProperties();
    }
    
    private void RunSlicer()
    {
        _logger.LogWithTime("PythonSlicePostProcessor RunSlicer START");
        
        var inputDirectory = _propertiesForParsers?.BrowsedFileProperties.OutputDirectory;
        var outputDirectory = "";
        if (inputDirectory != null)
        {
            outputDirectory = inputDirectory[..inputDirectory.LastIndexOf('\\')];
            outputDirectory = outputDirectory[..(outputDirectory.LastIndexOf('\\') + 1)] + "layer";
        }
        
        var laserPassChecked = _propertiesForParsers?.LaserPassCheckBoxChecked;
        
        if (laserPassChecked != null && inputDirectory != null)
        {
            new ProcessRunner().RunPythonSlicerProcess(inputDirectory, outputDirectory, laserPassChecked.Value);
        }
        
        _logger.LogWithTime("PythonSlicePostProcessor RunSlicer END");

    }

    private void RunPreviousLogic()
    {
        _previousParser?.Parse();
        _previousPostProcessor?.PostProcess();
    }
}