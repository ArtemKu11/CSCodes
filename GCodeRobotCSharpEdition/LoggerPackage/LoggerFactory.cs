using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace GCodeRobotCSharpEdition.LoggerPackage
{
    public static class LoggerFactory
    {
        private static readonly Dictionary<string, Logger> ExistingLoggers = new();
        private static bool _enabledCheckFlag;
        private static bool _enabled;
        

        public static Logger GetNewLogger(string name)
        {
            if (name.ToLowerInvariant().StartsWith("appendable"))
            {
                throw new Exception("Обычный логгер не может быть appendable. Используйте GetAppendableLogger()");
            }

            if (ExistingLoggers.TryGetValue(name, out var logger))
            {
                throw new Exception("Логгер с именем " + name + " уже существует");
            }

            logger = new Logger(name);
            ExistingLoggers.Add(name, logger);
            return logger;
        }

        public static Logger GetExistingLogger(string name)
        {
            if (ExistingLoggers.TryGetValue(name, out var logger))
            {
                return logger;
            }

            throw new Exception("Логгер с именем " + name + " не существует");
        }

        public static Logger GetExistingOrCreateNewLogger(string name)
        {
            return ExistingLoggers.TryGetValue(name, out var logger) ? logger : GetNewLogger(name: name);
        }

        public static Logger GetAppendableLogger(string name)
        {
            if (ExistingLoggers.TryGetValue("Appendable" + name, out var logger))
            {
                return logger;
            }

            logger = new Logger("Appendable" + name);
            ExistingLoggers.Add("Appendable" + name, logger);
            ExistingLoggers.Add(name, logger);
            return logger;
        }

        public static bool IsEnabled()
        {
            if (_enabledCheckFlag)
            {
                return _enabled;
            }

            if (!Directory.Exists("logs/"))
            {
                Directory.CreateDirectory("logs/");
            }

            if (File.Exists("logs/logs.yaml"))
            {
                _enabled = ReadEnabledStatus();
                _enabledCheckFlag = true;
                return _enabled;
            }

            _enabled = CreateLogYamlFile();
            _enabledCheckFlag = true;
            return _enabled;
        }

        private static bool ReadEnabledStatus()
        {
            string[] lines = File.ReadAllLines("logs/logs.yaml");
            foreach (string line in lines)
            {
                if (line.ToLowerInvariant().Contains("enable: true"))
                {
                    return true;
                }

                if (line.ToLowerInvariant().Contains("enable: false"))
                {
                    return false;
                }
            }

            string[] falseLine = { "enable: false  # true/false" };
            File.Create("logs/logs.yaml").Close();
            File.AppendAllLines("logs/logs.yaml", falseLine);
            return false;
        }

        private static bool CreateLogYamlFile()
        {
            File.Create("logs/logs.yaml").Close();
            string[] falseLine = { "enable: false  # true/false" };
            File.AppendAllLines("logs/logs.yaml", falseLine);
            return false;
        }
    }

    public class Logger
    {
        private readonly string _loggerPath;
        private readonly string _name;
        private static bool _ignoreInsideEnabledChecks = false; // false - внутри каждого log метода будет проверка, включены ли логи

        public Logger(string name)
        {
            _name = name;
            _loggerPath = "logs/" + name + ".log";
            if (!Directory.Exists("logs/"))
            {
                Directory.CreateDirectory("logs/");
            }

            if (name.StartsWith("Appendable"))
            {
                CheckLogFileExist(_loggerPath);
                CheckFileSizeLimit(_loggerPath);
            }
            else
            {
                File.Create(_loggerPath).Close();
            }
        }

        public string NTS(object obj) // NullSafetyToString
        {
            if (obj is null)
            {
                return "null";
            }
            else
            {
                return obj.ToString();
            }
        }
        
        private void CheckLogFileExist(string path)
        {
            if (!File.Exists(path))
            {
                File.Create(path).Close();
            }
        }

        private void CheckFileSizeLimit(string path)
        {
            FileInfo info = new FileInfo(path);
            if (info.Length > 5000000)
            {
                File.Create(path).Close();
            }
        }

        private bool InsideEnabledCheck()
        {
            return _ignoreInsideEnabledChecks || IsEnabled();
        }
        
        public void Log(string logText)
        {
            if (!_name.StartsWith("Appendable"))
            {
                if (!InsideEnabledCheck())
                {
                    return;
                }
            }
            
            
            string[] logLines = { logText };
            File.AppendAllLines(_loggerPath, logLines);
        }
        
        public void LogWithTime(string logText)
        {
            if (!_name.StartsWith("Appendable"))
            {
                if (!InsideEnabledCheck())
                {
                    return;
                }
            }
            
            var dateTimeNow = DateTime.Now;
            string[] logLines = { dateTimeNow + " " + logText };
            File.AppendAllLines(_loggerPath, logLines);
        }

        public void LogStartOfMethod(MethodInfo methodInfo, IEnumerable<IComparable> args, DateTime dateTime)
        {
            if (!_name.StartsWith("Appendable"))
            {
                if (!InsideEnabledCheck())
                {
                    return;
                }
            }
            
            // string methodName = methodInfo.ReflectedType + " " + methodInfo.Name;
            string methodName = methodInfo.ReflectedType + " " + methodInfo;
            string template = dateTime + " " + methodName + " started with args: ";
            // int counter = 1;
            if (args is null)
            {
                template += "null, ";
            }
            else
            {
                foreach (var comparable in args)
                {
                    if (comparable is not null)
                    {
                        // template = template + counter + ". " + comparable + " тип: " + comparable.GetType() + ", ";
                        template = template + comparable + ", ";
                    }
                    else
                    {
                        // template = template + counter + ". " + "null" + " тип: null, ";
                        template += "null, ";
                    }

                    // counter++;
                }
            }

            

            if (template.EndsWith(", "))
            {
                template = template[..^2];
            }

            Log(template);
        }

        public void LogEndOfMethod(MethodInfo methodInfo, IEnumerable<IComparable> args, object? result,
            TimeSpan timeSpan)
        {
            if (!_name.StartsWith("Appendable"))
            {
                if (!InsideEnabledCheck())
                {
                    return;
                }
            }
            
            DateTime dateTime = DateTime.Now;
            // string methodName = methodInfo.ReflectedType + " " + methodInfo.Name;
            string methodName = methodInfo.ReflectedType + " " + methodInfo;
            string template = dateTime + " " + methodName + " ended with args: ";

            if (args is null)
            {
                template += "null, ";
            }
            else
            {
                foreach (var comparable in args)
                {
                    if (comparable is not null)
                    {
                        // template = template + counter + ". " + comparable + " тип: " + comparable.GetType() + ", ";
                        template = template + comparable + ", ";
                    }
                    else
                    {
                        // template = template + counter + ". " + "null" + " тип: null, ";
                        template += "null, ";
                    }

                    // counter++;
                }
            }

            if (result is not null)
            {
                // template = template + "result: " + result + ", тип: " + result.GetType();
                template = template + "result: " + result;
            }
            else
            {
                template += "result: null";
            }

            template = template + ", Время выполнения: " + timeSpan;
            Log(template);
        }

        public void LogException(Exception e)
        {
            if (!_name.StartsWith("Appendable"))
            {
                if (!InsideEnabledCheck())
                {
                    return;
                }
            }
            
            DateTime dateTime = DateTime.Now;
            Log(dateTime + "\n" + e);
        }

        public static bool IsEnabled()
        {
            return LoggerFactory.IsEnabled();
        }
    }
}