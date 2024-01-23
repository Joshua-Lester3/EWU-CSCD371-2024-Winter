#nullable enable

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
        // Arrange
        LogFactory logFactory = new();
        logFactory.ConfigureFileLogger(Environment.CurrentDirectory + "Text.txt");

        // Act
        BaseLogger? logger = logFactory.CreateLogger("Jeff");

        // Assert
        Assert.IsTrue(logger is BaseLogger);
    }

    [TestMethod]
    public void CreateLogger_FilePathExists_WritesToFile()
    {
        // Arrange
        string filePath = Path.Combine(Environment.CurrentDirectory, "Text.txt");
        LogFactory logFactory = new();
        logFactory.ConfigureFileLogger(filePath);

        // Act
        BaseLogger? fileLogger = logFactory.CreateLogger("Jeff");

        // Assert
        bool containsExpectedOutput = FileLoggerTests.DoesLoggerLog(fileLogger, filePath, "hi Jim!");
        Assert.IsTrue(containsExpectedOutput);
    }

    [TestMethod]
    public void CreateLogger_DidNotCallConfigureFileLogger_ReturnsNull()
    {
        // Arrange
        LogFactory logFactory = new();

        // Act

        //Assert
        Assert.IsNull(logFactory.CreateLogger("Jeff!"));
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException), "filePath is null")]
    public void ConfigureLogger_FilePathNull_ThrowException()
    {
        // Arrange
        LogFactory factory = new();

        // Act
        factory.ConfigureFileLogger(null);
    }
}
