using Microsoft.VisualStudio.TestTools.UnitTesting;
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
        FileLogger logger = new();

        // Act
        logger.ClassName = expectedResult;

        // Assert
        Assert.AreEqual(expectedResult, logger.ClassName);
    }

    [TestMethod]
    [DataRow("hi!!!")]
    public void Log_FileExists_SuccessfullyAppends(string loggedString)
    {
        // Arrange
        string path = "Text.txt";
        File.Create(path);
        FileLogger fileLogger = new()
        {
            FilePath = path
        };

        // Act
        fileLogger.Log(LogLevel.Information, loggedString);
        string[] allLines = File.ReadAllLines(path);
        string appendedLine = allLines[allLines.Length - 1];
        File.Delete(path);

        // Assert
        Assert.IsTrue(appendedLine.Contains(loggedString));
    }
}
