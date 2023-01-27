
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GCodeRobotCSharpEdition.Tamplates;
using NetMQ;
using NetMQ.Sockets;
using System.Threading;
using GCodeRobotCSharpEdition.LoggerPackage;

namespace GCodeRobotCSharpEdition.Robot
{
    public class RobotTamplate
    {

        SendTampalte sTmpl = new SendTampalte();
        public printParam PrParam;

        private bool _sendNextFile = false;
        public string extension = ".tp";

        private Logger _logger = LoggerFactory.GetExistingOrCreateNewLogger("RootLogger");
        private Logger _appendableMainLogger = LoggerFactory.GetAppendableLogger("MainLogger");

        public bool SendNextFile
        {
            get
            {
                _logger.LogWithTime("SendNextFile.get START, value = " + _logger.NTS(_sendNextFile));
                _logger.LogWithTime("SendNextFile.get END, value = " + _logger.NTS(_sendNextFile));
                return _sendNextFile;
            }
            set
            {
                _logger.LogWithTime("SendNextFile.set START");
                if (value && isAwait/* && !isPrinting*/)
                {
                    _logger.LogWithTime("SendNextFile.set inside if START");

                    stateTempl.color = Color.Orange;
                    stateTempl.CurrentState = "Await sending Next file";
                    form.CurState = stateTempl;
                    
                    _logger.LogWithTime("SendNextFile.set inside if END");


                }
                


                _sendNextFile = value;
                _logger.LogWithTime("SendNextFile.set END, value: " + _logger.NTS(value));
            }
        }
        public string Addres { get; set; }

        private bool lastFile = false;
        public StateTempl stateTempl;

        public bool isPrinting { private set; get; } = false;

        private bool isAwait = false;
        public Form2 form;

        public RobotTamplate(string ip)
        {
            _logger.LogWithTime("RobotTamplate constr START");
            Addres = ip;
            stateTempl.CurrentState = "Unconnect";
            stateTempl.color = Color.Red;
            _logger.LogWithTime("ChechConnection CONSTR START");
            ChechConnection();
            _logger.LogWithTime("ChechConnection CONSTR END");
            _logger.LogWithTime("RobotTamplate constr END");
        }

