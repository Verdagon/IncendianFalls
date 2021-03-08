using System;
using Geomancer.Model;

namespace Geomancer {
  public class ConsoleLoggers {
    public class ConsoleLogger : ILogger {
      public void Error(string str) {
        Console.WriteLine(str);
      }
      public void Info(string str) {
        Console.WriteLine(str);
      }
      public void Warning(string str) {
        Console.WriteLine(str);
      }
    }

  }
}
