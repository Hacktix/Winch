using System;
using System.IO;
using Winch.Config;

namespace Winch.Logging
{
    public class LogFile
    {
        private string LogPath;

        private static string DefaultLogfile()
        {
            DateTime now = DateTime.Now;
            return $"Winch-{now.Year:0000}-{now.Month:00}-{now.Day:00}-{now.Hour:00}_{now.Minute:00}.log";
        }

        public LogFile() : this(DefaultLogfile()) { }

        public LogFile(string filename)
        {
            string logBasePath = WinchConfig.GetProperty("LogsFolder", "Logs");
            string logPath = Path.Combine(logBasePath, filename);

            if(!Directory.Exists(logBasePath))
                Directory.CreateDirectory(logBasePath);

            if (File.Exists(logPath))
                File.Delete(logPath);

            LogPath = logPath;
        }

        public void Write(string message)
        {
            File.AppendAllText(LogPath, message + Environment.NewLine);
        }
    }
}
