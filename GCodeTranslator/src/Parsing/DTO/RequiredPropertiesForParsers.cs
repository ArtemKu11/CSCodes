using GCodeTranslator.Forms.MainWindow;
using GCodeTranslator.Service.DTO;

namespace GCodeTranslator.Parsing.DTO;

/// <summary>
/// Используется для передачи значений полей формы <see cref="MainWindowForm"/> парсерам
/// </summary>
public struct RequiredPropertiesForParsers
{
    public BrowsedFileProperties BrowsedFileProperties;  // Параметры выбранного файла
    public string SplitLayersTextBoxText;  // Значение поля "Split layers" 
    public bool AutoSplitLayersCheckBoxChecked;  // Значение галочки "Autosplit layers" 
    public string NormalMovementTextBoxText;  // Значение поля "Normal movement"
    public string WeldingMovementTextBoxText;  // Значение поля "Welding movement" 
    public bool AutoArcByExtrusionCheckBoxChecked;  // Значение галочки "Auto Arc Enabled By Extrusion" 
    public bool RunWithoutArcCheckBoxChecked;  // Значение галочки "Run Without Arc" 
    public bool RoEnableCheckBoxChecked;  // Значение галочки "RO enable"
    public bool WaveEnableCheckBoxChecked;  // Значение галочки "Wave enable"
    public string WaveEnableTextBoxText;  // Значение поля "Wave enable"
    public string WeldShieldTextBoxText;  // Значение поля "Weld Shield"
    public bool WeldShieldCheckBoxChecked;  // Значение галочки "Weld Shield"
    public bool UseWeldSpeedCheckBoxChecked;  // Значение галочки "Use WELD_SPEED instead of feedrate"  
    public string RobotUfTextBoxText;  // Значение поля "UF" в Robot Frame
    public string RobotUtTextBoxText;  // Значение поля "UT" в Robot Frame
    public string PositionerUfTextBoxTex;  // Значение поля "UF" в Positioner Frame  
    public string PositionerUtTextBoxTex;  // Значение поля "UT" в Positioner Frame  
    public string XTextBoxText;  // Значение поля "X"
    public string YTextBoxText;  // Значение поля "Y"
    public string ZTextBoxText;  // Значение поля "Z"
    public string WTextBoxText;  // Значение поля "W"
    public string PTextBoxText;  // Значение поля "P"
    public string RTextBoxText;  // Значение поля "R"
    public bool LaserPassCheckBoxChecked;  // Значение галочки "Laser pass"
    public bool RemoveSmallStopStartCheckBoxChecked;  // Значение галочки "Remove Small Stop Start"
    public string CheckingDistanceTextBoxText;  // Значение поля "Checking distance (mm)"
    public string ShortWristComboBoxText; // Короткое значение комбо-бокса "Wrist" "F"/"N"
    public string ShortArmComboBoxText;  // Короткое значение комбо-бокса "Arm" "U"/"D"
    public string ShortBaseComboBoxText;  // Короткое значение комбо-бокса "Base" "T"/"B"
    public string TurnsJ1TextBox;  // Значение поля "Turns J1" 
    public string TurnsJ4TextBox;  // Значение поля "Turns J4" 
    public string TurnsJ6TextBox;  // Значение поля "Turns J6" 
    public string J1OffsetTextBoxText;  // Значение поля "A(j1)"  
    public string J2OffsetTextBoxText;  // Значение поля "B(j2)"
    public bool AngleScriptCheckBoxChecked;  // Значение галочки "Pankratov Angle-Technology™"
    public string CriticalAngleDifferenceTextBoxText;  // Значение поля "Критическая разница углов"  
    public string MaxAngleValueTextBoxText;  // Значение поля "Максимальное значение угла"  

    public override string ToString()
    {
        return $"SplitLayersTextBoxText: {SplitLayersTextBoxText}\n" +
               $"AutoSplitLayersCheckBoxChecked: {AutoSplitLayersCheckBoxChecked}\n" +
               $"NormalMovementTextBoxText: {NormalMovementTextBoxText}\n" +
               $"WeldingMovementTextBoxText: {WeldingMovementTextBoxText}\n" +
               $"AutoArcByExtrusionCheckBoxChecked: {AutoArcByExtrusionCheckBoxChecked}\n" +
               $"RunWithoutArcCheckBoxChecked: {RunWithoutArcCheckBoxChecked}\n" +
               $"RoEnableCheckBoxChecked: {RoEnableCheckBoxChecked}\n" +
               $"WaveEnableCheckBoxChecked: {WaveEnableCheckBoxChecked}\n" +
               $"WaveEnableTextBoxText: {WaveEnableTextBoxText}\n" +
               $"WeldShieldTextBoxText: {WeldShieldTextBoxText}\n" +
               $"WeldShieldCheckBoxChecked: {WeldShieldCheckBoxChecked}\n" +
               $"UseWeldSpeedCheckBoxChecked: {UseWeldSpeedCheckBoxChecked}\n" +
               $"RobotUfTextBoxText: {RobotUfTextBoxText}\n" +
               $"RobotUtTextBoxText: {RobotUtTextBoxText}\n" +
               $"PositionerUfTextBoxTex: {PositionerUfTextBoxTex}\n" +
               $"PositionerUtTextBoxTex: {PositionerUtTextBoxTex}\n" +
               $"XTextBoxText: {XTextBoxText}\n" +
               $"YTextBoxText: {YTextBoxText}\n" +
               $"ZTextBoxText: {ZTextBoxText}\n" +
               $"WTextBoxText: {WTextBoxText}\n" +
               $"PTextBoxText: {PTextBoxText}\n" +
               $"RTextBoxText: {RTextBoxText}\n" +
               $"LaserPassCheckBoxChecked: {LaserPassCheckBoxChecked}\n" +
               $"RemoveSmallStopStartCheckBoxChecked: {RemoveSmallStopStartCheckBoxChecked}\n" +
               $"CheckingDistanceTextBoxText: {CheckingDistanceTextBoxText}\n" +
               $"ShortWristComboBoxText: {ShortWristComboBoxText}\n" +
               $"ShortArmComboBoxText: {ShortArmComboBoxText}\n" +
               $"ShortBaseComboBoxText: {ShortBaseComboBoxText}\n" +
               $"TurnsJ1TextBox: {TurnsJ1TextBox}\n" +
               $"TurnsJ4TextBox: {TurnsJ4TextBox}\n" +
               $"TurnsJ6TextBox: {TurnsJ6TextBox}\n" +
               $"J1OffsetTextBoxText: {J1OffsetTextBoxText}\n" +
               $"J2OffsetTextBoxText: {J2OffsetTextBoxText}\n" +
               $"AngleScriptCheckBoxChecked: {AngleScriptCheckBoxChecked}\n" +
               $"CriticalAngleDifferenceTextBoxText: {CriticalAngleDifferenceTextBoxText}\n" +
               $"MaxAngleValueTextBoxText: {MaxAngleValueTextBoxText}";
    }
}