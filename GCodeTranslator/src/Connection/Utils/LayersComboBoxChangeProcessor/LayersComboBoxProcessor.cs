using GCodeTranslator.Forms.RobotConnectionWindow;

namespace GCodeTranslator.Connection.Utils.LayersComboBoxChangeProcessor;

/// <summary>
/// Класс, предназначенный для Thread-safety изменения выбранного слоя в _layersComboBox в <see cref="RobotConnectionForm"/>
/// вне зависимости от потока, в котором используется
/// </summary>
public class LayersComboBoxProcessor
{
    private readonly object _locker = new();
    private readonly ComboBox _layersComboBox;
    private readonly RobotConnectionForm _robotConnectionForm;

    public LayersComboBoxProcessor(ComboBox layersComboBox, RobotConnectionForm robotConnectionForm)
    {
        _layersComboBox = layersComboBox;
        _robotConnectionForm = robotConnectionForm;
    }

    public void SetItem(int id)
    {
        lock (_locker)
        {
            try
            {
                ChangeItem(id);
            }
            catch (ObjectDisposedException)
            {
            }
        }
    }

    private void ChangeItem(int id)
    {
        if (_robotConnectionForm is { IsDisposed: false })
        {
            if (_robotConnectionForm.InvokeRequired)
            {
                _robotConnectionForm.Invoke(() =>
                {
                    if (id < _layersComboBox.Items.Count && id > 0)
                    {
                        _layersComboBox.SelectedIndex = id;
                    }
                });
            }
            else
            {
                if (id < _layersComboBox.Items.Count && id > 0)
                {
                    _layersComboBox.SelectedIndex = id;
                }
            }
        }
    }
}