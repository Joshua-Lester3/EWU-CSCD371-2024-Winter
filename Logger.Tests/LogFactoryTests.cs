using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

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

    }
}
