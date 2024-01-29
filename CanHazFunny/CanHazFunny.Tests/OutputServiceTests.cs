using System.IO;
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CanHazFunny.Tests;

[TestClass]
public class OutputServiceTests : IDisposable
{
    private TextWriter? _OldOut;
    private StringWriter? _NewOut;
    private OutputService? _Service;
    private bool _Disposed;

    [TestInitialize]
    public void Init()
    {
        ObjectDisposedException.ThrowIf(_Disposed, _NewOut!);
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
        Dispose();
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
        OutputService service = _Service!;

        // Act
        service.Output("Hello Jeff!");

        // Assert
        Assert.AreEqual<string>("Hello Jeff!", _NewOut!.ToString());
    }

    [TestMethod]
    public void Output_PassInNonNullValue_ReturnsTrue()
    {
        OutputService service = new();
        bool result = service.Output("hi");
        Assert.IsTrue(result);
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (_Disposed)
        {
            return;
        }

        if (disposing)
        {
            if (_NewOut != null)
            {
                _NewOut.Dispose();
            }
            _Disposed = true;
        }
    }
}
