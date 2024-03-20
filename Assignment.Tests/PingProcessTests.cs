using IntelliTect.TestTools;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Threading;
using System.Text;
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
        Process process = Process.Start("ping", "-c 4 localhost");
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
    public async Task RunLongRunningAsync_UsingTpl_Success()
    {
        // Arrange
        var startInfo = new ProcessStartInfo("ping", "localhost");
        StringBuilder stringBuilder = new();
        var outputAction = new Action<string?>(input => stringBuilder.AppendLine(input));
        var errorAction = new Action<string?>(input => Console.WriteLine($"Error: {input}"));
        
        // Act
        int exitCode = await Sut.RunLongRunningAsync(startInfo,outputAction, errorAction, default);

        // Assert
        AssertValidPingOutput(exitCode, stringBuilder.ToString());
    }

    // Not sure why this method is here so I'm commenting it out for the time being
    //[TestMethod]
    //public void StringBuilderAppendLine_InParallel_IsNotThreadSafe()
    //{
    //    IEnumerable<int> numbers = Enumerable.Range(0, short.MaxValue);
    //    StringBuilder stringBuilder = new();
    //    numbers.AsParallel().ForAll(item => stringBuilder.AppendLine(""));
    //    int lineCount = stringBuilder.ToString().Split(Environment.NewLine).Length;
    //    Assert.AreNotEqual(lineCount, numbers.Count()+1);
    //}
    //4
    [TestMethod]
     public async Task RunAsync_ReturnsCorrectResult()
    {
        // Arrange
        CancellationToken cancellationToken = default;
        
        var hostNamesOrAddresses = new List<string> { "localhost", "google.com", "intellitect.com" };

        //Act
        PingResult result = await Sut.RunAsync(hostNamesOrAddresses, cancellationToken);

        //Assert
        AssertValidPingOutput(result);

    }
    //4
    [TestMethod]
    [ExpectedException(typeof(TaskCanceledException))]
    public void RunAsync_ParallelQuery_WithCancellation()
    {
        // Arrange
        CancellationTokenSource cancellationTokenSource = new();
        cancellationTokenSource.Cancel();
        var hostNamesOrAddresses = new List<string> { "localhost", "localhost", "localhost", "localhost" };

        //Act
        Task<PingResult> result = Sut.RunAsync(hostNamesOrAddresses, cancellationTokenSource.Token);

        //Assert
        try
        {
            result.Wait();
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

//    readonly string PingOutputLikeExpression = @"
//Pinging * with 32 bytes of data:
//Reply from ::1: time<*
//Reply from ::1: time<*
//Reply from ::1: time<*
//Reply from ::1: time<*

//Ping statistics for ::1:
//    Packets: Sent = *, Received = *, Lost = 0 (0% loss),
//Approximate round trip times in milli-seconds:
//    Minimum = *, Maximum = *, Average = *".Trim();

    readonly string PingOutputLikeExpression = @"PING * 56 data bytes
64 bytes from localhost (::1): icmp_seq=1 ttl=64 time=* ms
64 bytes from localhost (::1): icmp_seq=2 ttl=64 time=* ms
64 bytes from localhost (::1): icmp_seq=3 ttl=64 time=* ms
64 bytes from localhost (::1): icmp_seq=4 ttl=64 time=* ms
--- localhost ping statistics ---
4 packets transmitted, 4 received, 0% packet loss, time *ms
rtt min/avg/max/mdev = */*/*/* ms".Trim();
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