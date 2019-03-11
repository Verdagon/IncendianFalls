using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Atharia.Model;

namespace IncendianFalls {
  public interface ISuperstructureObserver {
    void BeforeRequest(IRequest request);
    void AfterRequest(IRequest request);
    void OnFlare(string message);
  }

  public class SSContext {
    public readonly ILogger logger;
    public readonly Root root;
    private readonly List<ISuperstructureObserver> observers;

    public SSContext(
        ILogger logger,
        Root root,
        List<ISuperstructureObserver> observers) {
      this.logger = logger;
      this.root = root;
      this.observers = observers;
    }

    public void Flare(
        string message,
        [CallerFilePath] string file = "",
        [CallerLineNumber] int lineNumber = 0) {
      broadcastFlare(file + ":" + lineNumber + ": " + message);
    }

    public void Flare(
        int message,
        [CallerFilePath] string file = "",
        [CallerLineNumber] int lineNumber = 0) {
      broadcastFlare(file + ":" + lineNumber + ": " + message);
    }

    private void broadcastFlare(string message) {
      root.Lock();
      foreach (var observer in observers) {
        observer.OnFlare(message);
      }
      root.Unlock();
    }
  }

  public class Superstructure {
    private readonly Root root;
    private readonly List<ISuperstructureObserver> observers;
    SSContext context;
    SortedDictionary<int, Superstate> superstateByGameId;

    public Superstructure(ILogger logger) {
      observers = new List<ISuperstructureObserver>();
      root = new Root(logger);
      context = new SSContext(logger, root, observers);
      superstateByGameId = new SortedDictionary<int, Superstate>();
    }

    public void AddObserver(ISuperstructureObserver observer) {
      observers.Add(observer);
    }

    private void broadcastBeforeRequest(IRequest request) {
      root.Lock();
      foreach (var observer in observers) {
        observer.BeforeRequest(request);
      }
      root.Unlock();
    }

    private void broadcastAfterRequest(IRequest request) {
      root.Lock();
      foreach (var observer in observers) {
        observer.AfterRequest(request);
      }
      root.Unlock();
    }

    public Root GetRoot() {
      return root;
    }

    public Superstate GetSuperstate(Game game) {
      return superstateByGameId[game.id];
    }

    public Game RequestSetupGame(int randomSeed, bool squareLevelsOnly, bool gauntletMode) {
      var stopwatch = new System.Diagnostics.Stopwatch();
      stopwatch.Start();

      root.Unlock();
      //var rollbackPoint = root.Snapshot();
      try {
        var request = new SetupGameRequest(randomSeed, squareLevelsOnly, gauntletMode);
        broadcastBeforeRequest(new SetupGameRequestAsIRequest(request));
        context.Flare(GetDeterministicHashCode());
        Superstate superstate = null;
        var game = SetupGameRequestExecutor.Execute(context, out superstate, request);

        superstateByGameId.Add(game.id, superstate);

        // context.Flare(game.DStr());
        broadcastAfterRequest(new SetupGameRequestAsIRequest(request));
        context.Flare(GetDeterministicHashCode());
        return game;
      //} catch (Exception) {
      //  Logger.Error("Caught exception, rolling back!");
      //  root.Revert(rollbackPoint);
      //  throw;
      } finally {
        root.Lock();
        root.FlushEvents();

        stopwatch.Stop();
        Console.WriteLine("RequestSetupGame RunTime " + stopwatch.Elapsed.TotalMilliseconds);
      }
    }

    public string RequestInteract(int gameId) {
      var stopwatch = new System.Diagnostics.Stopwatch();
      stopwatch.Start();

      root.Unlock();
      //var rollbackPoint = root.Snapshot();
      try {
        var request = new InteractRequest(gameId);
        broadcastBeforeRequest(new InteractRequestAsIRequest(request));
        context.Flare(GetDeterministicHashCode());
        var superstate = superstateByGameId[gameId];
        var success = InteractRequestExecutor.Execute(context, superstate, request);
        context.Flare(success.DStr());
        broadcastAfterRequest(new InteractRequestAsIRequest(request));
        context.Flare(GetDeterministicHashCode());
        return success;
      //} catch (Exception) {
      //  Logger.Error("Caught exception, rolling back!");
      //  root.Revert(rollbackPoint);
      //  throw;
      } finally {
        root.Lock();
        root.FlushEvents();

        stopwatch.Stop();
        Console.WriteLine("RequestInteract RunTime " + stopwatch.Elapsed.TotalMilliseconds);
      }
    }

    public string RequestCheat(int gameId, string cheatName) {
      var stopwatch = new System.Diagnostics.Stopwatch();
      stopwatch.Start();

      root.Unlock();
      //var rollbackPoint = root.Snapshot();
      try {
        var request = new CheatRequest(gameId, cheatName);
        broadcastBeforeRequest(new CheatRequestAsIRequest(request));
        context.Flare(GetDeterministicHashCode());
        var superstate = superstateByGameId[gameId];
        var success = CheatRequestExecutor.Execute(context, superstate, request);
        context.Flare(success.DStr());
        broadcastAfterRequest(new CheatRequestAsIRequest(request));
        context.Flare(GetDeterministicHashCode());
        return success;
        //} catch (Exception) {
        //  Logger.Error("Caught exception, rolling back!");
        //  root.Revert(rollbackPoint);
        //  throw;
      } finally {
        root.Lock();
        root.FlushEvents();

        stopwatch.Stop();
        Console.WriteLine("RequestCheat RunTime " + stopwatch.Elapsed.TotalMilliseconds);
      }
    }

