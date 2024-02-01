using System.IO;
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CanHazFunny.Tests;

[TestClass]
public class OutputServiceTests : IDisposable
{
    // We know OldOut and NewOut will not be null when used because they are set
    // in the beginning of every test that utilizes them
    #pragma warning disable CS8618
    private TextWriter OldOut { get; set; }
    private StringWriter NewOut { get; set; }
    private OutputService Service { get; set; }
    private bool Disposed { get; set; }
    #pragma warning restore CS8618

    [TestInitialize]
    public void Init()
    {
        ObjectDisposedException.ThrowIf(Disposed, NewOut!);
        OldOut = Console.Out;
        NewOut = new StringWriter();
        Console.SetOut(NewOut);
        Console.SetError(NewOut);
        Service = new OutputService();
    }

    [TestCleanup]
    public void Cleanup()
    {
        Console.SetOut(OldOut!);
        Console.SetError(OldOut!);
        Dispose();
    }

    [TestMethod]
    public void Output_PassInNonNullString_PrintsStringToOut()
    {
        // Arrange

        // Act
        bool result = Service.Output("Hello Jeff!");

        // Assert
        Assert.AreEqual<string>("Hello Jeff!", NewOut.ToString());
    }

    [TestMethod]
    public void Output_PassInNullString_ThrowsException()
    {
        // Arrange

        // Act

        // Assert
        Assert.ThrowsException<ArgumentNullException>(() => Service.Output(null!));
    }

    [TestMethod]
    public void Output_AssignedToIOutputable_PrintsStringToOut()
    {
        // Arrange

        // Act
        Service.Output("Hello Jeff!");

        // Assert
        Assert.AreEqual<string>("Hello Jeff!", NewOut.ToString());
    }

    [TestMethod]
    public void Output_PassInNonNullValue_ReturnsTrue()
    {
        bool result = Service.Output("hi");
        Assert.IsTrue(result);
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (Disposed)
        {
            return;
        }

        if (disposing)
        {
            if (NewOut != null)
            {
                NewOut.Dispose();
            }
            Disposed = true;
        }
    }
}
