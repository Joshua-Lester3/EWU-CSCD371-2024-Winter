using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;

namespace Logger.Tests;

[TestClass]
public class LogFactoryTests
{
    [TestMethod]
    public void CreateLogger_NonNullInputString_ReturnsBaseLogger()
    {
        LogFactory logFactory = new LogFactory();
        logFactory.ConfigureFileLogger(Environment.CurrentDirectory + "Text.txt");
        BaseLogger logger = logFactory.CreateLogger("Jeff");
        Assert.IsTrue(logger is BaseLogger);
    }

    [TestMethod]
    public void CreateLogger_FilePathExists_WritesToFile()
    {
        string filePath = Path.Combine(Environment.CurrentDirectory, "Text.txt");
        LogFactory logFactory = new LogFactory();
        logFactory.ConfigureFileLogger(filePath);
        BaseLogger fileLogger = logFactory.CreateLogger("Jeff");
        bool containsExpectedOutput = FileLoggerTests.DoesLoggerLog(fileLogger, filePath, "hi Jim!");
        Assert.IsTrue(containsExpectedOutput);
    }

    [TestMethod]
    public void CreateLogger_DidNotCallConfigureFileLogger_ReturnsNull()
    {
        LogFactory logFactory = new LogFactory();
        Assert.IsNull(logFactory.CreateLogger("Jeff!"));
    }
}
