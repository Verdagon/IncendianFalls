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

    public Superstructure(ILogger logger) {
      observers = new List<ISuperstructureObserver>();
      root = new Root(logger);
      context = new SSContext(logger, root, observers);
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

    public Game RequestSetupGame(int randomSeed, bool squareLevelsOnly) {
      root.Unlock();
      //var rollbackPoint = root.Snapshot();
      try {
        var request = new SetupGameRequest(randomSeed, squareLevelsOnly);
        broadcastBeforeRequest(new SetupGameRequestAsIRequest(request));
        context.Flare(GetDeterministicHashCode());
        var game = SetupGameRequestExecutor.Execute(context, randomSeed, squareLevelsOnly);
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
      }
    }

    public bool RequestInteract(int gameId) {
      root.Unlock();
      //var rollbackPoint = root.Snapshot();
      try {
        var request = new InteractRequest(gameId);
        broadcastBeforeRequest(new InteractRequestAsIRequest(request));
        context.Flare(GetDeterministicHashCode());
        var success = InteractRequestExecutor.Execute(context, gameId, -1337);
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
      }
    }

    public bool RequestMove(int gameId, Location newLocation) {
      root.Unlock();
      //var rollbackPoint = root.Snapshot();
      try {
        var request = new MoveRequest(gameId, newLocation);
        broadcastBeforeRequest(new MoveRequestAsIRequest(request));
        context.Flare(GetDeterministicHashCode());
        bool success = MoveRequestExecutor.Execute(context, gameId, newLocation);
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
      }
    }

    public bool RequestAttack(int gameId, int targetUnitId) {
      root.Unlock();
      //var rollbackPoint = root.Snapshot();
      try {
        var request = new AttackRequest(gameId, targetUnitId);
        broadcastBeforeRequest(new AttackRequestAsIRequest(request));
        context.Flare(GetDeterministicHashCode());
        bool success = AttackRequestExecutor.Execute(context, gameId, targetUnitId);
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
      }
    }

    public bool RequestTimeShift(
        int gameId,
        RootIncarnation pastIncarnation,
        int futuremostTime) {
      root.Unlock();
      //var rollbackPoint = root.Snapshot();
      try {
        var request = new TimeShiftRequest(gameId, pastIncarnation.version, futuremostTime);
        broadcastBeforeRequest(new TimeShiftRequestAsIRequest(request));
        context.Flare(GetDeterministicHashCode());
        bool success = TimeShiftRequestExecutor.Execute(context, gameId, pastIncarnation, futuremostTime);
        context.Flare(success.DStr());
        broadcastAfterRequest(new TimeShiftRequestAsIRequest(request));
        context.Flare(GetDeterministicHashCode());
        return success;
      //} catch (Exception) {
      //  Logger.Error("Caught exception, rolling back!");
      //  root.Revert(rollbackPoint);
      //  throw;
      } finally {
        root.Lock();
      }
    }

    public bool RequestDefend(int gameId) {
      root.Unlock();
      //var rollbackPoint = root.Snapshot();
      try {
        var request = new DefendRequest(gameId);
        broadcastBeforeRequest(new DefendRequestAsIRequest(request));
        context.Flare(GetDeterministicHashCode());
        bool success = DefendRequestExecutor.Execute(context, gameId);
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
      }
    }

    public bool RequestFollowDirective(int gameId) {
      root.Unlock();
      //var rollbackPoint = root.Snapshot();
      try {
        var request = new FollowDirectiveRequest(gameId);
        broadcastBeforeRequest(new FollowDirectiveRequestAsIRequest(request));
        context.Flare(GetDeterministicHashCode());
        bool success = FollowDirectiveRequestExecutor.Execute(context, gameId);
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
      }
    }

    public bool RequestResume(int gameId) {
      root.Unlock();
      //var rollbackPoint = root.Snapshot();
      try {
        var request = new ResumeRequest(gameId);
        broadcastBeforeRequest(new ResumeRequestAsIRequest(request));
        context.Flare(GetDeterministicHashCode());
        bool success = ResumeRequestExecutor.Execute(context, gameId);
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
      }
    }

    public RootIncarnation Snapshot() {
      root.Unlock();
      var request = new SnapshotRequest();
      broadcastBeforeRequest(new SnapshotRequestAsIRequest(request));
      context.Flare(GetDeterministicHashCode());
      var snapshot = root.Snapshot();
      // context.Flare(snapshot.DStr());
      broadcastAfterRequest(new SnapshotRequestAsIRequest(request));
      context.Flare(GetDeterministicHashCode());
      root.Lock();
      return snapshot;
    }

    public int GetDeterministicHashCode() {
      return root.GetDeterministicHashCode();
    }

    public void SanityCheck() {
      root.CheckForViolations();
    }
  }
}
