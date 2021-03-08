using System;
using IncendianFalls;

namespace ConsoleDriveyThing {
  public class MainReplay {
    public static void Replay(string logFile) {
      Superstructure ss = new Superstructure(new ConsoleLoggers.ConsoleLogger());
      var replayer = new ReplayReader(ss, logFile);

      var sw = new System.Diagnostics.Stopwatch();
      sw.Start();

      bool sanityChecking = false;

      while (replayer.ReadAndReplayNext()) {
        if (sanityChecking) {
          ss.SanityCheck();
        }
      }

      sw.Stop();
      Console.WriteLine("Done!");
      if (!sanityChecking) {
        Console.WriteLine("DID NOT SANITY CHECK");
        Console.WriteLine("DID NOT SANITY CHECK");
        Console.WriteLine("DID NOT SANITY CHECK");
        Console.WriteLine("DID NOT SANITY CHECK");
        Console.WriteLine("Total time to replay: " + sw.Elapsed.TotalMilliseconds);
      }
    }
  }
}
