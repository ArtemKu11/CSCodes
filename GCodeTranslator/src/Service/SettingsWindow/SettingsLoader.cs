using System.Text.Json;
using GCodeTranslator.Forms.SettingsWindow;
using GCodeTranslator.Service.DTO;

namespace GCodeTranslator.Service.SettingsWindow;

/// <summary>
/// Класс, использующийся для сериализации/десериалиации настроек <see cref="SettingsWindowForm"/> в json
/// <para></para>
/// Необходим для реализации кнопок "Сохранить" и "Отменить" в <see cref="SettingsWindowForm"/>
/// </summary>
public class SettingsLoader
{
    public void WriteSettingsOnDisk(SettingsHolder settingsHolder)
    {
        ResolveDirectory();
        using FileStream fs = new FileStream("JSON\\settings.json", FileMode.Create);
        JsonSerializer.Serialize(fs, settingsHolder);
    }

    private void ResolveDirectory()
    {
        if (!Directory.Exists("JSON"))
        {
            Directory.CreateDirectory("JSON");
        }
    }

    public SettingsHolder? ReadSettingsFromDisk()
    {
        if (File.Exists("JSON\\settings.json"))
        {
            using (FileStream fs = new FileStream("JSON\\settings.json", FileMode.OpenOrCreate))
            {
                try
                {
                    var settingsHolder = JsonSerializer.Deserialize(fs, typeof(SettingsHolder));
                    if (settingsHolder != null)
                    {
                        return (SettingsHolder)settingsHolder;
                    }
                    MessageBox.Show("Фатальная ошибка.\nНастройки десериализовались в null");
                }
                catch (Exception)
                {
                    MessageBox.Show("Фатальная ошибка.\nНастройки не десериализовались вообще");
                }
                
            }
        }
        else
        {
            MessageBox.Show("Файла с настройками не обнаружилось");
        }

        return null;
    }
}