        public void PrintAsync(bool await_layer, string fname, int fid)
        {
            _logger.LogWithTime("PrintAsync START");
            _logger.LogWithTime("VALUES: " + _logger.NTS(await_layer) + _logger.NTS(fname) + _logger.NTS(fid));
            lastFile = false;
            isAwait = await_layer;


            if (PrParam.count == 0)
            {
                _logger.LogWithTime("PrintAsync INSIDE FIRST IF START");
                stateTempl.CurrentState = "Print Errore empty patch";
                LogList.Add(3, "Print Errore empty patch");
                stateTempl.color = Color.Orange;
                form.CurState = stateTempl;
                _logger.LogWithTime("PrintAsync INSIDE FIRST IF END");
                return;
            }
            form.CurState = stateTempl;
            _logger.LogWithTime("PrintAsync ChechConnection START");
            ChechConnection();
            _logger.LogWithTime("PrintAsync ChechConnection END");
            sTmpl.is_start = true.ToString();
            sTmpl.stop_after_layer = await_layer.ToString();

            if (await_layer)
            {
                _logger.LogWithTime("PrintAsync INSIDE SECOND IF START");
                sTmpl.current_file_path = fname;
                _logger.LogWithTime("PrintAsync INSIDE SECOND IF END");
            }
            else
                sTmpl.current_file_path = fname;
            _logger.LogWithTime("PrintAsync FTPLoad START");
            FTPLoad(fname, PrParam.patch + "\\" + fname);
            _logger.LogWithTime("PrintAsync FTPLoad STOP");
            isPrinting = true;
            _logger.LogWithTime("PrintAsync sendtoServ START");
            sendtoServ(sTmpl);
            _logger.LogWithTime("PrintAsync sendtoServ END");
            printing = true;
            PrParam.curnumb = fid;
            stateTempl.CurrentState = $"Printing file {PrParam.curnumb + 1}/{PrParam.count}";
            
            LogList.Add(1, $"Printing file { PrParam.curnumb + 1}/{ PrParam.count}"); // Это не мои логи
            _logger.LogWithTime($"Printing file { PrParam.curnumb + 1}/{ PrParam.count}"); // А вот это мои
            _logger.LogWithTime("fname: " + _logger.NTS(fname) + " fpath: " + _logger.NTS(PrParam.patch + "\\" + fname)); 
            _appendableMainLogger.LogWithTime($"Printing file { PrParam.curnumb + 1}/{ PrParam.count}");
            _appendableMainLogger.LogWithTime("fname: " + _logger.NTS(fname) + " fpath: " + _logger.NTS(PrParam.patch + "\\" + fname));
            
            
            stateTempl.color = Color.Green;
            PrParam.curnumb++;
            form.CurState = stateTempl;
            if (form.ct.IsCancellationRequested)
            {
                _logger.LogWithTime("PrintAsync INSIDE THIRD IF START");
                _logger.LogWithTime("PrintAsync INSIDE THIRD IF END");
                return;
            }

            while (PrParam.curnumb < PrParam.count)
            {
                _logger.LogWithTime("PrintAsync INSIDE WHILE START ITER");
                
                if (form.ct.IsCancellationRequested)
                {
                    _logger.LogWithTime("PrintAsync INSIDE WHILE RETURN IF");
                    return;
                }
                
                _logger.LogWithTime("PrintAsync CheckNext START");
                CheckNext();
                _logger.LogWithTime("PrintAsync CheckNext END, SendNextFile: " + _logger.NTS(SendNextFile));

                if (SendNextFile)
                {
                    _logger.LogWithTime("PrintAsync INSIDE SendNextFile IF START");

                    if (await_layer)
                    {
                        _logger.LogWithTime("PrintAsync INSIDE SendNextFile IF RETURN");
                        return;
                    }

                    else
                    {
                        int delay = 0;
                        var a = Form1.sets.Params.Where(c => c.ParamName == "NextLayerDelay").ToList();
                        if (a.Count > 0)
                        {
                            delay = Convert.ToInt32(a[0].ParamValue);
                        }

                        SendNextFile = false;
                        var userResult = AutoClosingMessageBox.Show("Все хорошо? Продолжаем?", "Caption", int.Parse(Form1.sets.GetParamByName("NextLayerDelay").ParamValue), MessageBoxButtons.YesNo);
                        if (userResult == System.Windows.Forms.DialogResult.Yes)
                        {

                           // PrParam.curnumb++;
                            if (PrParam.curnumb == PrParam.count) // Если последний файл
                            {
                                _logger.LogWithTime("PrintAsync INSIDE DOWNLOAD IF START");
                                sTmpl.is_start = true.ToString();
                                sTmpl.stop_after_layer = await_layer.ToString();
                                sTmpl.is_last_file = true.ToString();

                                sTmpl.current_file_path = PrParam.filename + $"_{ PrParam.curnumb + 1}" + extension;
                                
                                LogList.Add(1, $"Printing file { PrParam.curnumb + 1}/{ PrParam.count}");
                                _logger.LogWithTime($"Printing file { PrParam.curnumb + 1}/{ PrParam.count}");
                                _logger.LogWithTime("fname: " + PrParam.filename + $"_{ PrParam.curnumb + 1}" + extension + " fpath: " + PrParam.patch + "\\" + PrParam.filename + $"_{ PrParam.curnumb + 1 }" + extension); 
                                _appendableMainLogger.LogWithTime($"Printing file { PrParam.curnumb + 1}/{ PrParam.count}");
                                _appendableMainLogger.LogWithTime("fname: " + PrParam.filename + $"_{ PrParam.curnumb + 1}" + extension + " fpath: " + PrParam.patch + "\\" + PrParam.filename + $"_{ PrParam.curnumb + 1 }" + extension);
                                
                                _logger.LogWithTime("PrintAsync INSIDE DOWNLOAD IF FTPLoad START");
                                FTPLoad(PrParam.filename + $"_{ PrParam.curnumb + 1}" + extension, PrParam.patch + "\\" + PrParam.filename + $"_{ PrParam.curnumb + 1 }" + extension);
                                _logger.LogWithTime("PrintAsync INSIDE DOWNLOAD IF FTPLoad END");
                                _logger.LogWithTime("PrintAsync INSIDE DOWNLOAD IF sendtoServ START");
                                sendtoServ(sTmpl);
                                _logger.LogWithTime("PrintAsync INSIDE DOWNLOAD IF sendtoServ END");
                                printing = true;
                                lastFile = true;
                                _logger.LogWithTime("PrintAsync INSIDE DOWNLOAD IF END");

                            }
                            else // Если не последний файл
                            {
                                _logger.LogWithTime("PrintAsync INSIDE DOWNLOAD ELSE START");

                                sTmpl.is_start = true.ToString();

                                sTmpl.stop_after_layer = await_layer.ToString();
                                sTmpl.current_file_path = PrParam.filename + $"_{ PrParam.curnumb + 1}" + extension;
                                
                                _logger.LogWithTime($"Printing file { PrParam.curnumb + 1}/{ PrParam.count}");
                                _logger.LogWithTime("fname: " + PrParam.filename + $"_{ PrParam.curnumb + 1}" + extension + " fpath: " + PrParam.patch + "\\" + PrParam.filename + $"_{ PrParam.curnumb + 1 }" + extension); 
                                _appendableMainLogger.LogWithTime($"Printing file { PrParam.curnumb + 1}/{ PrParam.count}");
                                _appendableMainLogger.LogWithTime("fname: " + PrParam.filename + $"_{ PrParam.curnumb + 1}" + extension + " fpath: " + PrParam.patch + "\\" + PrParam.filename + $"_{ PrParam.curnumb + 1 }" + extension);
                                
                                _logger.LogWithTime("PrintAsync INSIDE DOWNLOAD ELSE FTPLoad START");
                                FTPLoad(PrParam.filename + $"_{ PrParam.curnumb + 1}" + extension, PrParam.patch + "\\" + PrParam.filename + $"_{ PrParam.curnumb + 1 }" + extension);
                                _logger.LogWithTime("PrintAsync INSIDE DOWNLOAD ELSE FTPLoad END");
                                
                                printing = true;
                                
                                _logger.LogWithTime("PrintAsync INSIDE DOWNLOAD ELSE sendtoServ START");
                                sendtoServ(sTmpl);
                                _logger.LogWithTime("PrintAsync INSIDE DOWNLOAD ELSE sendtoServ END")
                                    ;
                                _logger.LogWithTime("PrintAsync INSIDE DOWNLOAD ELSE END");

                            }
                            _logger.LogWithTime("PrintAsync AFTER DOWNLOAD IF ELSE START");
                            PrParam.curnumb++;
                            Thread.Sleep(2000);
                            LogList.Add(1, $"Printing file { PrParam.curnumb}/{ PrParam.count}");
                            stateTempl.CurrentState = $"Printing file {PrParam.curnumb}/{PrParam.count}";
                            stateTempl.color = Color.Green;

                            form.CurState = stateTempl;
                            
                            _logger.LogWithTime("PrintAsync AFTER DOWNLOAD IF ELSE END");

                        }

                    }
                    form.CurState = stateTempl;
                    Thread.Sleep(100);
                    
                    _logger.LogWithTime("PrintAsync INSIDE SendNextFile IF END");

                }

                _logger.LogWithTime("PrintAsync INSIDE WHILE END ITER");
            }
            isPrinting = false;
            
            _logger.LogWithTime("PrintAsync END");
        }


