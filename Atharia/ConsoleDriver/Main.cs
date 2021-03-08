using System;
using IncendianFalls;

namespace ConsoleDriveyThing {
  public class Program {
    static void Main(string[] args) {
      Console.WriteLine("Enter log file to replay, a number to do random plays, or hit enter to start new game: ");
      string logFile = Console.ReadLine();
      if (logFile.Trim().Length == 0) {
        MainPlay.Play();
        Console.WriteLine("Replaying!");
        MainReplay.Replay("Latest.sslog");
      } else if (int.TryParse(logFile, out int number)) {
        for (int i = 0; i < number; i++) {
          RandomPlayer.Play();
        }
      } else {
        MainReplay.Replay(logFile);
      }
    }

  }
}
