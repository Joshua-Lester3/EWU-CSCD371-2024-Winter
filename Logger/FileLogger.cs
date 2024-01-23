#nullable enable
using System;
using System.Globalization;
using System.IO;

namespace Logger;

public class FileLogger : BaseLogger
{

    public string FilePath
    {
        init
        {
            _FilePath = value;
            if (!File.Exists(value))
            {
                File.Create(value).Dispose();
            }
        }
    }
    private readonly string? _FilePath;
    public override void Log(LogLevel logLevel, string message)
    {
        string output = DateTime.Now.ToString("MM/dd/yyyy hh:mm:ss tt", CultureInfo.CurrentCulture);
        output += " " + nameof(FileLogger) + " " + logLevel + ": " + message;
        File.AppendAllText(_FilePath!, Environment.NewLine + output);
    }
}