    public string RequestMove(int gameId, Location newLocation) {
      var stopwatch = new System.Diagnostics.Stopwatch();
      stopwatch.Start();

      root.Unlock();
      //var rollbackPoint = root.Snapshot();
      try {
        var request = new MoveRequest(gameId, newLocation);
        broadcastBeforeRequest(new MoveRequestAsIRequest(request));
        context.Flare(GetDeterministicHashCode());
        var superstate = superstateByGameId[gameId];
        string success = MoveRequestExecutor.Execute(context, superstate, request);
        context.Flare(success.DStr());
        broadcastAfterRequest(new MoveRequestAsIRequest(request));
        context.Flare(GetDeterministicHashCode());
        return success;
      //} catch (Exception) {
      //  Logger.Error("Caught exception, rolling back!");
      //  root.Revert(rollbackPoint);
      //  throw;
      } finally {
        root.Lock();
        root.FlushEvents();

        stopwatch.Stop();
        Console.WriteLine("RequestMove RunTime " + stopwatch.Elapsed.TotalMilliseconds);
      }
    }

    public string RequestFire(int gameId, int targetUnitId) {
      var stopwatch = new System.Diagnostics.Stopwatch();
      stopwatch.Start();

      root.Unlock();
      //var rollbackPoint = root.Snapshot();
      try {
        var request = new FireRequest(gameId, targetUnitId);
        broadcastBeforeRequest(new FireRequestAsIRequest(request));
        context.Flare(GetDeterministicHashCode());
        var superstate = superstateByGameId[gameId];
        string result = FireRequestExecutor.Execute(context, superstate, request);
        context.Flare(result.DStr());
        broadcastAfterRequest(new FireRequestAsIRequest(request));
        context.Flare(GetDeterministicHashCode());
        return result;
        //} catch (Exception) {
        //  Logger.Error("Caught exception, rolling back!");
        //  root.Revert(rollbackPoint);
        //  throw;
      } finally {
        root.Lock();
        root.FlushEvents();

        stopwatch.Stop();
        Console.WriteLine("RequestFire RunTime " + stopwatch.Elapsed.TotalMilliseconds);
      }
    }

    public string RequestAttack(int gameId, int targetUnitId) {
      var stopwatch = new System.Diagnostics.Stopwatch();
      stopwatch.Start();

      root.Unlock();
      //var rollbackPoint = root.Snapshot();
      try {
        var request = new AttackRequest(gameId, targetUnitId);
        broadcastBeforeRequest(new AttackRequestAsIRequest(request));
        context.Flare(GetDeterministicHashCode());
        var superstate = superstateByGameId[gameId];
        string success = AttackRequestExecutor.Execute(context, superstate, request);
        context.Flare(success.DStr());
        broadcastAfterRequest(new AttackRequestAsIRequest(request));
        context.Flare(GetDeterministicHashCode());
        return success;
      //} catch (Exception) {
      //  Logger.Error("Caught exception, rolling back!");
      //  root.Revert(rollbackPoint);
      //  throw;
      } finally {
        root.Lock();
        root.FlushEvents();

        stopwatch.Stop();
        Console.WriteLine("RequestAttack RunTime " + stopwatch.Elapsed.TotalMilliseconds);
      }
    }

    public string RequestDefend(int gameId) {
      var stopwatch = new System.Diagnostics.Stopwatch();
      stopwatch.Start();

      root.Unlock();
      //var rollbackPoint = root.Snapshot();
      try {
        var request = new DefendRequest(gameId);
        broadcastBeforeRequest(new DefendRequestAsIRequest(request));
        context.Flare(GetDeterministicHashCode());
        var superstate = superstateByGameId[gameId];
        string success = DefendRequestExecutor.Execute(context, superstate, request);
        context.Flare(success.DStr());
        broadcastAfterRequest(new DefendRequestAsIRequest(request));
        context.Flare(GetDeterministicHashCode());
        return success;
      //} catch (Exception) {
      //  Logger.Error("Caught exception, rolling back!");
      //  root.Revert(rollbackPoint);
      //  throw;
      } finally {
        root.Lock();
        root.FlushEvents();

        stopwatch.Stop();
        Console.WriteLine("RequestDefend RunTime " + stopwatch.Elapsed.TotalMilliseconds);
      }
    }

