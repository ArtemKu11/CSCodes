using GCodeTranslator.CmdProcessRunner;
using GCodeTranslator.Forms.MainWindow;
using GCodeTranslator.Parsing.TpConverter;

namespace GCodeTranslator
{
    
    /*
     * Приложение переписано с оригинального транслятора
     * Архитектурно - это новое приложение
     * Функционал - сохранен полностью
     * Часть логики скопирована полностью с целью сохранения функционала (нереально понять что там происходит)) ). Помечена соответствующим комментарием
     * 
     * Для некоторых классов есть интерфейсы с комментариями
     * Над каждым классом есть комментарий с описанием
     */
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            var defaultPythonPath = "python";
            var venvPythonPath = @"Scripts\venv\Scripts\python";
            ProcessRunner.PythonPath = venvPythonPath;
            
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            Application.Run(new MainWindowForm()); // Показать основное окно
        }
    }
}