#nullable enable
using System;
using System.IO;

namespace Logger;

public class LogFactory
{
    private string? _FilePath;
    public void ConfigureFileLogger(string filePath)
    {
        if (filePath is null)
        {
            throw new ArgumentNullException("filePath is null");
        }
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
