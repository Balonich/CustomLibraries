namespace Logger
{
    using System;
    using System.IO;

    public class Logger
    {
        private const string LogsDirPath = @".\Logs\";

        public Logger()
        {
            CreateLogsDir();
        }

        public Logger(string logsFileName)
        : this()
        {
            LogsFileName = logsFileName;
        }

        public string LogsFileName { get; set; } = "logs.txt";

        public void LogToFile(string text)
        {
            string logsFilePath = LogsDirPath + LogsFileName;
            FileInfo logsFile = new(logsFilePath);

            if (logsFile.Exists && logsFile.Length > 50000)
            {
                logsFile.MoveTo(logsFilePath + ((DateTimeOffset)DateTime.UtcNow).ToUnixTimeMilliseconds().ToString());
            }

            using (StreamWriter stream = new(logsFilePath, true))
            {
                stream.WriteLine(text);
            }
        }

        private static void CreateLogsDir()
        {
            DirectoryInfo logsDir = new(LogsDirPath);
            if (!logsDir.Exists)
            {
                logsDir.Create();
            }
        }
    }
}