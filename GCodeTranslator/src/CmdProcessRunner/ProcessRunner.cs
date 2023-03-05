using System.Diagnostics;
using NetMQ;
using NetMQ.Sockets;

namespace GCodeTranslator.CmdProcessRunner;


/*
 * Предназначен для облегчения поиска и идентификации запуска CMD процессов (питон скрипты и прочее)
 */
public class ProcessRunner : IProcessRunner
{   
    /*
     * Запускает Scripts\Slicer.py
     * Асинхронно: нет
     * Когда в коде: в конце работы метода Parse() ToRobotParser
     * Когда в форме: внутри алгоритмов после нажатия кнопки "START" в MainWindowForm
     * Принимает:
     *  inputDirectory - директория с .ls файлом после парсера
     *  outputDirectory - директория под .ls файлы после Slicer.py
     *  laserPass - значение галочки Laser pass
     */
    public void RunPythonSlicerProcess(string inputDirectory, string outputDirectory, bool laserPass)
    {
        var pythonSliceProcess = CreatePythonSliceProcess(inputDirectory, outputDirectory, laserPass);
        pythonSliceProcess.Start();
        
        pythonSliceProcess.WaitForExit();
    }

    private Process CreatePythonSliceProcess(string inputDirectory, string outputDirectory, bool laserPass)
    {
        ProcessStartInfo processStartInfo = new ProcessStartInfo();
        string cmdString = $"Scripts\\Slicer.py {inputDirectory} {outputDirectory}";
        if (laserPass)
            cmdString += " d";
        Process sliceProcess = new Process();
        processStartInfo.FileName = "python";
        processStartInfo.Arguments = cmdString;
        sliceProcess.StartInfo = processStartInfo;
        return sliceProcess;
    }


    
    
    /*
     * Запускает процесс открытия директории с результатом парсинга
     * Асинхронно: да
     * Когда в коде: в конце работы метода Parse() ToRobotParser
     * Когда в форме: внутри алгоритмов после нажатия кнопки "START" в MainWindowForm
     * Принимает:
     *  directory - директория, которую необходимо открыть
     */
    public void RunOpenDirectoryProcess(string directory)
    {
        var process = new Process();
        var processStartInfo = new ProcessStartInfo
        {
            CreateNoWindow = true,
            WindowStyle = ProcessWindowStyle.Normal,
            FileName = "explorer",
            Arguments = @"/n, /select, " + directory
        };
        process.StartInfo = processStartInfo;
        process.Start();
    }

    public Process StartTpConvertingServer()
    {
        ProcessStartInfo processStartInfo = new ProcessStartInfo();
        string cmdString = @"Scripts\convert_2_tp_zmq.py";

        Process tpServerProcess = new Process();
        processStartInfo.FileName = "python";
        processStartInfo.Arguments = cmdString;
        tpServerProcess.StartInfo = processStartInfo;
        tpServerProcess.Start();
        return tpServerProcess;
    }

    public void RunConvertToTpOneFileProcess(string filePath, string fileDirectory)
    {
        var startInfo = new ProcessStartInfo()
        {
            FileName = "cmd.exe",
            Arguments = @$"/k ""maketp {filePath.Substring(filePath.LastIndexOf('\\') + 1)}""",
            WorkingDirectory = fileDirectory,
            UseShellExecute = true
        };
        var process = Process.Start(startInfo);
        // process?.WaitForExit();
    }

    public string RunConvertToTpAllFilesProcess(string fileDirectory)
    {
        using (var client = new RequestSocket())
        {
            client.Connect($"tcp://localhost:5001");
            client.SendFrame($"path${fileDirectory}");
            var message = client.ReceiveFrameString();
            return message;
        }
    }

    public void RunPythonAngleFixProcess(string fileDirectory, string maxAngleValue, string criticalAngleDifference, bool cmdRequired = false)
    {
        var processStartInfo = new ProcessStartInfo();
        string cmdString;
        if (cmdRequired)
        {
            cmdString = @$"/k ""python Scripts\PankratovAngleFix.py {fileDirectory} {maxAngleValue} {criticalAngleDifference}""";
            processStartInfo.FileName = "cmd.exe";
        }
        else
        {
            cmdString = $"Scripts\\PankratovAngleFix.py {fileDirectory} {maxAngleValue} {criticalAngleDifference}";
            processStartInfo.FileName = "python";
        }
        processStartInfo.Arguments = cmdString;

        var angleFixProcess = new Process();
        angleFixProcess.StartInfo = processStartInfo;

        angleFixProcess.Start();
        angleFixProcess.WaitForExit();
    }
}