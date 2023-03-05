using System.ComponentModel;
using GCodeTranslator.Utils.DebugUtils.DebugService;

namespace GCodeTranslator.Utils.DebugUtils.DebugWindow;

partial class DebugWindowForm
{
    public DebugWindowFormService DebugWindowFormService
    {
        get => _debugWindowFormService;
        set => _debugWindowFormService = value ?? throw new ArgumentNullException(nameof(value));
    }

    public IContainer Components
    {
        get => components;
        set => components = value;
    }

    public TabControl MainTabControl
    {
        get => mainTabControl;
        set => mainTabControl = value;
    }

    public TabPage ConnectionEmulationTabPage
    {
        get => connectionEmulationTabPage;
        set => connectionEmulationTabPage = value;
    }

    public Button ApplyValueButton
    {
        get => applyValueButton;
        set => applyValueButton = value;
    }

    public TextBox StateTextBox
    {
        get => stateTextBox;
        set => stateTextBox = value;
    }

    public Label ValueHolderLabel
    {
        get => valueHolderLabel;
        set => valueHolderLabel = value;
    }

    public CheckBox ValueRequiredCheckBox
    {
        get => valueRequiredCheckBox;
        set => valueRequiredCheckBox = value;
    }

    public CheckBox MessageBoxRequiredCheckBox
    {
        get => messageBoxRequiredCheckBox;
        set => messageBoxRequiredCheckBox = value;
    }

    public Label StatusHolderLabel
    {
        get => statusHolderLabel;
        set => statusHolderLabel = value;
    }

    public Label StatusLabel
    {
        get => statusLabel;
        set => statusLabel = value;
    }

    public TabPage RealConnectionTabPage
    {
        get => realConnectionTabPage;
        set => realConnectionTabPage = value;
    }

    public Label MessageHolderLabel
    {
        get => messageHolderLabel;
        set => messageHolderLabel = value;
    }

    public Label ResultHolderLabel
    {
        get => resultHolderLabel;
        set => resultHolderLabel = value;
    }

    public Label MessageLabel
    {
        get => messageLabel;
        set => messageLabel = value;
    }

    public Label ResultLabel
    {
        get => resultLabel;
        set => resultLabel = value;
    }

    public Button FormNotRequiredCheckConnectionButton
    {
        get => formNotRequiredCheckConnectionButton;
        set => formNotRequiredCheckConnectionButton = value;
    }

    public Button FormRequiredCheckConnectionButton
    {
        get => formRequiredCheckConnectionButton;
        set => formRequiredCheckConnectionButton = value;
    }

    public TextBox TimeOutTextBox
    {
        get => timeOutTextBox;
        set => timeOutTextBox = value;
    }

    public Label TimeOutLabel
    {
        get => timeOutLabel;
        set => timeOutLabel = value;
    }

    public TextBox IpAddressTextBox
    {
        get => ipAddressTextBox;
        set => ipAddressTextBox = value;
    }

    public Label IpAddressLabel
    {
        get => ipAddressLabel;
        set => ipAddressLabel = value;
    }

    public Label ZLabel
    {
        get => zLabel;
        set => zLabel = value;
    }

    public Label StateLabel
    {
        get => stateLabel;
        set => stateLabel = value;
    }

    public TextBox ZTextBox
    {
        get => zTextBox;
        set => zTextBox = value;
    }

    public Button FileRequiredButton
    {
        get => fileRequiredButton;
        set => fileRequiredButton = value;
    }

    public Button PrintButton
    {
        get => printButton;
        set => printButton = value;
    }

    public Button ReadyButton
    {
        get => readyButton;
        set => readyButton = value;
    }

    public Button TimeOutExceptionButton
    {
        get => timeOutExceptionButton;
        set => timeOutExceptionButton = value;
    }
}