        public void printNext(string fname, int fid)
        {
            _logger.LogWithTime("printNext START");
            _logger.LogWithTime("printNext VALUES: " + _logger.NTS(fname) + " " + _logger.NTS(fid));

            _logger.LogWithTime("printNext ChechConnection START");
            ChechConnection();
            _logger.LogWithTime("printNext ChechConnection END");
            SendNextFile = false;
            //isPrinting = true;
            if (fid + 1 == PrParam.count)
            {
                _logger.LogWithTime("printNext INSIDE FIRST IF START");
                sTmpl.is_start = true.ToString();
                sTmpl.is_last_file = true.ToString();
                sTmpl.current_file_path = fname;
                _logger.LogWithTime("printNext INSIDE FIRST IF FTPLoad START");
                FTPLoad(fname, PrParam.patch + "\\" + fname);
                _logger.LogWithTime("printNext INSIDE FIRST IF FTPLoad END");
                
                _logger.LogWithTime("printNext INSIDE FIRST IF sendtoServ START");
                sendtoServ(sTmpl);
                _logger.LogWithTime("printNext INSIDE FIRST IF sendtoServ END");
                
                printing = true;
                lastFile = true;
                LogList.Add(1, $"Printing file { PrParam.curnumb+1}/{ PrParam.count}");
                form.CurState = stateTempl;
                _logger.LogWithTime("printNext INSIDE FIRST IF END");
            }
            else
            {
                _logger.LogWithTime("printNext INSIDE FIRST IF ELSE START");

                sTmpl.is_start = true.ToString();

                sTmpl.current_file_path = fname;
                _logger.LogWithTime("printNext INSIDE FIRST IF ELSE FTPLoad START");

                FTPLoad(fname, PrParam.patch + "\\" + fname);
                _logger.LogWithTime("printNext INSIDE FIRST IF ELSE FTPLoad END");
                _logger.LogWithTime("printNext INSIDE FIRST IF ELSE sendtoServ START");

                sendtoServ(sTmpl);
                _logger.LogWithTime("printNext INSIDE FIRST IF ELSE sendtoServ END");

                printing = true;
                LogList.Add(1, $"Printing file { PrParam.curnumb+1}/{ PrParam.count}");
                form.CurState = stateTempl;
                _logger.LogWithTime("printNext INSIDE FIRST IF ELSE FTPLoad END");

            }
            PrParam.curnumb = fid + 1;
            while (SendNextFile == false)
            {
                _logger.LogWithTime("printNext INSIDE WHILE FTPLoad START");

                if (form.ct.IsCancellationRequested)
                {
                    _logger.LogWithTime("printNext INSIDE WHILE FTPLoad RETURN");
                    return;
                }
                _logger.LogWithTime("printNext INSIDE WHILE FTPLoad CheckNext START");
                CheckNext();
                _logger.LogWithTime("printNext INSIDE WHILE FTPLoad CheckNext END");
                form.CurState = stateTempl;
                Thread.Sleep(100);
                _logger.LogWithTime("printNext INSIDE WHILE FTPLoad END");

            }
            if (lastFile)
            {
                isPrinting = false;
                _logger.LogWithTime("printNext INSIDE LastFile IF FTPLoad");

            }
            _logger.LogWithTime("printNext END");

        }
        private void sendtoServ(SendTampalte req)
        {
            _logger.LogWithTime("sendtoServ START");
            using (var client = new RequestSocket())
            {
                client.Connect($"tcp://{Addres}:5000");
                client.SendFrame(req.convertToString());
                var msg = client.ReceiveFrameString();
                if (msg != "1")
                {
                    stateTempl.color = Color.Red;
                    stateTempl.CurrentState = "format errore ";
                    form.CurState = stateTempl;
                }
            }
            _logger.LogWithTime("sendtoServ END");
        }

