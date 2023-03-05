namespace GCodeTranslator.Connection.DTO;

public class SendFileInfo
{
    private string _currentFileName = "None"; // директория текущего файла (Название текущего файла)
    private string _nextFilePath = "None"; // директория следующего файла при непрерывной печати
    private bool _isStart; //начать печать
    private bool _stopAfterLayer; // ожидать подтверждения продолжения печати
    private bool _isContinuous; // если печать не по слоям и в рамках одного файла высота меняется
    private bool _isLastFile; // true если последний файл в списке

    public string CurrentFileName
    {
        get { return _currentFileName; }
        set { _currentFileName = value; }
    }

    private string GetCurrentFilePathToString()
    {
        return $"current_file_path${_currentFileName}";
    }
    
    public bool IsStart
    {
        set { _isStart = value; }
    }
    
    private string GetIsStartToString()
    {
        return $"is_start${_isStart.ToString()}";
    }

    public bool StopAfterLayer
    {
        set { _stopAfterLayer = value; }
    }
    
    private string GetStopAfterLayerToString()
    {
        return $"stop_after_layer${_stopAfterLayer.ToString()}";
    }

    public bool IsContinuous
    {
        set { _isContinuous = value; }
    }

    private string GetIsContinuousToString()
    {
        return $"is_continuous${_isContinuous.ToString()}";
    }

    public bool IsLastFile
    {
        set { _isLastFile = value; }
    }
    
    private string GetIsLastFileToString()
    {
        return $"is_last_file${_isLastFile.ToString()}";
    }

    public string NextFilePath
    {
        set { _nextFilePath = value; }
    }
    
    private string GetNextFilePathToString()
    {
        return $"next_file_path${_nextFilePath}";
    }

    public override string ToString()
    {
        return GetCurrentFilePathToString() + ";" + GetIsStartToString() + ";" + GetStopAfterLayerToString() + ";" 
               + GetIsContinuousToString() + ";" + GetIsLastFileToString() + ";" + GetNextFilePathToString();
    }
}