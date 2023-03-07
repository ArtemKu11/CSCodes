using System.Diagnostics;
using GCodeTranslator.Forms.MainWindow;
using GCodeTranslator.Forms.RobotConnectionWindow;
using GCodeTranslator.Parsing.PostProcessors.AfterAllPostProcessor;
using GCodeTranslator.Parsing.PostProcessors.AngleFixPostProcessor;
using GCodeTranslator.Parsing.PostProcessors.SlicePostProcessor;
using GCodeTranslator.Parsing.TpConverter;
using GCodeTranslator.Service.MainWindow;

namespace GCodeTranslator.CmdProcessRunner;


/// <summary>
/// Предназначен для облегчения поиска и идентификации запуска CMD процессов (питон скрипты и прочее)
/// </summary>
public interface IProcessRunner
{
    /// <summary>
    /// <para>
    /// Запускает Scripts\Slicer.py
    /// </para>
    /// Асинхронно: нет
    /// <para>
    /// Когда в коде: в <see cref="PythonSlicePostProcessor"/>
    /// </para>
    /// Когда в форме: внутри алгоритмов после нажатия кнопки "START" в <see cref="MainWindowForm"/>
    /// </summary>
    /// <param name="inputDirectory">Директория с .ls файлом после парсера</param>
    /// <param name="outputDirectory">Директория под .ls файлы после Slicer.py</param>
    /// <param name="laserPass">Значение галочки "Laser pass"</param>
    public void RunPythonSlicerProcess(string inputDirectory, string outputDirectory, bool laserPass);

    
    /// <summary>
    /// <para>
    /// Запускает процесс открытия директории с результатом парсинга
    /// </para>
    /// Асинхронно: да
    /// <para>
    /// Когда в коде: в <see cref="LastPostProcessor"/>
    /// </para>
    /// Когда в форме: внутри алгоритмов после нажатия кнопки "START" в <see cref="MainWindowForm"/>
    /// </summary>
    /// <param name="directory">Директория, которую необходимо открыть</param>
    public void RunOpenDirectoryProcess(string directory);

    /// <summary>
    /// <para>
    /// Запускает Python-сервер конвертера .ls в .tp
    /// </para>
    /// Асинхронно: да
    /// <para>
    /// Когда в коде: в конструкторе <see cref="MainWindowFormService"/>
    /// </para>
    /// Когда в форме: при запуске приложения
    /// </summary>
    public Process StartTpConvertingServer();

    /// <summary>
    /// <para>
    /// Запускает консольную команду "maketp"
    /// </para>
    /// Асинхронно: да
    /// <para>
    /// Когда в коде: <see cref="ToTpConverter"/>
    /// </para>
    /// Когда в форме: при нажатии "Экспорт в TP" с галочкой "Экспортировать один" в <see cref="RobotConnectionForm"/> / При нажатии "Конвертировать файл"
    /// во вкладке "Convert to .tp" в <see cref="MainWindowForm"/>
    /// </summary>
    /// <param name="filePath">Полный путь до файла</param>
    /// <param name="fileDirectory">Полный путь до директории этого файла (с Robot.ini)</param>
    public void RunConvertToTpOneFileProcess(string filePath, string fileDirectory);
    
    /// <summary>
    /// <para>
    /// Запускает Scripts\PankratovAngleFix.py
    /// </para>
    /// Асинхронно: нет
    /// <para>
    /// Когда в коде: <see cref="PythonAngleFixPostProcessor"/>
    /// </para>
    /// Когда в форме: по кнопке "START" с галочкой "Pankratov Angle Fix" / Во вкладке Angle Fix в <see cref="MainWindowForm"/>
    /// </summary>
    /// <param name="fileDirectory">Директория с .ls файлами</param>
    /// <param name="maxAngleValue">Значение с поля "Максимальное значение угла"</param>
    /// <param name="criticalAngleDifference">Значение с поля "Критическая разница углов"</param>
    /// <param name="cmdRequired">Требуется ли запуск в виде отдельного процесса cmd.exe</param>
    public void RunPythonAngleFixProcess(string fileDirectory, string maxAngleValue, string criticalAngleDifference, bool cmdRequired);
}