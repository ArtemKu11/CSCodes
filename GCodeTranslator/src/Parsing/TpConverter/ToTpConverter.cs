using System.Diagnostics;
using GCodeTranslator.CmdProcessRunner;

namespace GCodeTranslator.Parsing.TpConverter;

public class ToTpConverter
{
    private static Process? _toTpConverterProcess;
    
    public static void StartTpConvertingServer()
    {
        _toTpConverterProcess = new ProcessRunner().StartTpConvertingServer();
    }

    public static void EliminateTpConvertingProcess()
    {
        _toTpConverterProcess?.Kill();
    }
    
    public string ConvertAllFiles(string fileDirectory)
    {
        return new ProcessRunner().RunConvertToTpAllFilesProcess(fileDirectory);
    }
    
    
    public void ConvertOneFile(string filePath)
    {
        var fileDirectory = filePath.Substring(0, filePath.LastIndexOf('\\'));
        if (!File.Exists(fileDirectory + "\\robot.ini")) return;
        
        WriteInfoInRobotIni(fileDirectory);
        new ProcessRunner().RunConvertToTpOneFileProcess(filePath, fileDirectory);
    }

    private void WriteInfoInRobotIni(string fileDir)
    {
        var file = new StreamWriter(fileDir + "\\robot.ini");
        file.WriteLine("[WinOLPC_Util]");
        file.WriteLine("Robot=\\C\\Users\\02Robot\\Documents\\My Workcells\\Fanuc_002\\Robot_1");
        file.WriteLine("Version=V7.70-1");
        file.WriteLine(@"Path=C:\Program Files (x86)\FANUC\WinOLPC\Versions\V770-1\bin");
        file.WriteLine(@"Support=C:\Users\02Robot\Documents\My Workcells\Fanuc_002\Robot_1\support");
        file.WriteLine(@"Output=C:\Users\02Robot\Documents\My Workcells\Fanuc_002\Robot_1\output");
        file.Close();
    }
    
}