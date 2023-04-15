using System;
using System.Reflection;
using Winch.Config;

namespace Winch.Logging
{
    enum LogLevel
    {
        DEBUG = 1, INFO = 2, WARN = 3, ERROR = 4
    }

    public class Logger
    {
        private LogFile _log;
        private LogFile _latestLog;

        private bool _writeLogs;
        private LogLevel _minLogLevel;

        public Logger()
        {
            _writeLogs = WinchConfig.GetProperty("WriteLogsToFile", true);
            if (!_writeLogs)
                return;

            _minLogLevel = (LogLevel)Enum.Parse(typeof(LogLevel), WinchConfig.GetProperty("LogLevel", "DEBUG"));
            _log = new LogFile();
            _latestLog = new LogFile("latest.log");
        }

        private void Log(LogLevel level, string message)
        {
            if (!_writeLogs)
                return;
            if (level < _minLogLevel)
                return;

            string logMessage = $"[{GetLogTimestamp()}] [{level,-5}] [{GetSourceTag()}] : {message}";
            _log.Write(logMessage);
            _latestLog.Write(logMessage);
        }

        private string GetLogTimestamp()
        {
            DateTime now = DateTime.Now;
            return $"{now.Year:0000}-{now.Month:00}-{now.Day:00} {now.Hour:00}:{now.Minute:00}:{now.Second:00}";
        }

        private string GetSourceTag()
        {
            return $"{Assembly.GetCallingAssembly().GetName().Name}";
        }



        public void Debug(object message) { Log(LogLevel.DEBUG, message.ToString()); }
        public void Info(object message) { Log(LogLevel.INFO, message.ToString()); }
        public void Warn(object message) { Log(LogLevel.WARN, message.ToString()); }
        public void Error(object message) { Log(LogLevel.ERROR, message.ToString()); }
    }
}
