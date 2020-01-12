using System;

namespace GA.Lib.OutputWriters
{
  public class ConsoleOutputWriter : IOutputWriter
  {
    public bool DebugMode { get; private set;}

    public ConsoleOutputWriter(bool debugMode = false)
    {
      this.DebugMode = debugMode;
    }

    public void WriteLine(string message)
    {
      Console.WriteLine(message);
    }

    public void Debug(string message)
    {
      if (this.DebugMode)
      {
        var originalConsoleColor = Console.ForegroundColor;

        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine($"DEBUG: {message}");

        Console.ForegroundColor = originalConsoleColor;
      }
    }
  }
}
