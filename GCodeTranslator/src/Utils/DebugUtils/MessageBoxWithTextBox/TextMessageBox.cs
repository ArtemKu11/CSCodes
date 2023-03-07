using GCodeTranslator.Utils.DebugUtils.DebugWindow;

namespace GCodeTranslator.Utils.DebugUtils.MessageBoxWithTextBox;

/// <summary>
/// Вспомогательная форма для работы галочки "Запрашивать MessageBox" в <see cref="DebugWindowForm"/>
/// </summary>
public partial class TextMessageBox : Form
{
    private string _state = "1";
    private string _z = "0";

    public string State
    {
        get => _state;
    }

    public string Z
    {
        get => _z;
    }

    public TextMessageBox()
    {
        InitializeComponent();
    }

    public void ShowDialog(string defaultState, string defaultZ)
    {
        _state = defaultState;
        _z = defaultZ;
        stateTextBox.Text = defaultState;
        zTextBox.Text = defaultZ;
        ShowDialog();
    }

    private void okButton_Click(object sender, EventArgs e)
    {
        _state = stateTextBox.Text;
        _z = zTextBox.Text;
        Close();
    }
}