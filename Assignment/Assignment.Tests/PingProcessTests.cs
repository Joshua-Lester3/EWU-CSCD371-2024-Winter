using IntelliTect.TestTools;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Threading;
using System.Threading.Tasks;

namespace Assignment.Tests;

[TestClass]
public class PingProcessTests
{
    PingProcess Sut { get; set; } = new();

    [TestInitialize]
    public void TestInitialize()
    {
        Sut = new();
    }

    [TestMethod]
    public void Start_PingProcess_Success()
    {
        Process process = Process.Start("ping", "localhost");
        process.WaitForExit();
        Assert.AreEqual<int>(0, process.ExitCode);
    }

    [TestMethod]
    public void Run_GoogleDotCom_Success()
    {
        int exitCode = Sut.Run("google.com").ExitCode;
        Assert.AreEqual<int>(0, exitCode);
    }


    [TestMethod]
    public void Run_InvalidAddressOutput_Success()
    {
        (int exitCode, string? stdOutput) = Sut.Run("badaddress");
        Assert.IsFalse(string.IsNullOrWhiteSpace(stdOutput));
        stdOutput = WildcardPattern.NormalizeLineEndings(stdOutput!.Trim());
        Assert.AreEqual<string?>(
            "Ping request could not find host badaddress. Please check the name and try again.".Trim(),
            stdOutput,
            $"Output is unexpected: {stdOutput}");
        Assert.AreEqual<int>(1, exitCode);
    }

    [TestMethod]
    public void Run_CaptureStdOutput_Success()
    {
        PingResult result = Sut.Run("localhost");
        AssertValidPingOutput(result);
    }

    [TestMethod]
    public void RunTaskAsync_Success()
    {
        // Arrange

        // Act
        PingResult result = Sut.RunTaskAsync("localhost").Result;

        // Assert
        AssertValidPingOutput(result);
    }

    [TestMethod]
    public void RunAsync_UsingTaskReturn_Success()
    {
        //// Do NOT use async/await in this test.
        //PingResult result = default;
        //// Test Sut.RunAsync("localhost");
        //AssertValidPingOutput(result);

        // Arrange

        // Act
        Task<PingResult> task = Sut.RunAsync("localhost");
        PingResult result = task.Result;

        // Assert
        AssertValidPingOutput(result);
    }

    [TestMethod]
    async public Task RunAsync_UsingTpl_Success()
    {
        //// DO use async/await in this test.
        //PingResult result = default;

        //// Test Sut.RunAsync("localhost");
        //AssertValidPingOutput(result);

        // Arrange

        // Act
        PingResult result = await Sut.RunAsync("localhost");

        // Assert
        AssertValidPingOutput(result);
    }


    [TestMethod]
    [ExpectedException(typeof(AggregateException))]
    public void RunAsync_UsingTplWithCancellation_CatchAggregateExceptionWrapping()
    {
        // if supposed to be async, change aggregateexception to operationcanceledexception and void to Task

        // Arrange
        CancellationTokenSource cancellationTokenSource = new();
        cancellationTokenSource.Cancel();
        Task<PingResult> task = Sut.RunAsync("localhost", cancellationTokenSource.Token);

        // Act

        // Assert
        task.Wait();
    }

    [TestMethod]
    [ExpectedException(typeof(TaskCanceledException))]
    public void RunAsync_UsingTplWithCancellation_CatchAggregateExceptionWrappingTaskCanceledException()
    {
        // Arrange
        CancellationTokenSource cancellationTokenSource = new();
        cancellationTokenSource.Cancel();
        Task<PingResult> task = Sut.RunAsync("localhost", cancellationTokenSource.Token);

        // Act

        // Assert
        try
        {
            task.Wait();
        }
        catch (AggregateException aggregateException)
        {
            foreach (Exception ex in aggregateException.InnerExceptions)
            {
                if (ex is TaskCanceledException)
                {
                    ExceptionDispatchInfo.Capture(ex).Throw();
                }
            }
        }
    }

    [TestMethod]
    async public Task RunAsync_MultipleHostAddresses_True()
    {
        // Pseudo Code - don't trust it!!!
        string[] hostNames = new string[] { "localhost", "localhost", "localhost", "localhost" };
        int expectedLineCount = PingOutputLikeExpression.Split(Environment.NewLine).Length*hostNames.Length;
        PingResult result = await Sut.RunAsync(hostNames);
        int? lineCount = result.StdOutput?.Split(Environment.NewLine).Length;
        Assert.AreEqual(expectedLineCount, lineCount);
    }

    [TestMethod]
#pragma warning disable CS1998 // Remove this
    async public Task RunLongRunningAsync_UsingTpl_Success()
    {
        PingResult result = default;
        // Test Sut.RunLongRunningAsync("localhost");
        AssertValidPingOutput(result);
    }
#pragma warning restore CS1998 // Remove this

    [TestMethod]
    public void StringBuilderAppendLine_InParallel_IsNotThreadSafe()
    {
        IEnumerable<int> numbers = Enumerable.Range(0, short.MaxValue);
        System.Text.StringBuilder stringBuilder = new();
        numbers.AsParallel().ForAll(item => stringBuilder.AppendLine(""));
        int lineCount = stringBuilder.ToString().Split(Environment.NewLine).Length;
        Assert.AreNotEqual(lineCount, numbers.Count()+1);
    }
    //4
    [TestMethod]
    async public Task RunAsync_ReturnsCorrectResult()
    {
        // Arrange
        var cancellationToken = CancellationToken.None;
        var hostNamesOrAddresses = new List<string> { "localhost", "localhost", "localhost", "localhost" };
        var expectedTotal = hostNamesOrAddresses.Count;

        //Act
        PingResult result = await Sut.RunAsync(hostNamesOrAddresses, cancellationToken);

        //Assert
        Assert.AreEqual(expectedTotal, result.StdOutput?.Length);

    }

    //5
    [TestMethod]
    public async void RunLongRunningAsync_StratProcess()
    {
        //Arrange
        var cancellationToken = new CancellationTokenSource(); ;
        var startInfo = new ProcessStartInfo();
        var pingProcess = new PingProcess();
        

        var output = new Action<string?>((output) => Console.WriteLine($"Output: {output}"));
        var error = new Action<string?>((error) => Console.WriteLine($"Error: {error}"));


        //Act
        var resultTask = Sut.RunLongRunningAsync(startInfo, output, error, cancellationToken.Token);

        var result = await resultTask;

        //Assert
        Assert.Equals(1, result);
    }

    readonly string PingOutputLikeExpression = @"
Pinging * with 32 bytes of data:
Reply from ::1: time<*
Reply from ::1: time<*
Reply from ::1: time<*
Reply from ::1: time<*

Ping statistics for ::1:
    Packets: Sent = *, Received = *, Lost = 0 (0% loss),
Approximate round trip times in milli-seconds:
    Minimum = *, Maximum = *, Average = *".Trim();
    private void AssertValidPingOutput(int exitCode, string? stdOutput)
    {
        Assert.IsFalse(string.IsNullOrWhiteSpace(stdOutput));
        stdOutput = WildcardPattern.NormalizeLineEndings(stdOutput!.Trim());
        Assert.IsTrue(stdOutput?.IsLike(PingOutputLikeExpression)??false,
            $"Output is unexpected: {stdOutput}");
        Assert.AreEqual<int>(0, exitCode);
    }
    private void AssertValidPingOutput(PingResult result) =>
        AssertValidPingOutput(result.ExitCode, result.StdOutput);
}