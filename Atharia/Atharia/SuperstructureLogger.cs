using System;
using System.Collections.Generic;
using System.IO;
using Atharia.Model;

namespace IncendianFalls {
  public class NullLogger : ILogger {
    public void Error(string str) { }
    public void Info(string str) { }
    public void Warning(string str) { }
    public void Flare(string str) { }
  }

  public class ReplayLogger : ISuperstructureObserver, IDisposable {
    private Superstructure externalSS;
    //private Superstructure duplicateSS;
    List<StreamWriter> writers;

    public ReplayLogger(Superstructure externalSS, string[] logFilenames) {
      this.externalSS = externalSS;
      externalSS.AddObserver(this);
      writers = new List<StreamWriter>();
      for (int i = 0; i < logFilenames.Length; i++) {
        try {
          writers.Add(new StreamWriter(logFilenames[i], false));
        } catch (UnauthorizedAccessException e) {
          externalSS.GetRoot().logger.Error(
              "Couldn't make a log file: " + e.Message);
        }
      }
    }

    public void Dispose() {
      foreach (var writer in writers) {
        writer.Dispose();
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

    public Replayer(Superstructure ss) {
      this.ss = ss;
    }
    public void Replay(IRequest request) {
      if (request is SetupIncendianFallsGameRequestAsIRequest setupIncendianFallsGame) {
        ss.RequestSetupIncendianFallsGame(setupIncendianFallsGame.obj.randomSeed, setupIncendianFallsGame.obj.squareLevelsOnly);
      } else if (request is SetupGauntletGameRequestAsIRequest setupGauntletGame) {
        ss.RequestSetupGauntletGame(setupGauntletGame.obj.randomSeed, setupGauntletGame.obj.squareLevelsOnly);
      } else if (request is SetupRavaArcanaGameRequestAsIRequest setupRavaArcanaGame) {
        ss.RequestSetupRavaArcanaGame(setupRavaArcanaGame.obj.randomSeed, setupRavaArcanaGame.obj.startLevel, setupRavaArcanaGame.obj.squareLevelsOnly);
      } else if (request is SetupEmberDeepGameRequestAsIRequest setupEmberDeepGame) {
        ss.RequestSetupEmberDeepGame(setupEmberDeepGame.obj.randomSeed, setupEmberDeepGame.obj.startLevel, setupEmberDeepGame.obj.squareLevelsOnly);
      } else if (request is InteractRequestAsIRequest interact) {
        ss.RequestInteract(interact.obj.gameId);
      } else if (request is MoveRequestAsIRequest move) {
        ss.RequestMove(move.obj.gameId, move.obj.destination);
      } else if (request is AttackRequestAsIRequest attack) {
        ss.RequestAttack(attack.obj.gameId, attack.obj.targetUnitId);
      } else if (request is FireRequestAsIRequest fire) {
        ss.RequestFire(fire.obj.gameId, fire.obj.targetUnitId);
      } else if (request is MireRequestAsIRequest mire) {
        ss.RequestMire(mire.obj.gameId, mire.obj.targetUnitId);
      } else if (request is TimeShiftRequestAsIRequest timeShift) {
        ss.RequestTimeShift(timeShift.obj.gameId);
      } else if (request is CounterRequestAsIRequest counter) {
        ss.RequestCounter(counter.obj.gameId);
      } else if (request is DefyRequestAsIRequest defend) {
        ss.RequestDefy(defend.obj.gameId);
      //} else if (request is FollowDirectiveRequestAsIRequest follow) {
      //  ss.RequestFollowDirective(follow.obj.gameId);
      } else if (request is TimeAnchorMoveRequestAsIRequest tamrI) {
        ss.RequestTimeAnchorMove(tamrI.obj.gameId, tamrI.obj.destination);
      } else if (request is CheatRequestAsIRequest crI) {
        ss.RequestCheat(crI.obj.gameId, crI.obj.cheatName);
      } else if (request is TimeShiftRequestAsIRequest tsrI) {
        ss.RequestTimeShift(tsrI.obj.gameId);
      } else if (request is CommActionRequestAsIRequest trI) {
        ss.RequestCommAction(trI.obj.gameId, trI.obj.commId, trI.obj.actionIndex);
      } else if (request is FireBombRequestAsIRequest fbI) {
        ss.RequestFireBomb(fbI.obj.gameId, fbI.obj.location);
      } else if (request is ResumeRequestAsIRequest rrI) {
        ss.RequestResume(rrI.obj.gameId);
      } else {
        Asserts.Assert(false);
      }
    }
  }
}

