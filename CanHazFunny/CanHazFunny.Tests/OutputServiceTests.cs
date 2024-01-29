using System.IO;
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CanHazFunny.Tests;

[TestClass]
public class OutputServiceTests
{
    private TextWriter? _OldOut;
    private StringWriter? _NewOut;
    private OutputService? _Service;

    [TestInitialize]
    public void Init()
    {
        _OldOut = Console.Out;
        _NewOut = new StringWriter();
        Console.SetOut(_NewOut);
        Console.SetError(_NewOut);
        _Service = new OutputService();
    }

    [TestCleanup]
    public void Cleanup()
    {
        Console.SetOut(_OldOut!);
        Console.SetError(_OldOut!);
    }

    [TestMethod]
    public void Output_PassInNonNullString_PrintsStringToOut()
    {
        // Arrange

        // Act
        bool result = _Service!.Output("Hello Jeff!");

        // Assert
        Assert.AreEqual<string>("Hello Jeff!", _NewOut!.ToString());
    }

    [TestMethod]
    public void Output_PassInNullString_ThrowsException()
    {
        // Arrange

        // Act

        // Assert
        Assert.ThrowsException<ArgumentNullException>(() => _Service!.Output(null!));
    }

    [TestMethod]
    public void Output_AssignedToIOutputable_PrintsStringToOut()
    {
        // Arrange
        IOutputable outable = _Service!;

        // Act
        outable.Output("Hello Jeff!");

        // Assert
        Assert.AreEqual<string>("Hello Jeff!", _NewOut!.ToString());
    }

    [TestMethod]
    public void Output_PassInNonNullValue_ReturnsTrue()
    {
        IOutputable outputable = new OutputService();
        bool result = outputable.Output("hi");
        Assert.IsTrue(result);
    }
}
