using GCodeTranslator.Parsing.DTO;

namespace GCodeTranslator.Parsing.PostProcessors;

public interface IPostProcessor
{
    RequiredPropertiesForParsers? GetRequiredProperties();
    
    void PostProcess();
}