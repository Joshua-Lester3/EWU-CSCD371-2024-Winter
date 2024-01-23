#nullable enable

using System;

namespace Logger;

public static class BaseLoggerMixins
{
    public static void Error(this BaseLogger? logger, string message, params string[] messages)
    {
        LogHelper(logger, LogLevel.Error, message, messages);
    }

    public static void Warning(this BaseLogger? logger, string message, params string[] messages)
    {
        LogHelper(logger, LogLevel.Warning, message, messages);
    }

    public static void Information(this BaseLogger? logger, string message, params string[] messages)
    {
        LogHelper(logger, LogLevel.Information, message, messages);
    }

    public static void Debug(this BaseLogger? logger, string message, params string[] messages)
    {
        LogHelper(logger, LogLevel.Debug, message, messages);
    }

    private static void LogHelper(BaseLogger? logger, LogLevel level, string message, params string[] messages)
    {
        ArgumentNullException.ThrowIfNull(nameof(logger));
        string fullMessage = message;
        foreach (string element in messages)
        {
            fullMessage += $" {element}";
        }
        logger?.Log(level, fullMessage);
    }
}
