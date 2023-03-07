using GCodeTranslator.Connection.Utils.CheckConnection;

namespace GCodeTranslator.Forms.ConnectionWaitingWindowForm;


/// <summary>
/// Форма ожидания соединения с сервером. Порядок работы:
/// <para>
/// 1. Запустить <see cref="StartLogicAndShowDialog"/>
/// </para>
/// 2. main поток тормозит до тех пор, пока открыта форма
/// <para>
/// 3. После закрытия формы и освобождения main потока получить <see cref="Result"/>
/// </para>
/// Написана немного некрасиво, но работает исправно
/// <para>
/// Предполагается использование через <see cref="ConnectionChecker"/>
/// </para>
/// Если ошибка или нажат крестик, воззвращает "-1"
/// </summary>
public partial class CheckConnectionForm : Form
{
    private readonly CheckConnectionWrapper _checkConnectionWrapper;
    private readonly object _locker = new();
    private KeyValuePair<string, string> _result = new("-1", "0");


    private bool _isCheckingFinished;
    private string _state = "-1";
    private readonly string _stateIfFormClosed = "-1";
    private readonly string _stateIfTimeOutException = "-1";
    private readonly string _stateIfAnotherException = "-1";
    private readonly CancellationTokenSource _tokenSource = new();
    private readonly CancellationToken _cancellationToken;
    private string _ipAddress = "";
    private int _timeOutInSec;

    private readonly bool _autoCloseIf0;
    private readonly bool _autoCloseIf1;
    private readonly bool _autoCloseIf2;
    private readonly bool _autoCloseIfMinus1;

    public KeyValuePair<string, string> Result
    {
        get
        {
            lock (_locker)
            {
                return _result;
            }
        }
    }

    public CheckConnectionForm(bool autoCloseIf0 = true, bool autoCloseIf1 = false, bool autoCloseIf2 = false, bool autoCloseIfMinus1 = false)
    {
        InitializeComponent();
        _cancellationToken = _tokenSource.Token;
        _checkConnectionWrapper = new CheckConnectionWrapper();
            
        _autoCloseIf0 = autoCloseIf0;
        _autoCloseIf1 = autoCloseIf1;
        _autoCloseIf2 = autoCloseIf2;
        _autoCloseIfMinus1 = autoCloseIfMinus1;
    }

    public void StartLogicAndShowDialog(string ipAddress, int timeOutInSec)
    {
        _ipAddress = ipAddress;
        _timeOutInSec = timeOutInSec;
            
        var checkConnectionTask = new Task(() =>
        {
            try
            {
                CheckConnection();
            }
            catch (ObjectDisposedException)
            {
            }
        });  // Первый поток, осуществляет проверку соединения
        checkConnectionTask.Start();
                
                
        var checkCancelTask = new Task(() =>
        {
            try
            {
                CancellationChecking();
            }
            catch (ObjectDisposedException)
            {
            }
        });  // Второй поток, геобходим для правильной обработки досрочного закрытия формы крестиком. При таком сценарии сетает в Result _stateIfFormClosed
        checkCancelTask.Start();


        CenterToScreen();
        ShowDialog();  // Основной поток тормозит тут
    }
        
    private void CheckConnectionForm_FormClosing(object sender, FormClosingEventArgs e)
    {
        _tokenSource.Cancel();
        _tokenSource.Dispose();
        Dispose();
    }

    private void CancellationChecking()
    {
        while (!_isCheckingFinished)
        {
            if (_cancellationToken.IsCancellationRequested)
            {
                lock (_locker)
                {
                    if (!_isCheckingFinished)
                    {
                        _state = _stateIfFormClosed;
                        _result = new KeyValuePair<string, string>(_state, "0");
                        return;
                    }
                }
            }
        }
    }

    private void CheckConnection()
    {
        WaitUntilFormCreated();

        var waitingAnimationTask = CreateAnimationTask();  
        waitingAnimationTask.Start();  // 3 поток. Тупо анимация точек в "Соединение..."
        try
        {
            var result = _checkConnectionWrapper.CheckConnection(_ipAddress, _timeOutInSec);
            _isCheckingFinished = true;
            HandleSuccessWrapper(result, waitingAnimationTask);
        }
        catch (TimeoutException exception)
        {
            _isCheckingFinished = true;
            HandleTimeoutException(exception, waitingAnimationTask);
        }
        catch (Exception exception)
        {
            _isCheckingFinished = true;
            HandleAnotherException(exception, waitingAnimationTask);
        }
    }

    private Task CreateAnimationTask()
    {
        return new Task(() =>
        {
            try
            {
                StartAnimation();
            }
            catch (ObjectDisposedException)
            {
            }
        });
    }

    private void HandleSuccessWrapper(KeyValuePair<string, string> result, Task waitingAnimationTask)
    {
        lock (_locker)
        {
            if (!_cancellationToken.IsCancellationRequested)
            {
                _result = result;
            }
        }
        if (!IsDisposed)
        {
            Invoke(HandleSuccess);
        }
                
        waitingAnimationTask.Wait();
        ResolveCloseForm();
    }

    private void HandleTimeoutException(Exception exception, Task waitingAnimationTask)
    {
        if (!IsDisposed)
        {
            Invoke(() =>
            {
                HandleException(exception, true);
            });
        }
        waitingAnimationTask.Wait();
        ResolveCloseForm();
    }

