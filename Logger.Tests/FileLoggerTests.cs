using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Globalization;
using System.IO;

namespace Logger.Tests;

[TestClass]
public class FileLoggerTests
{

    [TestMethod]
    [DataRow("Jeff")]
    public void NameProperty_NonNullInputString_Success(string expectedResult)
    {
        // Arrange
        FileLogger logger = new()
        {
            ClassName = expectedResult
        };

        // Assert
        Assert.AreEqual(expectedResult, logger.ClassName);
    }

    [TestMethod]
    [DataRow("hi!!!")]
    public void Log_FileDoesNotExist_SuccessfullyCreatesAndAppends(string loggedString)
    {
        // Arrange
        string path = Path.Combine(Environment.CurrentDirectory, "Text.txt");
        FileLogger fileLogger = new()
        {
            FilePath = path
        };

        // Act
        bool containsExpectedOutput = FileLoggerTests.DoesLoggerLog(fileLogger, path, loggedString);

        // Assert
        Assert.IsTrue(containsExpectedOutput);
    }

    [TestMethod]
    public void Log_FileExists_SuccessfullyAppends()
    {
        // Arrange
        string path = Path.Combine(Environment.CurrentDirectory, "Text.txt");
        File.Create(path).Dispose();
        FileLogger fileLogger = new()
        {
            FilePath = path
        };

        // Act
        bool containsExpectedOutput = FileLoggerTests.DoesLoggerLog(fileLogger, path, "hi");

        // Assert
        Assert.IsTrue(containsExpectedOutput);
    }

    // DoesFileLoggerLog is a public, static method to reduce repeated code
    // across FileLoggerTests.cs and LogFactoryTests.cs
    public static bool DoesLoggerLog(BaseLogger? fileLogger, string filePath, string message)
    {
        // Act
        fileLogger?.Log(LogLevel.Information, message);
        string[] allLines = File.ReadAllLines(filePath);
        string appendedLine = allLines[allLines.Length - 1];
        File.Delete(filePath);

        // Check
        string date = DateTime.Now.ToString("MM/dd/yyyy", CultureInfo.CurrentCulture);
        return appendedLine.Contains(date) && appendedLine.Contains("Information: hi");
    }
}
