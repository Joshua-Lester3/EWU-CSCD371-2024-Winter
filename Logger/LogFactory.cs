namespace Logger;

public class LogFactory
{
    public string? FileName { get; set; }

    // TODO: as said in class, we don't want to require configurefilelogger in order to return a valid output. Combine into CreateLogger
    public BaseLogger? CreateLogger(string className) => 
        FileName is null ? null : new FileLogger(className, FileName);

    public void ConfigureFileLogger(string fileName) => FileName=fileName;
}
