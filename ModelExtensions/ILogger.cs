using System;

namespace Atharia.Model {
  public interface ILogger {
    void Info(string str);
    void Warning(string str);
    void Error(string str);
  }

  public static class Logger {
    private static ILogger logger = null;

    public static void SetLogger(ILogger newLogger) {
      Asserts.Assert(logger == null);
      logger = newLogger;
    }

    public static void Info(string str) {
      logger.Info(str);
    }
    public static void Warning(string str) {
      logger.Warning(str);
    }
    public static void Error(string str) {
      logger.Error(str);
    }
  }
}
