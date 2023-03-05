using GCodeTranslator.Connection.Utils.InfoTextBoxChangeProcessor;
using NetMQ;
using NetMQ.Sockets;
using Timer = System.Windows.Forms.Timer;

namespace GCodeTranslator.Connection.Utils.Timers;

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