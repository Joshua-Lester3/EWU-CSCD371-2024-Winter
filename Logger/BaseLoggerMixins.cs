using System;

namespace Logger;

public static class BaseLoggerMixins
{
    public static void Error(this BaseLogger logger, string message, params string[] messages)
    {
        if (logger == null)
        {
            throw new ArgumentNullException(nameof(logger));
        }
        string fullMessage = message;
        foreach (string element in messages) 
        {
            fullMessage += $" {element}";
        }
        logger.Log(LogLevel.Error, fullMessage);
    }

    public static void Warning(this BaseLogger logger, string message, params string[] messages)
    {
        if (logger == null)
        {
            throw new ArgumentNullException(nameof(logger));
        }
        string fullMessage = message;
        foreach (string element in messages)
        {
            fullMessage += $" {element}";
        }
        logger.Log(LogLevel.Warning, fullMessage);
    }

    public static void Information(this BaseLogger logger, string message, params string[] messages)
    {
        if (logger == null)
        {
            throw new ArgumentNullException(nameof(logger));
        }
        string fullMessage = message;
        foreach (string element in messages)
        {
            fullMessage += $" {element}";
        }
        logger.Log(LogLevel.Information, fullMessage);
    }

    public static void Debug(this BaseLogger logger, string message, params string[] messages)
    {
        if (logger == null)
        {
            throw new ArgumentNullException(nameof(logger));
        }
        string fullMessage = message;
        foreach (string element in messages)
        {
            fullMessage += $" {element}";
        }
        logger.Log(LogLevel.Debug, fullMessage);
    }
}
