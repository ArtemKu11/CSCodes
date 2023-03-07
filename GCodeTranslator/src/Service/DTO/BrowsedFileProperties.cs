namespace GCodeTranslator.Service.DTO;

/// <summary>
/// ДТОшка с выбранным файлом для парсинга из G-кодов в .ls, содержащая свойства файла (пути, директории)
/// </summary>
public struct BrowsedFileProperties
{
    public string FilePath;  // Полный путь до файла
    public string FileNameWithoutExtension;  // Название файла без разрешения
    public string OutputDirectory;  // Полный путь до директории с результатом парсинга
}