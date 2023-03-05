using GCodeTranslator.CmdProcessRunner;
using GCodeTranslator.Parsing.DTO;
using GCodeTranslator.Parsing.ObjectToRobotParser;
using GCodeTranslator.Utils.LogUtils;

namespace GCodeTranslator.Parsing.PostProcessors.AfterAllPostProcessor;

public class LastPostProcessor : IPostProcessor
{
    private readonly IToRobotParser? _previousParser;
    private readonly IPostProcessor? _previousPostProcessor;
    private RequiredPropertiesForParsers? _propertiesForParsers;
    private readonly Logger _logger = LoggerFactory.GetExistingOrCreateNewLogger("root_log");


    public LastPostProcessor(IToRobotParser toRobotParser)
    {
        _previousParser = toRobotParser;
    }

    public LastPostProcessor(IPostProcessor previousPostProcessor)
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
        RunFinalProcedures();
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

    private void RunFinalProcedures()
    {
        _logger.LogWithTime("LastPostProcessor RunFinalProcedures START");
        
        var outputDirectory = _propertiesForParsers?.BrowsedFileProperties.OutputDirectory;
        MessageBox.Show("done");
        if (outputDirectory != null) new ProcessRunner().RunOpenDirectoryProcess(outputDirectory);
        
        _logger.LogWithTime("LastPostProcessor RunFinalProcedures END");
    }
}