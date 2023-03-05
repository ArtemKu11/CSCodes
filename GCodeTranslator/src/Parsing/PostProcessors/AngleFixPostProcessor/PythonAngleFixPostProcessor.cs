using GCodeTranslator.CmdProcessRunner;
using GCodeTranslator.Parsing.DTO;
using GCodeTranslator.Parsing.ObjectToRobotParser;
using GCodeTranslator.Utils.LogUtils;

namespace GCodeTranslator.Parsing.PostProcessors.AngleFixPostProcessor;

public class PythonAngleFixPostProcessor : IPostProcessor
{
    private readonly IToRobotParser? _previousParser;
    private readonly IPostProcessor? _previousPostProcessor;
    private RequiredPropertiesForParsers? _propertiesForParsers;
    private readonly Logger _logger = LoggerFactory.GetExistingOrCreateNewLogger("root_log");


    public PythonAngleFixPostProcessor(IToRobotParser toRobotParser)
    {
        _previousParser = toRobotParser;
    }

    public PythonAngleFixPostProcessor(IPostProcessor previousPostProcessor)
    {
        _previousPostProcessor = previousPostProcessor;
    }
    public RequiredPropertiesForParsers? GetRequiredProperties()
    {
        return _propertiesForParsers;
    }

    public void PostProcess()
    {
        RunPreviousLogic();
        GetPropertiesFromPrevious();
        RunPythonAngleFix();
    }
    
    private void RunPreviousLogic()
    {
        _previousParser?.Parse();
        _previousPostProcessor?.PostProcess();
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

    private void RunPythonAngleFix()
    {
        var angleScriptCheckBoxChecked = _propertiesForParsers?.AngleScriptCheckBoxChecked;
        if (angleScriptCheckBoxChecked is true)
        {
            _logger.LogWithTime("PythonAngleFixPostProcessor RunPythonAngleFix START");
            
            var inputDirectory = ResolveInputDirectory();
            var maxAngleValueTextBoxText = _propertiesForParsers?.MaxAngleValueTextBoxText;
            var criticalAngleDifferenceTextBoxText = _propertiesForParsers?.CriticalAngleDifferenceTextBoxText;
            
            if (maxAngleValueTextBoxText != null && criticalAngleDifferenceTextBoxText != null)
            {
                new ProcessRunner().RunPythonAngleFixProcess(inputDirectory, maxAngleValueTextBoxText, criticalAngleDifferenceTextBoxText);
            }
            
            _logger.LogWithTime("PythonAngleFixPostProcessor RunPythonAngleFix END");
        }
    }

    private string ResolveInputDirectory()
    {
        var inputDirectory = _propertiesForParsers?.BrowsedFileProperties.OutputDirectory;
        string outputDirectory = "";
        if (inputDirectory != null)
        {
            outputDirectory = inputDirectory[..inputDirectory.LastIndexOf('\\')];
            outputDirectory = outputDirectory[..(outputDirectory.LastIndexOf('\\') + 1)] + "layer";
        }

        return outputDirectory;
    }
}