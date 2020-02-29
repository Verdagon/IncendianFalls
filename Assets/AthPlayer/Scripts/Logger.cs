using System;
using Atharia.Model;
using AthPlayer;

namespace AthPlayer {
  public class LoggerImpl : ILogger {
    public LoggerImpl() {
    }
    public void Info(String message) {
      UnityEngine.Debug.Log(message);
    }
    public void Warning(String message) {
      UnityEngine.Debug.LogWarning(message);
    }
    public void Error(String message) {
      UnityEngine.Debug.LogError(message);
    }
  }
  public class Logger {
    public static void Info(String message) {
      UnityEngine.Debug.Log(message);
    }
    public static void Warning(String message) {
      UnityEngine.Debug.LogWarning(message);
    }
    public static void Error(String message) {
      UnityEngine.Debug.LogError(message);
    }
  }
}