    public string RequestCounter(int gameId) {
      var stopwatch = new System.Diagnostics.Stopwatch();
      stopwatch.Start();

      root.Unlock();
      //var rollbackPoint = root.Snapshot();
      try {
        var request = new CounterRequest(gameId);
        broadcastBeforeRequest(new CounterRequestAsIRequest(request));
        context.Flare(GetDeterministicHashCode());
        var superstate = superstateByGameId[gameId];
        string result = CounterRequestExecutor.Execute(context, superstate, request);
        context.Flare(result.DStr());
        broadcastAfterRequest(new CounterRequestAsIRequest(request));
        context.Flare(GetDeterministicHashCode());
        return result;
        //} catch (Exception) {
        //  Logger.Error("Caught exception, rolling back!");
        //  root.Revert(rollbackPoint);
        //  throw;
      } finally {
        root.Lock();
        root.FlushEvents();

        stopwatch.Stop();
        Console.WriteLine("RequestCounter RunTime " + stopwatch.Elapsed.TotalMilliseconds);
      }
    }

    public string RequestFollowDirective(int gameId) {
      var stopwatch = new System.Diagnostics.Stopwatch();
      stopwatch.Start();

      root.Unlock();
      //var rollbackPoint = root.Snapshot();
      try {
        var request = new FollowDirectiveRequest(gameId);
        broadcastBeforeRequest(new FollowDirectiveRequestAsIRequest(request));
        context.Flare(GetDeterministicHashCode());
        var superstate = superstateByGameId[gameId];
        string success = FollowDirectiveRequestExecutor.Execute(context, superstate, request);
        context.Flare(success.DStr());
        broadcastAfterRequest(new FollowDirectiveRequestAsIRequest(request));
        context.Flare(GetDeterministicHashCode());
        return success;
      //} catch (Exception) {
      //  Logger.Error("Caught exception, rolling back!");
      //  root.Revert(rollbackPoint);
      //  throw;
      } finally {
        root.Lock();
        root.FlushEvents();

        stopwatch.Stop();
        Console.WriteLine("RequestFollowDirective RunTime " + stopwatch.Elapsed.TotalMilliseconds);
      }
    }

    public string RequestTimeAnchorMove(int gameId, Location destination) {
      var stopwatch = new System.Diagnostics.Stopwatch();
      stopwatch.Start();

      root.Unlock();
      //var rollbackPoint = root.Snapshot();
      try {
        var request = new TimeAnchorMoveRequest(gameId, destination);
        broadcastBeforeRequest(request.AsIRequest());
        context.Flare(GetDeterministicHashCode());
        var superstate = superstateByGameId[gameId];
        string success = TimeAnchorMoveRequestExecutor.Execute(context, superstate, request);
        context.Flare(success.DStr());
        broadcastAfterRequest(request.AsIRequest());
        context.Flare(GetDeterministicHashCode());
        return success;
        //} catch (Exception) {
        //  Logger.Error("Caught exception, rolling back!");
        //  root.Revert(rollbackPoint);
        //  throw;
      } finally {
        root.Lock();
        root.FlushEvents();

        stopwatch.Stop();
        Console.WriteLine("RequestTimeAnchorMove RunTime " + stopwatch.Elapsed.TotalMilliseconds);
      }
    }

    public string RequestTimeShift(int gameId) {
      var stopwatch = new System.Diagnostics.Stopwatch();
      stopwatch.Start();

      root.Unlock();
      //var rollbackPoint = root.Snapshot();
      try {
        var request = new TimeShiftRequest(gameId);
        broadcastBeforeRequest(new TimeShiftRequestAsIRequest(request));
        context.Flare(GetDeterministicHashCode());
        var superstate = superstateByGameId[gameId];
        string result = TimeShiftRequestExecutor.Execute(context, superstate, request);
        context.Flare(result.DStr());
        broadcastAfterRequest(new TimeShiftRequestAsIRequest(request));
        context.Flare(GetDeterministicHashCode());
        return result;
        //} catch (Exception) {
        //  Logger.Error("Caught exception, rolling back!");
        //  root.Revert(rollbackPoint);
        //  throw;
      } finally {
        root.Lock();
        root.FlushEvents();

        stopwatch.Stop();
        Console.WriteLine("RequestTimeShift RunTime " + stopwatch.Elapsed.TotalMilliseconds);
      }
    }

    public string RequestResume(int gameId) {

      var stopwatch = new System.Diagnostics.Stopwatch();
      stopwatch.Start();

      root.Unlock();
      //var rollbackPoint = root.Snapshot();
      try {
        var request = new ResumeRequest(gameId);
        broadcastBeforeRequest(new ResumeRequestAsIRequest(request));
        context.Flare(GetDeterministicHashCode());
        var superstate = superstateByGameId[gameId];
        string success = ResumeRequestExecutor.Execute(context, superstate, request);
        context.Flare(success.DStr());
        broadcastAfterRequest(new ResumeRequestAsIRequest(request));
        context.Flare(GetDeterministicHashCode());
        return success;
      //} catch (Exception) {
      //  Logger.Error("Caught exception, rolling back!");
      //  root.Revert(rollbackPoint);
      //  throw;
      } finally {
        root.Lock();
        root.FlushEvents();

        stopwatch.Stop();
        Console.WriteLine("RequestResume RunTime " + stopwatch.Elapsed.TotalMilliseconds);
      }
    }

    public int GetDeterministicHashCode() {
      return root.GetDeterministicHashCode();
    }

    public void SanityCheck() {
      root.CheckForViolations();
    }
  }
}
