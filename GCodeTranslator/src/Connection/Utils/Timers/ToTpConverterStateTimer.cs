using GCodeTranslator.Connection.Utils.InfoTextBoxChangeProcessor;
using GCodeTranslator.Forms.RobotConnectionWindow;
using NetMQ;
using NetMQ.Sockets;
using Timer = System.Windows.Forms.Timer;

namespace GCodeTranslator.Connection.Utils.Timers;


/// <summary>
/// Какая-то костыльная оригинальная логика, вынесенная мной в отдельный модуль
/// <para>
/// После конвертации файлов в .tp по кнопке "Экспорт в TP" в <see cref="RobotConnectionForm"/>
/// обращается к конвертеру раз в 1 секунду и принтует в _infoTextBox ответ. Прекращает свое действие по кнопке "Сброс"
/// в <see cref="RobotConnectionForm"/>
/// </para>
/// </summary>
public class ToTpConverterStateTimer
{
    private readonly InfoTextBoxProcessor _infoTextProcessor;
    private readonly Timer _timer;

    public ToTpConverterStateTimer(InfoTextBoxProcessor infoTextProcessor)
    {
        _infoTextProcessor = infoTextProcessor;
        _timer = CreateTimer();

    }

    private Timer CreateTimer()
    {
        var timer = new Timer();
        timer.Tick += TimerEvent;
        timer.Interval = 1000;
        return timer;
    }

    public void Start()
    {
        if (!_timer.Enabled)
        {
            _timer.Start();
        }
    }

    public void Stop()
    {
        if (_timer.Enabled)
        {
            _timer.Stop();
        }
    }

    public void Close()
    {
        if (_timer.Enabled)
        {
            _timer.Stop();
        }
        _timer.Dispose();
    }
    
    private void TimerEvent(object? myObject, EventArgs myEventArgs)
    {
        using (var client = new RequestSocket())
        {
            client.Connect($"tcp://localhost:5001");
            client.SendFrame($"states");
            client.TryReceiveFrameString(TimeSpan.FromSeconds(2), out var resultMessage);
            if (resultMessage != null)
            {
                _infoTextProcessor.PrintLine(resultMessage, true);
            }
        }
    }
}