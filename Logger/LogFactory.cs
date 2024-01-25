using System;
using System.IO;

namespace Logger;

public class LogFactory
{
    public LogFactory(string? filePath)
    {
        _FilePath = filePath;
    }
    private string? _FilePath;
    public void ConfigureFileLogger(string? filePath)
    {
        ArgumentNullException.ThrowIfNull(filePath);
        _FilePath = filePath;
    }

    public BaseLogger? CreateLogger(string className)
    {
        ConfigureFileLogger(_FilePath);
        if (_FilePath == null || _FilePath == "")
        {
            return null;
        } else
        {
            BaseLogger logger = new FileLogger()
            {
               ClassName = className,
               FilePath = _FilePath
            };
            return logger;
        }
    }
}
