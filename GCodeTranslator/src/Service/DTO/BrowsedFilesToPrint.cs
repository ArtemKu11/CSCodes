namespace GCodeTranslator.Service.DTO;

public struct BrowsedFilesToPrint
{
    public string Path;  // Полный путь до директории
    public string DirectoryName;
    public int Count;
    public List<string> FilesList;

    public override string ToString()
    {
        return $"Path: {Path}\n" +
               $"DirectoryName: {DirectoryName}\n" +
               $"Count: {Count}\n" +
               $"FilesList: {FilesList.Count}";
    }
}