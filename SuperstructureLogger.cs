using System;
using System.Collections.Generic;
using System.IO;
using Atharia.Model;

namespace IncendianFalls {
  public class NullLogger : ILogger {
    public void Error(string str) { }
    public void Info(string str) { }
    public void Warning(string str) { }
  }

  public class ReplayLogger : ISuperstructureObserver {
    private Superstructure externalSS;
    //private Superstructure duplicateSS;
    StreamWriter[] writers;

    public ReplayLogger(Superstructure externalSS, string[] logFilenames) {
      this.externalSS = externalSS;
      externalSS.AddObserver(this);
      writers = new StreamWriter[logFilenames.Length];
      for (int i = 0; i < logFilenames.Length; i++) {
        writers[i] = new StreamWriter(logFilenames[i], false);
      }
    }

    public void AfterRequest(IRequest request) { }

    public void BeforeRequest(IRequest request) {
      string str = request.DStr();
      Asserts.Assert(str.Length > 0);
      foreach (var writer in writers) {
        writer.Write(str + "\n");
        writer.Flush();
      }
    }

    public void OnFlare(string message) {
      foreach (var writer in writers) {
        writer.Write(message + "\n");
        writer.Flush();
      }
    }
  }

  public class ReplayReader : ISuperstructureObserver {
    Superstructure ss;
    StreamReader reader;
    Replayer replayer;

    public ReplayReader(Superstructure ss, string logFilename) {
      this.ss = ss;
      reader = new StreamReader(logFilename);
      replayer = new Replayer(ss);

      ss.AddObserver(this);
    }

    public void AfterRequest(IRequest request) { }

    public void BeforeRequest(IRequest request) { }

    public void OnFlare(string inMemoryFlareMessage) {
      // We've received a flare from the destination superstructure. Let's make sure
      // that it appears in the source file.

      string logFlareMessage = reader.ReadLine();

      if (inMemoryFlareMessage == logFlareMessage) {
        //Console.WriteLine("Agreeing flares: " + logFlareMessage + " == " + inMemoryFlareMessage);
      } else {
        throw new Exception("Disagreeing flares! In memory flared " + inMemoryFlareMessage + " but log expected " + logFlareMessage);
      }
    }

    public bool ReadAndReplayNext() {
      string line = reader.ReadLine();
      if (line == null) {
        Console.WriteLine("Reached end!");
        return false;
      }
      //Console.WriteLine("Read: " + line);
      ParseSource source = new ParseSource(line);
      var request = IRequestParser.Parse(source);

      Console.WriteLine("Parsed: " + request.DStr());
      Asserts.Assert(line.Trim() == request.DStr().Trim());

      // This will cause some flares
      replayer.Replay(request);

      return true;
    }
  }

  public class Replayer {
    private Superstructure ss;
    SortedDictionary<int, RootIncarnation> snapshotByVersion;

    public Replayer(Superstructure ss) {
      this.ss = ss;
      snapshotByVersion = new SortedDictionary<int, RootIncarnation>();
    }
    public void Replay(IRequest request) {
      if (request is SetupGameRequestAsIRequest setupGame) {
        ss.RequestSetupGame(setupGame.obj.randomSeed, setupGame.obj.squareLevelsOnly);
      } else if (request is InteractRequestAsIRequest interact) {
        ss.RequestInteract(interact.obj.gameId);
      } else if (request is MoveRequestAsIRequest move) {
        ss.RequestMove(move.obj.gameId, move.obj.destination);
      } else if (request is AttackRequestAsIRequest attack) {
        ss.RequestAttack(attack.obj.gameId, attack.obj.targetUnitId);
      } else if (request is TimeShiftRequestAsIRequest timeShift) {
        ss.RequestTimeShift(
            timeShift.obj.gameId,
            snapshotByVersion[timeShift.obj.version],
            timeShift.obj.futuremostTime);
      } else if (request is DefendRequestAsIRequest defend) {
        ss.RequestDefend(defend.obj.gameId);
      } else if (request is ResumeRequestAsIRequest resume) {
        ss.RequestResume(resume.obj.gameId);
      } else if (request is FollowDirectiveRequestAsIRequest follow) {
        ss.RequestFollowDirective(follow.obj.gameId);
      } else if (request is SnapshotRequestAsIRequest snap) {
        var snapshot = ss.Snapshot();
        snapshotByVersion.Add(snapshot.version, snapshot);
      } else {
        Asserts.Assert(false);
      }
    }
  }
}

