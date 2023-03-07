using System.Diagnostics;
using NetMQ;
using NetMQ.Sockets;

namespace GCodeTranslator.CmdProcessRunner;

/// <inheritdoc cref="IProcessRunner"/>
public class ProcessRunner : IProcessRunner
{
    private static string _pythonPath = "python";

    public static string PythonPath
    {
        set => _pythonPath = value ?? throw new ArgumentNullException(nameof(value));
    }


    /// <inheritdoc cref="IProcessRunner.RunPythonSlicerProcess"/>
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
        processStartInfo.FileName = _pythonPath;
        processStartInfo.Arguments = cmdString;
        sliceProcess.StartInfo = processStartInfo;
        return sliceProcess;
    }


    
    
    /// <inheritdoc cref="IProcessRunner.RunOpenDirectoryProcess"/>
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

    
    /// <inheritdoc cref="IProcessRunner.StartTpConvertingServer"/>
    public Process StartTpConvertingServer()
    {
        ProcessStartInfo processStartInfo = new ProcessStartInfo();
        string cmdString = @"Scripts\convert_2_tp_zmq.py";

        Process tpServerProcess = new Process();
        processStartInfo.FileName = _pythonPath;
        processStartInfo.Arguments = cmdString;
        tpServerProcess.StartInfo = processStartInfo;
        tpServerProcess.Start();
        return tpServerProcess;
    }

    /// <inheritdoc cref="IProcessRunner.RunConvertToTpOneFileProcess"/>
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

    /// <inheritdoc cref="IProcessRunner.RunPythonAngleFixProcess"/>
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
            processStartInfo.FileName = _pythonPath;
        }
        processStartInfo.Arguments = cmdString;

        var angleFixProcess = new Process();
        angleFixProcess.StartInfo = processStartInfo;

        angleFixProcess.Start();
        angleFixProcess.WaitForExit();
    }
}