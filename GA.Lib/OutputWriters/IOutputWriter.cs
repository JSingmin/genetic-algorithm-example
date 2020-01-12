using System;

namespace GA.Lib.OutputWriters
{
  public interface IOutputWriter
  {
    void WriteLine(string message);

    void Debug (string message);
  }
}
