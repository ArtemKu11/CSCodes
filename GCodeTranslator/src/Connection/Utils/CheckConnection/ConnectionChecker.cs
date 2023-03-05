﻿using GCodeTranslator.Forms.ConnectionWaitingWindowForm;

namespace GCodeTranslator.Connection.Utils.CheckConnection;

public class ConnectionChecker
{
    private readonly CheckConnectionWrapper _checkConnectionWrapper = new();
    
    
    public KeyValuePair<string, string> CheckConnection(string ipAddress, int timeOutInSec, bool requiredForm,  
        bool autoCloseIf0 = true, bool autoCloseIf1 = false, bool autoCloseIf2 = false, bool autoCloseIfMinus1 = false)
    {
        if (requiredForm)
        {
            return CheckConnectionWithForm(ipAddress, timeOutInSec, autoCloseIf0, autoCloseIf1, autoCloseIf2, autoCloseIfMinus1);
        }

        return CheckConnectionWithoutForm(ipAddress, timeOutInSec);
    }

    public KeyValuePair<string, string> CheckConnectionWithForm(string ipAddress, int timeOutInSec, 
        bool autoCloseIf0 = true, bool autoCloseIf1 = false, bool autoCloseIf2 = false, bool autoCloseIfMinus1 = false)
    {
        var checkConnectionForm = new CheckConnectionForm(autoCloseIf0, autoCloseIf1, autoCloseIf2, autoCloseIfMinus1);
        checkConnectionForm.StartLogicAndShowDialog(ipAddress, timeOutInSec);  // Обработка ошибок внутри формы
        return checkConnectionForm.Result;
    }
    
    public KeyValuePair<string, string> CheckConnectionWithoutForm(string ipAddress, int timeOutInSec)
    {
        try
        {
            return _checkConnectionWrapper.CheckConnection(ipAddress, timeOutInSec);  // Обработки ошибок нет
        }
        catch (TimeoutException)
        {
            return new KeyValuePair<string, string>("-1", "0");
        }
    }
}