        private string lastState = "0";
        private bool printing = true;
        /// <summary>
        /// проверка необходимости отправки следующего файла работает пораллельно во время печати
        /// </summary>
        public void CheckNext()
        {
            _logger.LogWithTime("CheckNext START");
            using (var client = new RequestSocket())
            {
                Thread.Sleep(100);
                client.Connect($"tcp://{Addres}:5000");

                client.SendFrame("states");
                string message = client.ReceiveFrameString();
                var state = message;
                string z = "0";
                
                _logger.LogWithTime("CheckNext, message:" + _logger.NTS(message));
                try
                {
                    _logger.LogWithTime("CheckNext INSIDE TRY START, state:" + _logger.NTS(state));
                    state = message.Substring(0, message.IndexOf(';'));
                    z = message.Substring(message.IndexOf(';') + 1);
                    z = z.Substring(z.IndexOf('$') + 1);
                    state = state.Substring(state.IndexOf('$') + 1);
                    _logger.LogWithTime("CheckNext INSIDE TRY END, state:" + _logger.NTS(state));
                }
                catch (Exception)
                {
                    _logger.LogWithTime("CheckNext INSIDE CATCH START, state:" + _logger.NTS(state));

                    state = state.Substring(state.IndexOf('$') + 1);
                    
                    _logger.LogWithTime("CheckNext INSIDE CATCH END, state:" + _logger.NTS(state));

                }

                switch (state)
                {
                    case "0":
                        _logger.LogWithTime("CheckNext INSIDE case 0, state:" + _logger.NTS(state));

                        stateTempl.color = Color.Green;
                        stateTempl.CurrentState = "Ready to print";
                        //lastState = state;
                        LogList.Add(1, "Ready to print");
                        form.CurState = stateTempl;
                        if (printing)
                        {
                            SendNextFile = true;
                            printing = false;

                        }
                        else
                            SendNextFile = false;
                        
                        _logger.LogWithTime("CheckNext INSIDE case 0 END, state:" + _logger.NTS(state) + " SendNextFile: " + _logger.NTS(SendNextFile));

                        break;
                    case "1":
                        _logger.LogWithTime("CheckNext INSIDE case 1, state:" + _logger.NTS(state));

                        stateTempl.color = Color.Green;
                        lastState = state;
                        if (z == "0")
                        {
                            LogList.Add(1, $"Printing file { PrParam.curnumb}/{ PrParam.count}");
                            stateTempl.CurrentState = $"Printing file {PrParam.curnumb}/{PrParam.count}";
                        }
                        else
                        {
                            LogList.Add(2, "");
                            LogList.AddZ(float.Parse(z));
                            stateTempl.CurrentState = $"current hieght z={z}";
                        }
                        //stateTempl.CurrentState = $"Printing file {PrParam.curnumb}/{PrParam.count}, current z={z}";
                        form.CurState = stateTempl;
                        SendNextFile = false;
                        _logger.LogWithTime("CheckNext INSIDE case 1 END, state:" + _logger.NTS(state) + " SendNextFile: " + _logger.NTS(SendNextFile));
                        break;
                    case "2":
                        _logger.LogWithTime("CheckNext INSIDE case 2, state:" + _logger.NTS(state));

                        stateTempl.color = Color.Green;
                        lastState = state;
                        if (z == "0")
                        {
                            LogList.Add(1, $"Printing file { PrParam.curnumb}/{ PrParam.count}");
                            stateTempl.CurrentState = $"Printing file {PrParam.curnumb}/{PrParam.count}";
                        }
                        else
                        {
                            LogList.Add(2, "");
                            LogList.AddZ(float.Parse(z));
                            stateTempl.CurrentState = $"current hieght z={z}";
                        }
                        //stateTempl.CurrentState = $"Printing file {PrParam.curnumb}/{PrParam.count}, current z={z}";
                        form.CurState = stateTempl;
                        SendNextFile = false;
                        _logger.LogWithTime("CheckNext INSIDE case 2 END, state:" + _logger.NTS(state) + " SendNextFile: " + _logger.NTS(SendNextFile));

                        break;
                    default:
                        _logger.LogWithTime("CheckNext INSIDE default case START, state:" + _logger.NTS(state));

                        stateTempl.color = Color.Red;
                        stateTempl.CurrentState = "Connection errore";
                        form.CurState = stateTempl;
                        SendNextFile = false;
                        _logger.LogWithTime("CheckNext INSIDE default case END, state:" + _logger.NTS(state) + " SendNextFile: " + _logger.NTS(SendNextFile));

                        break;
                }




            }
            _logger.LogWithTime("CheckNext END");
        }

