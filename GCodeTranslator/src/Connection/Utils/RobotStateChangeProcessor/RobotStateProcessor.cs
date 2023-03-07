using GCodeTranslator.Forms.MainWindow;
using GCodeTranslator.Forms.RobotConnectionWindow;

namespace GCodeTranslator.Connection.Utils.RobotStateChangeProcessor;

/// <summary>
/// Класс, предназначенный для Thread-safety изменения текста свойств, отражающих состояние робота (_robotStateCell
/// в <see cref="MainWindowForm"/>, _robotStateLabel в <see cref="RobotConnectionForm"/>),
/// в не завсисимости от потока, в котором используется
/// </summary>
public class RobotStateProcessor
{
    private readonly object _locker = new();
    
    
    /*
     * Данные с MainWindowForm
     */
    private DataGridViewCell? _robotStateCell;
    private MainWindowForm? _mainWindowForm;  // До последнего не хотел тащить за собой формы, но ThreadSafety требует. Используется только в этом классе
    
    
    /*
     * Данные с RobotConnectionForm
     */
    private Label? _robotStateLabel;
    private RobotConnectionForm? _robotConnectionForm;  // Используется только в этом классе
    
    
    
    // Сначала получить данные с Main формы
    public void SetMainFormProperties(MainWindowForm mainWindowForm, DataGridViewCell robotStateCell)
    {
        _mainWindowForm = mainWindowForm;
        _robotStateCell = robotStateCell;
    }

    // Потом с RobotConnectionForm
    public void SetRobotConnectionFormProperties(RobotConnectionForm robotConnectionForm, Label robotStateLabel)
    {
        _robotConnectionForm = robotConnectionForm;
        _robotStateLabel = robotStateLabel;
    }
   
    // Потом использовать
    public void SetState(string line, Color color)
    {
        lock (_locker)
        {
            try
            {
                SetMainWindowFormState(line, color);
                SetRobotConnectionFormState(line, color);
            }
            catch (ObjectDisposedException) 
            {
            }
        }
    }

    private void SetMainWindowFormState(string line, Color color)
    {
        if (_mainWindowForm?.IsDisposed == false && _robotStateCell != null)
        {
            if (_mainWindowForm.InvokeRequired)
            {
                _mainWindowForm.Invoke(() =>
                {
                    _robotStateCell.Value = line;
                    _robotStateCell.Style.ForeColor = color;
                });
            }
            else
            {
                _robotStateCell.Value = line;
                _robotStateCell.Style.ForeColor = color;
            }
        }
    }

    private void SetRobotConnectionFormState(string line, Color color)
    {
        if (_robotConnectionForm?.IsDisposed == false && _robotStateLabel != null)
        {
            if (_robotConnectionForm.InvokeRequired)
            {
                _robotConnectionForm.Invoke(() =>
                {
                    _robotStateLabel.Text = line;
                    _robotStateLabel.ForeColor = color;
                });
            }
            else
            {
                _robotStateLabel.Text = line;
                _robotStateLabel.ForeColor = color;
            }
        }
    }
}