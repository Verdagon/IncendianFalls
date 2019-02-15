using System;

namespace Atharia.Model {
  public interface ILogger {
    void Info(string str);
    void Warning(string str);
    void Error(string str);
  }
}
