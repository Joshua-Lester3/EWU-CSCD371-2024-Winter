using IntelliTect.TestTools;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.ExceptionServices;
using System.Threading;
using System.Text;
using System.Threading.Tasks;
using System.Net.NetworkInformation;
using System.Net.Sockets;

namespace Assignment.Tests;

[TestClass]
public class PingProcessTests
{
    PingProcess Sut { get; set; } = new();

    // PingParameter will never be null because it is set in TestInitialize
#pragma warning disable CS8618
    string PingParameter { get; set; }
    bool IsUnix { get; set; }
#pragma warning restore CS8618

    [TestInitialize]
    public void TestInitialize()
    {
        Sut = new();
        IsUnix = Environment.OSVersion.Platform is PlatformID.Unix;
        PingParameter = IsUnix ? "-c" : "-n";
    }

    [TestMethod]
    public void Start_PingProcess_Success()
    {
        Process process = Process.Start("ping", $"{PingParameter} 4 localhost");
        process.WaitForExit();
        Assert.AreEqual<int>(0, process.ExitCode);
    }

    [TestMethod]
    public void Run_GoogleDotCom_Success()
    {
        int expectedExitCode = Environment.GetEnvironmentVariable("GITHUB_ACTIONS") is null ? 0 : 1;
        int exitCode = Sut.Run($"{PingParameter} 4 google.com").ExitCode;
        Assert.AreEqual<int>(expectedExitCode, exitCode);
    }


    [TestMethod]
    public void Run_InvalidAddressOutput_Success()
    {
        (int exitCode, string? stdOutput, string? stdError) = Sut.Run("badaddress");
        string? output = IsUnix ? stdError : stdOutput;
        Assert.IsFalse(string.IsNullOrWhiteSpace(output));
        stdOutput = WildcardPattern.NormalizeLineEndings(output!.Trim());
        string expectedOutput = IsUnix ? "ping: badaddress: Temporary failure in name resolution" :
            "Ping request could not find host badaddress. Please check the name and try again.";
        Assert.AreEqual<string?>(expectedOutput, output,
            $"Output is unexpected: {output}");
        int expectedExitCode = IsUnix ? 2 : 1;
        Assert.AreEqual<int>(expectedExitCode, exitCode);
    }

    [TestMethod]
    public void Run_CaptureStdOutput_Success()
    {
        PingResult result = Sut.Run($"{PingParameter} 4 localhost");
        AssertValidPingOutput(result);
    }

    [TestMethod]
    public void RunTaskAsync_Success()
    {
        // Arrange

        // Act
        PingResult result = Sut.RunTaskAsync($"{PingParameter} 4 localhost").Result;

        // Assert
        AssertValidPingOutput(result);
    }

    [TestMethod]
    public void RunAsync_UsingTaskReturn_Success()
    {
        // Arrange

        // Act
        Task<PingResult> task = Sut.RunAsync($"{PingParameter} 4 localhost");
        PingResult result = task.Result;

        // Assert
        AssertValidPingOutput(result);
    }

    [TestMethod]
    async public Task RunAsync_UsingTpl_Success()
    {
        // Arrange

        // Act
        PingResult result = await Sut.RunAsync($"{PingParameter} 4 localhost");

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
        Task<PingResult> task = Sut.RunAsync($"{PingParameter} 4 localhost", cancellationTokenSource.Token);

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
        Task<PingResult> task = Sut.RunAsync($"{PingParameter} 4 localhost", cancellationTokenSource.Token);

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
        var startInfo = new ProcessStartInfo("ping", $"{PingParameter} 4 localhost");
        StringBuilder stringBuilderOutput = new();
        StringBuilder stringBuilderError = new();
        var outputAction = new Action<string?>(input => stringBuilderOutput.AppendLine(input));
        var errorAction = new Action<string?>(input => stringBuilderError.AppendLine(input));
        
        // Act
        int exitCode = await Sut.RunLongRunningAsync(startInfo,outputAction, errorAction, default);

        // Assert
        AssertValidPingOutput(exitCode, stringBuilderOutput.ToString());
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
        
        var hostNamesOrAddresses = new List<string> { $"{PingParameter} 4 localhost", $"{PingParameter} 4 localhost", $"{PingParameter} 4 localhost" };

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
        var hostNamesOrAddresses = new List<string> { $"{PingParameter} 4 localhost", $"{PingParameter} 4 localhost", $"{PingParameter} 4 localhost", $"{PingParameter} 4 localhost" };

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

    // Windows version:
    readonly string WindowsPingOutputLikeExpression = @"
Pinging * with 32 bytes of data:
Reply from ::1: time<*
Reply from ::1: time<*
Reply from ::1: time<*
Reply from ::1: time<*

Ping statistics for ::1:
    Packets: Sent = *, Received = *, Lost = 0 (0% loss),
Approximate round trip times in milli-seconds:
    Minimum = *ms, Maximum = *ms, Average = *ms
".Trim();

    // Unix version:
    readonly string UnixPingOutputLikeExpression = @"
PING * * bytes*
64 bytes from * (*): icmp_seq=* ttl=* time=* ms
64 bytes from * (*): icmp_seq=* ttl=* time=* ms
64 bytes from * (*): icmp_seq=* ttl=* time=* ms
64 bytes from * (*): icmp_seq=* ttl=* time=* ms

--- * ping statistics ---
* packets transmitted, * received, *% packet loss, time *ms
rtt min/avg/max/mdev = */*/*/* ms
".Trim();

    string test = @"
PING localhost (127.0.0.1) 56(84) bytes of data.
64 bytes from localhost (127.0.0.1): icmp_seq=1 ttl=64 time=2.17 ms
64 bytes from localhost (127.0.0.1): icmp_seq=2 ttl=64 time=0.262 ms
64 bytes from localhost (127.0.0.1): icmp_seq=3 ttl=64 time=0.087 ms
64 bytes from localhost (127.0.0.1): icmp_seq=4 ttl=64 time=0.049 ms

--- localhost ping statistics ---
4 packets transmitted, 4 received, 0% packet loss, time 3088ms
rtt min/avg/max/mdev = 0.049/0.641/2.166/0.884 ms
".Trim();

    [TestMethod]
    public void Test()
    {
        AssertValidPingOutput(0, test);
    }
    private void AssertValidPingOutput(int exitCode, string? stdOutput)
    {
        Assert.IsFalse(string.IsNullOrWhiteSpace(stdOutput));
        stdOutput = WildcardPattern.NormalizeLineEndings(stdOutput!.Trim());
        string pingOutputLikeExpression = IsUnix ? UnixPingOutputLikeExpression : WindowsPingOutputLikeExpression;
        pingOutputLikeExpression = WildcardPattern.NormalizeLineEndings(pingOutputLikeExpression);
        Assert.IsTrue(stdOutput?.IsLike(pingOutputLikeExpression)??false,
            $"Output is unexpected: {stdOutput}");
        Assert.AreEqual<int>(0, exitCode);
    }
    private void AssertValidPingOutput(PingResult result) =>
        AssertValidPingOutput(result.ExitCode, result.StdOutput);
}