    private void HandleAnotherException(Exception exception, Task waitingAnimationTask)
    {
        if (!IsDisposed)
        {
            Invoke(() =>
            {
                HandleException(exception, false);
            });
        }
        waitingAnimationTask.Wait();
    }

    private void ResolveCloseForm()
    {
        switch (Result.Key)
        {
            case "0":
                Thread.Sleep(500);
                if (_autoCloseIf0 && !IsDisposed)
                {
                    Invoke(Close);
                }
                break;
            case "1":
                Thread.Sleep(1500);
                if (_autoCloseIf1 && !IsDisposed)
                {
                    Invoke(Close);
                }
                break;
            case "2":
                Thread.Sleep(1500);
                if (_autoCloseIf2 && !IsDisposed)
                {
                    Invoke(Close);
                }
                break;
            case "-1":
                if (_autoCloseIfMinus1 && !IsDisposed)
                {
                    Invoke(Close);
                }
                break;
        }
    }

    private void WaitUntilFormCreated()
    {
        while (!IsHandleCreated)
        {
            Thread.Sleep(100);
        }
    }

    private void StartAnimation()
    {
        var pointCounter = 1;
        while (!_isCheckingFinished) 
        {
            if (InvokeRequired && !IsDisposed)
            {
                var counter = pointCounter;
                Invoke(() =>
                {
                    if (!_isCheckingFinished)
                    {
                        connectionWaitingLabel.Text =
                            "Соединение" + String.Concat(Enumerable.Repeat(".", counter));
                        PutTextOnCenter(); 
                    }
                });
            }
            ++pointCounter;
            if (pointCounter > 3)
            {
                pointCounter = 1;
            }
            Thread.Sleep(500);
        }

    }

    private void PutTextOnCenter()
    {
        PutMainTextOnCenter();
        PutErrorTextOnCenter();
    }

    private void PutMainTextOnCenter()
    {
        var panelWidth = checkConnectionMainPanel.Width;
        var panelHeight = checkConnectionMainPanel.Height;
        var textWidth = connectionWaitingLabel.Width;
        var textHeight = connectionWaitingLabel.Height;
        var x = panelWidth / 2 - textWidth / 2;
        var y = panelHeight / 2 - textHeight / 2;
        connectionWaitingLabel.Location = new Point(x, y);
    }

    private void PutErrorTextOnCenter()
    {
        var errorTypeWidth = errorTypeLabel.Width;
        var errorClassWidth = errorClassLabel.Width;
        var summaryErrorWidth = errorTypeWidth + errorClassWidth + 6;
        var panelWidth = checkConnectionMainPanel.Width;

        var errorTypeX = panelWidth / 2 - summaryErrorWidth / 2;
        var errorClassX = errorTypeX + errorTypeWidth + 6;

        errorTypeLabel.Location = new Point(errorTypeX, errorTypeLabel.Location.Y);
        errorClassLabel.Location = new Point(errorClassX, errorClassLabel.Location.Y);

    }

    private void HandleException(Exception exception, bool isItTimeOut)
    {
        lock (_locker)
        {
                
            if (_cancellationToken.IsCancellationRequested)
            {
                _state = _stateIfFormClosed;
                return;
            }

            ResolveErrorText(exception, isItTimeOut);
               
            if (isItTimeOut)
            {
                _state = _stateIfTimeOutException;
                _result = new KeyValuePair<string, string>(_stateIfTimeOutException, "0");
            }
            else
            {
                _state = _stateIfAnotherException;
                _result = new KeyValuePair<string, string>(_stateIfAnotherException, "0");
            }
        }
    }

    private void ResolveErrorText(Exception exception, bool isItTimeOut)
    {
            
        connectionWaitingLabel.Text = "Ошибка соединения";
        connectionWaitingLabel.ForeColor = Color.Firebrick;
        errorTypeLabel.Text = "Тип ошибки:";
            
        if (isItTimeOut)
        {
            errorClassLabel.Text = "Время подключения истекло";
        }
        else
        {
            errorClassLabel.Text = exception.GetType().ToString();
            System.Diagnostics.Debug.WriteLine(exception.StackTrace);
        }
        PutTextOnCenter();
    }

    private void HandleSuccess()
    {
        switch (_result.Key)
        {
            case "0":
                connectionWaitingLabel.Text = "Успешно";
                connectionWaitingLabel.ForeColor = Color.Green;
                PutTextOnCenter();
                break;
            case "1":
                connectionWaitingLabel.Text = "Принтер печатает.\nХз че будет, если подключиться.\nЭкспериментируйте";
                connectionWaitingLabel.TextAlign = ContentAlignment.MiddleCenter;
                connectionWaitingLabel.ForeColor = Color.Orange;
                PutTextOnCenter();
                break;
            case "2":
                connectionWaitingLabel.Text = "Принтеру необходим файл.\nХз че будет, если подключиться.\nЭкспериментируйте";
                connectionWaitingLabel.TextAlign = ContentAlignment.MiddleCenter;
                connectionWaitingLabel.ForeColor = Color.Orange;
                PutTextOnCenter();
                break;
            default:
                connectionWaitingLabel.Text = "Неизвестный ответ принтера. Подключиться пока невозможно";
                connectionWaitingLabel.ForeColor = Color.Firebrick;
                PutTextOnCenter();
                break;
        }
    }
}