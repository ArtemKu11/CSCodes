using System.ComponentModel;
using GCodeTranslator.Connection.DTO;
using GCodeTranslator.Connection.GCodeSender;
using GCodeTranslator.Connection.Utils.InfoTextBoxChangeProcessor;
using GCodeTranslator.Connection.Utils.RobotStateChangeProcessor;
using GCodeTranslator.Connection.Utils.Timers;
using GCodeTranslator.Service.DTO;

namespace GCodeTranslator.Forms.RobotConnectionWindow;

partial class RobotConnectionForm
{
    public Button RefreshStateButton
    {
        get => refreshStateButton;
        set => refreshStateButton = value;
    }

    public IContainer Components
    {
        get => components;
        set => components = value;
    }

    public Panel MainPanel
    {
        get => mainPanel;
        set => mainPanel = value;
    }

    public Panel MainContentConnectionToRobotPanel
    {
        get => mainContentConnectionToRobotPanel;
        set => mainContentConnectionToRobotPanel = value;
    }

    public TextBox PrintInfoTextBox
    {
        get => printInfoTextBox;
        set => printInfoTextBox = value;
    }

    public Button ResetButton
    {
        get => resetButton;
        set => resetButton = value;
    }

    public CheckBox ExportOneCheckBox
    {
        get => exportOneCheckBox;
        set => exportOneCheckBox = value;
    }

    public Button ExportToTpButton
    {
        get => exportToTpButton;
        set => exportToTpButton = value;
    }

    public Button StartPrintingButton
    {
        get => startPrintingButton;
        set => startPrintingButton = value;
    }

    public Button PrevButton
    {
        get => prevButton;
        set => prevButton = value;
    }

    public Button NextButton
    {
        get => nextButton;
        set => nextButton = value;
    }

    public Button RepeatButton
    {
        get => repeatButton;
        set => repeatButton = value;
    }

    public CheckBox AwaitLayerCheckBox
    {
        get => awaitLayerCheckBox;
        set => awaitLayerCheckBox = value;
    }

    public ComboBox LayersComboBox
    {
        get => layersComboBox;
        set => layersComboBox = value;
    }

    public Button BrowseFolderButton
    {
        get => browseFolderButton;
        set => browseFolderButton = value;
    }

    public TextBox FolderTextBox
    {
        get => folderTextBox;
        set => folderTextBox = value;
    }

    public Label RobotStateLabel
    {
        get => robotStateLabel;
        set => robotStateLabel = value;
    }
}