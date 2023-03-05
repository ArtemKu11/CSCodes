namespace GCodeTranslator.Service.DTO;

public struct BrowsedFileProperties
{
    public string FilePath;  // Полный путь до файла
    public string FileNameWithoutExtension;  // Название файла без разрешения
    public string OutputDirectory;  // Полный путь до директории с результатом парсинга
}