namespace GCodeTranslator.Service.DTO;

/// <summary>
/// ДТОшка с выбранными файлам .tp для отправки на печать. Содержит их названия, путь, директорию
/// </summary>
public struct BrowsedFilesToPrint
{
    public string Path;  // Полный путь до директории
    public string DirectoryName;  // Последняя папка в пути
    public int Count;  // Количество файлов. По факту FilesList.Count()
    public List<string> FilesList;  // Содержит названия файлов .tp. По факту - выпадающий список _layersComboBox в RobotConnectionForm

    public override string ToString()
    {
        return $"Path: {Path}\n" +
               $"DirectoryName: {DirectoryName}\n" +
               $"Count: {Count}\n" +
               $"FilesList: {FilesList.Count}";
    }
}