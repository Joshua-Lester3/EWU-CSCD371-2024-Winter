using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Net.NetworkInformation;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Assignment;

public record struct PingResult(int ExitCode, string? StdOutput);

public class PingProcess
{
    private ProcessStartInfo StartInfo { get; } = new("ping");

    public PingResult Run(string hostNameOrAddress)
    {
        StartInfo.Arguments = hostNameOrAddress;
        StringBuilder? stringBuilder = null;
        void updateStdOutput(string? line) =>
            (stringBuilder ??= new StringBuilder()).AppendLine(line);
        Process process = RunProcessInternal(StartInfo, updateStdOutput, default, default);
        return new PingResult(process.ExitCode, stringBuilder?.ToString());
    }

    public Task<PingResult> RunTaskAsync(string hostNameOrAddress)
    {
        StartInfo.Arguments = hostNameOrAddress;
        StringBuilder? stringBuilder = null;
        void updateStdOutput(string? line) =>
            (stringBuilder ??= new StringBuilder()).AppendLine(line);
        Task<PingResult> task = Task.Run<PingResult>(() =>
        {
            Process process = RunProcessInternal(StartInfo, updateStdOutput, default, default);
            return new PingResult(process.ExitCode, stringBuilder?.ToString());
        });
        return task;
    }

    async public Task<PingResult> RunAsync(
        string hostNameOrAddress, CancellationToken cancellationToken = default)
    {
        StartInfo.Arguments = hostNameOrAddress;
        StringBuilder? stringBuilder = null;
        void updateStdOutput(string? line) =>
            (stringBuilder ??= new StringBuilder()).AppendLine(line);
        cancellationToken.ThrowIfCancellationRequested();
        Task<Process> task = Task.Run(() =>
        {
            return RunProcessInternal(StartInfo, updateStdOutput, default, default);
        }, cancellationToken);
        Process process = await task;
        return new PingResult(process.ExitCode, stringBuilder?.ToString());
    }
    //4
    /*async public Task<PingResult> RunAsync(params string[] hostNameOrAddresses)
    {
        StringBuilder? stringBuilder = null;
        ParallelQuery<Task<int>>? all = hostNameOrAddresses.AsParallel().Select(async item =>
        {
            Task<PingResult> task = null!;
            // ...

            await task.WaitAsync(default(CancellationToken));
            return task.Result.ExitCode;
        });

        await Task.WhenAll(all);
        int total = all.Aggregate(0, (total, item) => total + item.Result);
        return new PingResult(total, stringBuilder?.ToString());
    }*/
    //4
    async public Task<PingResult> RunAsync(IEnumerable<string> hostNameOrAddresses, CancellationToken cancellationToken = default)
    {
        ParallelQuery<Task<PingResult>> allResults = hostNameOrAddresses.AsParallel().WithCancellation(cancellationToken).Select(async item =>
        {
            return await RunAsync(item);
        });

        IEnumerable<PingResult> results = await Task.WhenAll(allResults);
        int total = results.Aggregate(0, (total, item) => total + item.ExitCode);
        StringBuilder stringBuilder = new StringBuilder();
        foreach (PingResult result in results)
        {
            stringBuilder.Append(result.StdOutput);
        }
        return new PingResult(total, stringBuilder?.ToString());
    }
    //5
    /*public async Task<PingResult> RunLongRunningAsync(string hostNameOrAddress, CancellationToken token = default)
    {
        StartInfo.Arguments = hostNameOrAddress;
        StringBuilder? stringBuilder = null;
        //should have left this as updateStdOuput..¯\_(ツ)_/¯
        void taskCreation(string? line) =>
            (stringBuilder ??= new StringBuilder()).AppendLine(line);
        void taskSchedular(string? line) =>
            (stringBuilder ??= new StringBuilder()).AppendLine(line);
        Task<Process> task = Task.Factory.StartNew(() =>
        {
            
            return RunProcessInternal(StartInfo,taskCreation ,taskSchedular, default);
        }, token, TaskCreationOptions.LongRunning, TaskScheduler.Current);
        Process process = await task;
        return new PingResult(process.ExitCode, stringBuilder?.ToString());
    }*/
    //5 from the steps
    public async Task<int> RunLongRunningAsync(ProcessStartInfo startInfo, Action<string?>? progressOutput,
       Action<string?>? progressError, CancellationToken token)
    {
        return await Task.Factory.StartNew(() => 
            RunProcessInternal(startInfo, progressOutput, progressError, token).ExitCode,
            token, TaskCreationOptions.LongRunning, TaskScheduler.Current);

    }

    private Process RunProcessInternal(
        ProcessStartInfo startInfo,
        Action<string?>? progressOutput,
        Action<string?>? progressError,
        CancellationToken token)
    {
        var process = new Process
        {
            StartInfo = UpdateProcessStartInfo(startInfo)
        };
        return RunProcessInternal(process, progressOutput, progressError, token);
    }

    private Process RunProcessInternal(
        Process process,
        Action<string?>? progressOutput,
        Action<string?>? progressError,
        CancellationToken token)
    {
        process.EnableRaisingEvents = true;
        process.OutputDataReceived += OutputHandler;
        process.ErrorDataReceived += ErrorHandler;

        try
        {
            if (!process.Start())
            {
                return process;
            }

            token.Register(obj =>
            {
                if (obj is Process p && !p.HasExited)
                {
                    try
                    {
                        p.Kill();
                    }
                    catch (Win32Exception ex)
                    {
                        throw new InvalidOperationException($"Error cancelling process{Environment.NewLine}{ex}");
                    }
                }
            }, process);


            if (process.StartInfo.RedirectStandardOutput)
            {
                process.BeginOutputReadLine();
            }
            if (process.StartInfo.RedirectStandardError)
            {
                process.BeginErrorReadLine();
            }

            if (process.HasExited)
            {
                return process;
            }
            process.WaitForExit();
        }
        catch (Exception e)
        {
            throw new InvalidOperationException($"Error running '{process.StartInfo.FileName} {process.StartInfo.Arguments}'{Environment.NewLine}{e}");
        }
        finally
        {
            if (process.StartInfo.RedirectStandardError)
            {
                process.CancelErrorRead();
            }
            if (process.StartInfo.RedirectStandardOutput)
            {
                process.CancelOutputRead();
            }
            process.OutputDataReceived -= OutputHandler;
            process.ErrorDataReceived -= ErrorHandler;

            if (!process.HasExited)
            {
                process.Kill();
            }

        }
        return process;

        void OutputHandler(object s, DataReceivedEventArgs e)
        {
            progressOutput?.Invoke(e.Data);
        }

        void ErrorHandler(object s, DataReceivedEventArgs e)
        {
            progressError?.Invoke(e.Data);
        }
    }

    private static ProcessStartInfo UpdateProcessStartInfo(ProcessStartInfo startInfo)
    {
        startInfo.CreateNoWindow = true;
        startInfo.RedirectStandardError = true;
        startInfo.RedirectStandardOutput = true;
        startInfo.UseShellExecute = false;
        startInfo.WindowStyle = ProcessWindowStyle.Hidden;

        return startInfo;
    }
}