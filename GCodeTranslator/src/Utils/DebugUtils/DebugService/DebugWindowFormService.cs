using GCodeTranslator.Connection.Utils.CheckConnection;
using GCodeTranslator.Connection.Utils.RobotConnector;
using GCodeTranslator.Forms.SettingsWindow;
using GCodeTranslator.Utils.DebugUtils.DebugCheckConnection;
using GCodeTranslator.Utils.DebugUtils.DebugWindow;
using GCodeTranslator.Utils.DebugUtils.StatusLabelProcessor;

namespace GCodeTranslator.Utils.DebugUtils.DebugService;

/// <summary>
/// Сервис к <see cref="DebugWindowForm"/>. Инкапсулирует логику
/// </summary>
public class DebugWindowFormService
{
    private readonly DebugWindowForm _debugWindowForm;
    private string _currentState = "1";
    private string? _currentZ = "0";
    private string _currentIp = "";
    private int _currentTimeOut;
    private bool _messageBoxRequired;


    public DebugWindowFormService(DebugWindowForm debugWindowForm)
    {
        _debugWindowForm = debugWindowForm;
        InitializeActualSettingsProperties();
    }

    private void InitializeActualSettingsProperties()
    {
        var actualSettingsHolder = new SettingsWindowForm().GetActualSettingsHolder();
        var defaultRobotIp = actualSettingsHolder.DefaultRobotIp;
        var maxConnectionTime = actualSettingsHolder.MaxConnectionTime;
        
        _currentIp = defaultRobotIp;
        _currentTimeOut = int.Parse(maxConnectionTime);
        _debugWindowForm.IpAddressTextBox.Text = defaultRobotIp;
        _debugWindowForm.TimeOutTextBox.Text = maxConnectionTime;
        
        DebugCheckConnectionWrapper.IpAddress = defaultRobotIp;
        DebugCheckConnectionWrapper.TimeOutInSec = int.Parse(maxConnectionTime);
        DebugCheckConnectionWrapper.StatusLabelChangeProcessor = new StatusLabelChangeProcessor(_debugWindowForm, _debugWindowForm.StatusHolderLabel);
        CheckConnectionWrapper.DebugModeEnabled = true;
        RobotServerConnector.DebugModeEnabled = true;
    }

    public void SetRequirements(bool messageBoxRequired)
    {
        if (messageBoxRequired)
        {
            DebugCheckConnectionWrapper.MessageBoxRequired = true;
            _messageBoxRequired = true;
        }
        else
        {
            DebugCheckConnectionWrapper.MessageBoxRequired = false;
            SetIntoLabel(_currentState, _currentZ);
            SetIntoCheckConnectionWrapper(_currentState, _currentZ);
            _messageBoxRequired = false;
        }
    }
    
    public void ChangeNextReturnedValue()
    {
        var state = _debugWindowForm.StateTextBox.Text;
        var z = _debugWindowForm.ZTextBox.Text;
        SetValues(state, z);
    }

    public void SetValues(string state, string? z)
    {
        SetIntoTextBoxes(state, z);
        SetIntoLabel(state, z);
        if (!_messageBoxRequired)
        {
            SetIntoCheckConnectionWrapper(state, z);
        }
    }

    private void SetIntoTextBoxes(string state, string? z)
    {
        if (!IsGoodValue(state))
        {
            state = _currentState;
        } 
        if (!IsGoodValue(z))
        {
            z = _currentZ;
        } 
        _debugWindowForm.StateTextBox.Text = state;
        _debugWindowForm.ZTextBox.Text = z;
        _currentState = state;
        _currentZ = z;
    }

    private void SetIntoLabel(string state, string? z)
    {
        if (!IsGoodValue(state))
        {
            state = _currentState;
        } 
        if (!IsGoodValue(z))
        {
            z = _currentZ;
        } 
        _debugWindowForm.ValueHolderLabel.Text = $"state: {state}, z: {z}";
        _currentState = state;
        _currentZ = z;
        
    }

    private void SetIntoCheckConnectionWrapper(string state, string? z)
    {
        DebugCheckConnectionWrapper.ResultState = state;
        if (z != null) DebugCheckConnectionWrapper.ResultZ = z;
    }

    private bool IsGoodValue(string? value)
    {
        try
        {
            _ = int.Parse(value ?? throw new ArgumentNullException(nameof(value)));
        }
        catch (FormatException)
        {
            return false;
        }
        catch (ArgumentNullException)
        {
            return false;
        }

        return true;
    }

    public void DisableDebugMode()
    {
        CheckConnectionWrapper.DebugModeEnabled = false;
        RobotServerConnector.DebugModeEnabled = false;
        MessageBox.Show("Режим разработчика отключен");
    }

    public void SaveNewSettings()
    {
        var ipAddress = _debugWindowForm.IpAddressTextBox.Text;
        var timeOut = _debugWindowForm.TimeOutTextBox.Text;

        if (ipAddress != null)
        {
            DebugCheckConnectionWrapper.IpAddress = ipAddress;
            _currentIp = ipAddress;
        }

        if (timeOut != null)
        {
            try
            {
                var intTimeOut = int.Parse(timeOut);
                DebugCheckConnectionWrapper.TimeOutInSec = intTimeOut;
                _currentTimeOut = intTimeOut;
            }
            catch (FormatException)
            {
                _debugWindowForm.TimeOutTextBox.Text = DebugCheckConnectionWrapper.TimeOutInSec.ToString();
            }
        }
    }

    public void CheckConnection(bool formRequired)
    {
        string state;
        string z;
        string message;

        DebugCheckConnectionWrapper.RealConnectionRequired = true;
        _debugWindowForm.FormRequiredCheckConnectionButton.Enabled = false;
        _debugWindowForm.FormNotRequiredCheckConnectionButton.Enabled = false;
        if (formRequired)
        {
            (state, z) = new ConnectionChecker().CheckConnection(_currentIp, _currentTimeOut, true);
        }
        else
        {
            (state, z) = new ConnectionChecker().CheckConnection(_currentIp, _currentTimeOut, false);
        }
        DebugCheckConnectionWrapper.RealConnectionRequired = false;
        _debugWindowForm.FormRequiredCheckConnectionButton.Enabled = true;
        _debugWindowForm.FormNotRequiredCheckConnectionButton.Enabled = true;

        message = DebugCheckConnectionWrapper.RealResultMessage;
        RefreshFormValues(state, z, message);
    }

    private void RefreshFormValues(string state, string z, string message)
    {
        _debugWindowForm.ResultHolderLabel.Text = $"state: {state}, z: {z}";
        _debugWindowForm.MessageHolderLabel.Text = message;
    }
}