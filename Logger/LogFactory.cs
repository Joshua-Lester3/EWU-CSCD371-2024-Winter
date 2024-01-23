using System;
using System.IO;

namespace Logger;

public class LogFactory
{
    private string? _FilePath;
    public void ConfigureFileLogger(string? filePath)
    {
        ArgumentNullException.ThrowIfNull(filePath);
        _FilePath = filePath;
    }

    public BaseLogger? CreateLogger(string className)
    {
        if (_FilePath == null)
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
