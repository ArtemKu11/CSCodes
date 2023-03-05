using System.Diagnostics;

namespace GCodeTranslator.CmdProcessRunner;


/*
 * Предназначен для облегчения поиска и идентификации запуска CMD процессов (питон скрипты и прочее)
 */
public interface IProcessRunner
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
    public void RunPythonSlicerProcess(string inputDirectory, string outputDirectory, bool laserPass);

    
    
    /*
     * Запускает процесс открытия директории с результатом парсинга
     * Асинхронно: да
     * Когда в коде: в конце работы метода Parse() ToRobotParser
     * Когда в форме: внутри алгоритмов после нажатия кнопки "START" в MainWindowForm
     * Принимает:
     *  directory - директория, которую необходимо открыть
     */
    public void RunOpenDirectoryProcess(string directory);

    public Process StartTpConvertingServer();

    public void RunConvertToTpOneFileProcess(string filePath, string fileDirectory);

    public string RunConvertToTpAllFilesProcess(string fileDirectory);

    public void RunPythonAngleFixProcess(string fileDirectory, string maxAngleValue, string criticalAngleDifference, bool cmdRequired);
}