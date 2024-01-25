using System;

namespace Logger;

public static class BaseLoggerMixins
{
    public static void Error(this BaseLogger? logger, string message, params object[] args)
    {
        LogHelper(logger, LogLevel.Error, message, args);
    }

    public static void Warning(this BaseLogger? logger, string message, params object[] args)
    {
        LogHelper(logger, LogLevel.Warning, message, args);
    }

    public static void Information(this BaseLogger? logger, string message, params object[] args)
    {
        LogHelper(logger, LogLevel.Information, message, args);
    }

    public static void Debug(this BaseLogger? logger, string message, params object[] args)
    {
        LogHelper(logger, LogLevel.Debug, message, args);
    }

    private static void LogHelper(BaseLogger? logger, LogLevel level, string message, params object[] args)
    {
        ArgumentNullException.ThrowIfNull(logger);
        string fullMessage = string.Format(message, args);
        logger?.Log(level, fullMessage);
    }
}
