using BenchmarkDotNet.Loggers;
using System;
using System.IO;

public class FileLogger : ILogger
{
    private readonly string filePath;

    public FileLogger(string directory)
    {
        try
        {
            Directory.CreateDirectory(directory);
            filePath = Path.Combine(directory, $"{DateTime.Now:yyyy-MM-dd_HH-mm-ss}.log");
            Console.WriteLine($"Logging to: {filePath}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error initializing FileLogger: {ex.Message}");
        }
    }

    public string Id => nameof(FileLogger);
    public int Priority => 0;

    public void Write(LogKind logKind, string text)
    {
        try
        {
            File.AppendAllText(filePath, text);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error writing log: {ex.Message}");
        }
    }

    public void WriteLine()
    {
        try
        {
            File.AppendAllText(filePath, Environment.NewLine);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error writing log: {ex.Message}");
        }
    }

    public void WriteLine(LogKind logKind, string text)
    {
        try
        {
            File.AppendAllText(filePath, text + Environment.NewLine);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error writing log: {ex.Message}");
        }
    }

    public void Flush() { }
}
