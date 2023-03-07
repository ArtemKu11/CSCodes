using GCodeTranslator.Utils.DebugUtils.DebugWindow;

namespace GCodeTranslator.Utils.DebugUtils.StatusLabelProcessor;

/// <summary>
/// Thread-safety изменение _statusHolderLabel в <see cref="DebugWindowForm"/>
/// </summary>
public class StatusLabelChangeProcessor
{
    private readonly DebugWindowForm _debugWindowForm;
    private readonly Label _statusHolderLabel;
    private readonly object _locker = new();

    public StatusLabelChangeProcessor(DebugWindowForm debugWindowForm, Label statusHolderLabel)
    {
        _debugWindowForm = debugWindowForm;
        _statusHolderLabel = statusHolderLabel;
    }

    public void SetState(string state, Color color)
    {
        try
        {
            lock (_locker)
            {
                if (_debugWindowForm is { IsDisposed: false }) ChangeState(state, color);
            }
        }
        catch (ObjectDisposedException)
        {
        }
    }

    private void ChangeState(string state, Color color)
    {
        if (_debugWindowForm.InvokeRequired)
        {
            _debugWindowForm.Invoke(() =>
            {
                _statusHolderLabel.Text = state;
                _statusHolderLabel.ForeColor = color;
            });
        }
        else
        {
            _statusHolderLabel.Text = state;
            _statusHolderLabel.ForeColor = color;
        }
    }
}