        /// <summary>
        /// проверяет связь с сервером работает в основном потоке
        /// </summary>
        public void ChechConnection()
        {
            _logger.LogWithTime("ChechConnection START");
            using (var client = new RequestSocket())
            {
                _logger.LogWithTime("ChechConnection Connect START");
                client.Connect($"tcp://{Addres}:5000");
                _logger.LogWithTime("ChechConnection Connect END");
                _logger.LogWithTime("ChechConnection SendFrame START");

                client.SendFrame("states");
                _logger.LogWithTime("ChechConnection SendFrame END");
                _logger.LogWithTime("ChechConnection ReceiveFrameString START");
                string message = client.ReceiveFrameString();
                _logger.LogWithTime("ChechConnection ReceiveFrameString END");

                _logger.LogWithTime("ChechConnection message: " + _logger.NTS(message));

                var state = message;
                //string z = "0";
                _logger.LogWithTime("ChechConnection Connect2 START");

                client.Connect($"tcp://{Addres}:5000");
                _logger.LogWithTime("ChechConnection Connect2 END");
                _logger.LogWithTime("ChechConnection SendFrame2 START");

                client.SendFrame("states");
                _logger.LogWithTime("ChechConnection SendFrame2 END");
                _logger.LogWithTime("ChechConnection ReceiveFrameString2 START");

                message = client.ReceiveFrameString();
                _logger.LogWithTime("ChechConnection ReceiveFrameString2 END");

                _logger.LogWithTime("ChechConnection message: " + _logger.NTS(message));


                string z = message;
                try
                {
                    state = message.Substring(0, message.IndexOf(';'));
                    z = message.Substring(message.IndexOf(';') + 1);
                    z = z.Substring(z.IndexOf('$') + 1);
                    state = state.Substring(state.IndexOf('$') + 1);
                }
                catch (Exception e)
                {
                    _logger.LogException(e);
                    state = state.Substring(state.IndexOf('$') + 1);
                }
                //state:1(0Готов  1Печать 2Необходим файл);z:0 
                switch (state)
                {
                    case "0":
                        stateTempl.color = Color.Green;
                        stateTempl.CurrentState = "Ready to print";
                        //form.ll.Add(1, "Ready to print");
                        //form.CurState = stateTempl;
                        break;
                    case "1":
                        stateTempl.color = Color.Green;
                        stateTempl.CurrentState = $"Printing file {PrParam.curnumb}/{PrParam.count}, current z={z}";
                        //form.ll.Add(1, $"Printing file {PrParam.curnumb}/{PrParam.count}");
                        break;
                    default:
                        stateTempl.color = Color.Red;
                        stateTempl.CurrentState = "Connection error";
                        LogList.Add(3, "Connection error");
                        form.CurState = stateTempl;
                        break;
                }
            }

            //form.UpdateRobotTable();
            _logger.LogWithTime("ChechConnection END");
        }
        public void ChechTest()
        {
            using (var client = new RequestSocket())
            {
                client.Connect($"tcp://{Addres}:5000");
                client.SendFrame("states");
                string message = client.ReceiveFrameString();
                var state = message;
                //string z = "0";
                client.Connect($"tcp://{Addres}:5000");
                client.SendFrame("states");
                message = client.ReceiveFrameString();

                string z = message;
                try
                {
                    state = message.Substring(0, message.IndexOf(';'));
                    z = message.Substring(message.IndexOf(';') + 1);
                    z = z.Substring(z.IndexOf('$') + 1);
                    state = state.Substring(state.IndexOf('$') + 1);
                }
                catch (Exception)
                {

                    state = state.Substring(state.IndexOf('$') + 1);
                }
                //state:1(0Готов  1Печать 2Необходим файл);z:0 
                switch (state)
                {
                    case "0":
                        stateTempl.CurrentState = $" current z={z}";
                        stateTempl.color = Color.Green;
                        stateTempl.CurrentState = "Ready to print";
                        //lastState = state;
                        form.CurState = stateTempl;

                        break;
                    case "1":
                        stateTempl.color = Color.Green;
                        lastState = state;
                        stateTempl.CurrentState = $" current z={z}";
                        form.CurState = stateTempl;
                        SendNextFile = false;
                        break;
                }
            }

            //form.UpdateRobotTable();
        }
        private void FTPLoad(string fileName, string pathWay)
        {
            _logger.LogWithTime("FTPLoad START");
            _logger.LogWithTime("FTPLoad VALUES: " + _logger.NTS(fileName) + _logger.NTS(pathWay));
            WebClient client = new WebClient();
            client.Credentials = new NetworkCredential("pi", "8");
            client.UploadFile(
                $"ftp://{Addres}/files/{fileName}", $"{pathWay}");
            _logger.LogWithTime("FTPLoad END");
        }



    }
}
