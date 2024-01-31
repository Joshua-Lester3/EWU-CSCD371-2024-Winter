using System;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace CanHazFunny.Tests;

[TestClass]
public class JesterTests : IDisposable
{
    private Mock<IOutputable> Outputable { get; set; }
    private Mock<IJokeable> Jokeable { get; set; }
    private static int Counter { get; set; }
    private bool Disposed { get; set; }
    private TextWriter? OldOut { get; set; }
    private StringWriter? NewOut { get; set; }

    public JesterTests()
    {
        Outputable = new Mock<IOutputable>();
        Jokeable = new Mock<IJokeable>();
    }

    [TestMethod]
    public void JesterConstructor_NonNullParams_DoesNotThrowException()
    {
        try
        {
            Jester jester = new(Outputable.Object, Jokeable.Object);
        }
        catch (ArgumentNullException ex)
        {
            Assert.Fail(ex.Message);
        }
        return;
    }

    [TestMethod]
    public void JesterConstructor_NullIOutputable_ThrowsArgumentNullException()
    {
        Assert.ThrowsException<ArgumentNullException>(() => { Jester jester = new(null!, Jokeable.Object); });
    }

    [TestMethod]
    public void JesterConstructor_NullIJokeable_ThrowsArgumentNullException()
    {
        Assert.ThrowsException<ArgumentNullException>(() => { Jester jester = new(Outputable.Object, null!); });
    }

    [TestMethod]
    public void TellJoke_DoesNotContainChuckNorris_PrintsJoke()
    {
        SetOut();
        Jokeable
            .Setup(x => x.GetJoke())
            .Returns("haha funny joke");

        Jester jester = new(new OutputService(), Jokeable.Object);
        jester.TellJoke();
        Assert.AreEqual<string>("haha funny joke", NewOut!.ToString());
        ResetOut();
    }

    [TestMethod]
    public void TellJoke_ContainsChuckNorris_PrintsJoke()
    {
        SetOut();
        Jokeable
            .Setup(x => x.GetJoke())
            .Returns(() =>
            {
                if (JesterTests.Counter == 0)
                {
                    JesterTests.Counter++;
                    return "haha funny joke about Chuck Norris";
                }
                else
                {
                    return "haha funny joke";
                }
            });
        Outputable
            .Setup(x => x.Output(It.IsAny<string>()))
            .Returns((string s) =>
            {
                Console.Write(s);
                return true;
            });

        Jester jester = new(Outputable.Object, Jokeable.Object);
        jester.TellJoke();
        Assert.AreEqual<string>("haha funny joke", NewOut!.ToString());
        ResetOut();
    }

    private void SetOut()
    {
        ObjectDisposedException.ThrowIf(Disposed, NewOut!);
        OldOut = Console.Out;
        NewOut = new StringWriter();
        Console.SetOut(NewOut);
        Console.SetError(NewOut);
    }

    private void ResetOut()
    {
        Console.SetOut(OldOut!);
        Console.SetError(OldOut!);
        Dispose();
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