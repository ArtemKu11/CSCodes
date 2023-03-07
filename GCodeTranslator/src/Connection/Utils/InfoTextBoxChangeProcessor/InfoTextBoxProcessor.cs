using GCodeTranslator.Forms.RobotConnectionWindow;

namespace GCodeTranslator.Connection.Utils.InfoTextBoxChangeProcessor;

/// <summary>
/// Класс, предназначенный для Thread-safety изменения текста в _infoTextBox в <see cref="RobotConnectionForm"/>
/// в не зависимости от потока, в котором используется
/// </summary>
public class InfoTextBoxProcessor
{
    private readonly object _locker = new();
    private TextBox? _infoTextBox;
    private RobotConnectionForm? _robotConnectionForm;
    private string _lastPrintedMessage = "";
    public string LastPrintedMessage
    {
        get
        {
            lock (_locker)
            {
                return _lastPrintedMessage;
            }
        }
    }


    public void SetRobotConnectionFormProperties(RobotConnectionForm robotConnectionForm, TextBox infoTextBox)
    {
        _robotConnectionForm = robotConnectionForm;
        _infoTextBox = infoTextBox;
    }
    
    public void PrintLine(string line, bool checkDuplicate = false)
    {
        if (!(checkDuplicate && !DuplicateCheck(line)))
        {
            PrintIntoTextBox(line);
        }
        
    }

    private void PrintIntoTextBox(string line)
    {
        lock (_locker)
        {
            if (_robotConnectionForm is { IsDisposed: false })
            {
                if (_robotConnectionForm.InvokeRequired)
                {
                    _robotConnectionForm.Invoke(() =>
                    {
                        _infoTextBox?.AppendText(line + "\r\n");
                        _lastPrintedMessage = line;
                    });
                }
                else
                {
                    _infoTextBox?.AppendText(line + "\r\n");
                    _lastPrintedMessage = line;
                }
            }
        }
    }

    private bool DuplicateCheck(string line)
    {
        return !line.Equals(LastPrintedMessage);
    }
}