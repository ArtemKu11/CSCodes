﻿using GCodeTranslator.Forms.SettingsWindow;
using GCodeTranslator.Service.DTO;

namespace GCodeTranslator.Service.SettingsWindow;

public class SettingsWindowFormService
{
    private readonly SettingsWindowForm _settingsWindowForm;

    public SettingsWindowFormService(SettingsWindowForm settingsWindowForm)
    {
        _settingsWindowForm = settingsWindowForm;
        SetPropertiesFromDisk();
    }

    public void SetPropertiesFromDisk()
    {
        var actualSettingsHolder = GetActualSettings();
        SetPropertiesFromSettingsHolder(actualSettingsHolder);
    }
    
    private SettingsHolder GetActualSettings()
    {
        var settingsFromDisk = new SettingsLoader().ReadSettingsFromDisk();
        if (settingsFromDisk == null)
        {
            return CreateDefaultSettingsHolder();
        }

        return settingsFromDisk;
    }
    
    private SettingsHolder CreateDefaultSettingsHolder()
    {
        var defaultSettingsHolder = CreateSettingsHolder();
        defaultSettingsHolder.NextLayerTimerEnabled = true;
        defaultSettingsHolder.NextLayerTimerText = "60";
            
        if (defaultSettingsHolder.DefaultRobotIp == "")
        {
            defaultSettingsHolder.DefaultRobotIp = "192.168.8.101";
        }

        if (defaultSettingsHolder.MaxConnectionTime == "")
        {
            defaultSettingsHolder.MaxConnectionTime = "5";
        }

        MessageBox.Show("Не удалось получить актуальные настройки. Установлены следующие:\n" +
                        "Таймер следующего слоя: 60\n" +
                        "Дефолт IP робота: 192.168.8.101\n" +
                        "Макс. время ожид. соединения (сек): 5");
        new SettingsLoader().WriteSettingsOnDisk(defaultSettingsHolder);
        return defaultSettingsHolder;
    }
    
    public SettingsHolder CreateSettingsHolder()
    {
        var settingsHolder = new SettingsHolder();
        settingsHolder.NextLayerTimerEnabled = _settingsWindowForm.NextLayerTimerCheckBox.Checked;
        settingsHolder.NextLayerTimerText = _settingsWindowForm.NextLayerTimerTextBox.Text;
        settingsHolder.CancelButtonTimeEnabled = _settingsWindowForm.CancelTimeCheckBox.Checked;
        settingsHolder.CancelButtonTimeText = _settingsWindowForm.CancelTimeTextBox.Text;
        settingsHolder.DefaultRobotIp = _settingsWindowForm.DefaultIpTextBox.Text;
        settingsHolder.MaxConnectionTime = _settingsWindowForm.MaxConnectionTimeTextBox.Text;
        settingsHolder.EnableLogs = _settingsWindowForm.EnableLogsCheckBox.Checked;

        return settingsHolder;
    }

    private void SetPropertiesFromSettingsHolder(SettingsHolder? settingsHolder)
    {
        if (settingsHolder != null)
        {
            _settingsWindowForm.NextLayerTimerCheckBox.Checked = settingsHolder.NextLayerTimerEnabled;
            _settingsWindowForm.NextLayerTimerTextBox.Text = settingsHolder.NextLayerTimerText;
            _settingsWindowForm.NextLayerTimerTextBox.Enabled = settingsHolder.NextLayerTimerEnabled;
  
            _settingsWindowForm.CancelTimeCheckBox.Checked = settingsHolder.CancelButtonTimeEnabled;
            _settingsWindowForm.CancelTimeTextBox.Text = settingsHolder.CancelButtonTimeText;
            _settingsWindowForm.CancelTimeTextBox.Enabled = settingsHolder.CancelButtonTimeEnabled;

            _settingsWindowForm.DefaultIpTextBox.Text = settingsHolder.DefaultRobotIp;
            _settingsWindowForm.MaxConnectionTimeTextBox.Text = settingsHolder.MaxConnectionTime;
            _settingsWindowForm.EnableLogsCheckBox.Checked = settingsHolder.EnableLogs;
        }
    }


    public bool SaveSettingsPropertiesOnDisk()
    {
        var settingsHolder = CreateSettingsHolder();

        if (!ValidateValues(settingsHolder))
        {
            return false;
        }
            
        new SettingsLoader().WriteSettingsOnDisk(settingsHolder);
        return true;
    } 
    
    private bool ValidateValues(SettingsHolder settingsHolder)
    {
        var errorText = "";
        var errorFlag = false;
            
        if (_settingsWindowForm.NextLayerTimerCheckBox.Checked)
        {
            if (!ValidateCorrectTime(_settingsWindowForm.NextLayerTimerTextBox.Text))
            {
                errorText += "Поле \"Таймер следующего слоя\"\n" +
                             "имеет неверное значение. Надо исправить\n\n";
                errorFlag = true;
            }
        }

        if (_settingsWindowForm.CancelTimeCheckBox.Checked)
        {
            if (!ValidateCorrectTime(_settingsWindowForm.CancelTimeTextBox.Text))
            {
                errorText += "Поле \"Время отмены нажатия кнопки\"\n" +
                             "имеет неверное значение. Надо исправить\n\n";
                errorFlag = true;
            }
        }
            
          
        if (!ValidateCorrectTime(_settingsWindowForm.MaxConnectionTimeTextBox.Text))
        {
            errorText += "Поле \"Макс. время ожид. соединения (сек)\"\n" +
                         "имеет неверное значение. Надо исправить";
            errorFlag = true;
        }

        if (errorFlag)
        {
            MessageBox.Show(errorText);
            return false;
        }

        return true;

    }
    
    private bool ValidateCorrectTime(string maxConnectionTimeValue)
    {
        try
        {
            var time = Int32.Parse(maxConnectionTimeValue);
            if (time < 1)
            {
                return false;
            }
        }
        catch (FormatException)
        {
            return false;
        }

        return true;
    }
    
}