using System;
using System.IO;

namespace Logger;

public class FileLogger : BaseLogger
{
    public string FilePath
    {
        get => _FilePath;
        set
        {
            _FilePath = value;
            if (!File.Exists(value))
            {
                File.Create(value);
            }
        }
    }
    private string _FilePath;
    public override void Log(LogLevel logLevel, string message)
    {
        string output = DateTime.Now.ToString();
        output += " " + ClassName + " " + logLevel + ": " + message;
        File.WriteAllText(FilePath, Environment.NewLine + output);
    }
}
