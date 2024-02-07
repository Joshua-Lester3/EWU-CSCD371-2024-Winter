namespace Logger;

// TODO: Make this class abstract because it seems it shouldn't be instantiated
public class BaseLoggerConfiguration : ILoggerConfiguration
{
    public BaseLoggerConfiguration(string logSource) => LogSource = string.IsNullOrWhiteSpace(logSource)
            ? throw new ArgumentException($"'{nameof(logSource)}' cannot be null or whitespace.", nameof(logSource))
            : logSource;
    
    public string LogSource { get; }
    
}