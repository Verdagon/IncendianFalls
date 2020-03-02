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
      foreach (var observer in observers) {
        observer.OnFlare(message);
      }
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
      foreach (var observer in observers) {
        observer.BeforeRequest(request);
      }
    }

    private void broadcastAfterRequest(IRequest request) {
      foreach (var observer in observers) {
        observer.AfterRequest(request);
      }
    }

    public Root GetRoot() {
      return root;
    }

    public Superstate GetSuperstate(Game game) {
      return superstateByGameId[game.id];
    }

    public Game RequestSetupIncendianFallsGame(int randomSeed, bool squareLevelsOnly) {
      //var rollbackPoint = root.Snapshot();
      try {
        var request = new SetupIncendianFallsGameRequest(randomSeed, squareLevelsOnly);
        broadcastBeforeRequest(request.AsIRequest());
        context.Flare(GetDeterministicHashCode());
        Superstate superstate = null;
        var game = context.root.Transact(delegate () {
          return SetupIncendianFallsGameRequestExecutor.Execute(context, out superstate, request);
        });

        superstateByGameId.Add(game.id, superstate);

        broadcastAfterRequest(request.AsIRequest());
        context.Flare(GetDeterministicHashCode());
        return game;
      } catch (Exception e) {
        root.logger.Error(e.Message);
        throw e;
        //} catch (Exception) {
        //  Logger.Error("Caught exception, rolling back!");
        //  root.Revert(rollbackPoint);
        //  throw;
      }
    }

    public Game RequestSetupGauntletGame(int randomSeed, bool squareLevelsOnly) {
      //var rollbackPoint = root.Snapshot();
      try {
        var request = new SetupGauntletGameRequest(randomSeed, squareLevelsOnly);
        broadcastBeforeRequest(request.AsIRequest());
        context.Flare(GetDeterministicHashCode());
        Superstate superstate = null;
        var game = context.root.Transact(delegate () {
          return SetupGauntletGameRequestExecutor.Execute(context, out superstate, request);
        });

        superstateByGameId.Add(game.id, superstate);

        // context.Flare(game.DStr());
        broadcastAfterRequest(request.AsIRequest());
        context.Flare(GetDeterministicHashCode());
        return game;
      } catch (Exception e) {
        root.logger.Error(e.Message);
        throw e;
        //} catch (Exception) {
        //  Logger.Error("Caught exception, rolling back!");
        //  root.Revert(rollbackPoint);
        //  throw;
      }
    }

    public Game RequestSetupEmberDeepGame(int randomSeed, bool squareLevelsOnly) {
      //var rollbackPoint = root.Snapshot();
      try {
        var request = new SetupEmberDeepGameRequest(randomSeed, squareLevelsOnly);
        broadcastBeforeRequest(request.AsIRequest());
        context.Flare(GetDeterministicHashCode());
        Superstate superstate = null;
        var game = context.root.Transact(delegate () {
          return SetupEmberDeepGameRequestExecutor.Execute(context, out superstate, request);
        });

        superstateByGameId.Add(game.id, superstate);

        // context.Flare(game.DStr());
        broadcastAfterRequest(request.AsIRequest());
        context.Flare(GetDeterministicHashCode());
        return game;
      } catch (Exception e) {
        root.logger.Error(e.Message);
        throw e;
        //} catch (Exception) {
        //  Logger.Error("Caught exception, rolling back!");
        //  root.Revert(rollbackPoint);
        //  throw;
      }
    }

    public string RequestTrigger(int gameId, string triggerName) {
      //var rollbackPoint = root.Snapshot();
      try {
        var request = new TriggerRequest(gameId, triggerName);
        broadcastBeforeRequest(request.AsIRequest());
        context.Flare(GetDeterministicHashCode());
        var superstate = superstateByGameId[gameId];
        var success = context.root.Transact(delegate () {
          return TriggerRequestExecutor.Execute(context, superstate, request);
        });
        context.Flare(success.DStr());
        broadcastAfterRequest(request.AsIRequest());
        context.Flare(GetDeterministicHashCode());
        return success;
      } catch (Exception e) {
        root.logger.Error(e.Message + "\n" + e.StackTrace);
        throw e;
        //} catch (Exception) {
        //  Logger.Error("Caught exception, rolling back!");
        //  root.Revert(rollbackPoint);
        //  throw;
      }
    }

    public Terrain RequestSetupTerrain(Pattern pattern) {
      //var rollbackPoint = root.Snapshot();
      try {
        var request = new SetupTerrainRequest(pattern);
        broadcastBeforeRequest(new SetupTerrainRequestAsIRequest(request));
        context.Flare(GetDeterministicHashCode());
        var terrain = context.root.Transact(delegate () {
          return SetupTerrainRequestExecutor.Execute(context, request);
        });

        // context.Flare(game.DStr());
        broadcastAfterRequest(new SetupTerrainRequestAsIRequest(request));
        context.Flare(GetDeterministicHashCode());
        return terrain;
        //} catch (Exception) {
        //  Logger.Error("Caught exception, rolling back!");
        //  root.Revert(rollbackPoint);
        //  throw;
      } catch (Exception e) {
        root.logger.Error(e.Message + "\n" + e.StackTrace);
        throw e;
      }
    }

    public string RequestInteract(int gameId) {
      //var rollbackPoint = root.Snapshot();
      try {
        var request = new InteractRequest(gameId);
        broadcastBeforeRequest(new InteractRequestAsIRequest(request));
        context.Flare(GetDeterministicHashCode());
        var superstate = superstateByGameId[gameId];
        var success = context.root.Transact(delegate () {
          return InteractRequestExecutor.Execute(context, superstate, request);
        });
        context.Flare(success.DStr());
        broadcastAfterRequest(new InteractRequestAsIRequest(request));
        context.Flare(GetDeterministicHashCode());
        return success;
      } catch (Exception e) {
        root.logger.Error(e.Message + "\n" + e.StackTrace);
        throw e;
        //} catch (Exception) {
        //  Logger.Error("Caught exception, rolling back!");
        //  root.Revert(rollbackPoint);
        //  throw;
      }
    }

    public string RequestCheat(int gameId, string cheatName) {
      //var rollbackPoint = root.Snapshot();
      try {
        var request = new CheatRequest(gameId, cheatName);
        broadcastBeforeRequest(new CheatRequestAsIRequest(request));
        context.Flare(GetDeterministicHashCode());
        var superstate = superstateByGameId[gameId];
        var success = context.root.Transact(delegate () {
          return CheatRequestExecutor.Execute(context, superstate, request);
        });
        context.Flare(success.DStr());
        broadcastAfterRequest(new CheatRequestAsIRequest(request));
        context.Flare(GetDeterministicHashCode());
        return success;
      } catch (Exception e) {
        root.logger.Error(e.Message);
        throw e;
        //} catch (Exception) {
        //  Logger.Error("Caught exception, rolling back!");
        //  root.Revert(rollbackPoint);
        //  throw;
      }
    }

    public string RequestMove(int gameId, Location newLocation) {
      //var rollbackPoint = root.Snapshot();
      try {
        var request = new MoveRequest(gameId, newLocation);
        broadcastBeforeRequest(new MoveRequestAsIRequest(request));
        context.Flare(GetDeterministicHashCode());
        var superstate = superstateByGameId[gameId];
        string success = context.root.Transact(delegate () {
          return MoveRequestExecutor.Execute(context, superstate, request);
        });
        context.Flare(success.DStr());
        broadcastAfterRequest(new MoveRequestAsIRequest(request));
        context.Flare(GetDeterministicHashCode());
        return success;
      } catch (Exception e) {
        root.logger.Error(e.Message + " " + e.StackTrace);
        throw e;
        //} catch (Exception) {
        //  Logger.Error("Caught exception, rolling back!");
        //  root.Revert(rollbackPoint);
        //  throw;
      }
    }

    public string RequestFire(int gameId, int targetUnitId) {
      

      //var rollbackPoint = root.Snapshot();
      try {
        var request = new FireRequest(gameId, targetUnitId);
        broadcastBeforeRequest(new FireRequestAsIRequest(request));
        context.Flare(GetDeterministicHashCode());
        var superstate = superstateByGameId[gameId];
        string result = context.root.Transact(delegate () {
          return FireRequestExecutor.Execute(context, superstate, request);
        });
        context.Flare(result.DStr());
        broadcastAfterRequest(new FireRequestAsIRequest(request));
        context.Flare(GetDeterministicHashCode());
        return result;
      } catch (Exception e) {
        root.logger.Error(e.Message);
        throw e;
        //} catch (Exception) {
        //  Logger.Error("Caught exception, rolling back!");
        //  root.Revert(rollbackPoint);
        //  throw;
      }
    }

    public string RequestAttack(int gameId, int targetUnitId) {
      
      //var rollbackPoint = root.Snapshot();
      try {
        var request = new AttackRequest(gameId, targetUnitId);
        broadcastBeforeRequest(new AttackRequestAsIRequest(request));
        context.Flare(GetDeterministicHashCode());
        var superstate = superstateByGameId[gameId];
        string success = context.root.Transact(delegate () {
          return AttackRequestExecutor.Execute(context, superstate, request);
        });
        context.Flare(success.DStr());
        broadcastAfterRequest(new AttackRequestAsIRequest(request));
        context.Flare(GetDeterministicHashCode());
        return success;
      } catch (Exception e) {
        root.logger.Error(e.Message);
        throw e;
        //} catch (Exception) {
        //  Logger.Error("Caught exception, rolling back!");
        //  root.Revert(rollbackPoint);
        //  throw;
      }
    }

    public string RequestDefend(int gameId) {
      //var rollbackPoint = root.Snapshot();
      try {
        var request = new DefendRequest(gameId);
        broadcastBeforeRequest(new DefendRequestAsIRequest(request));
        context.Flare(GetDeterministicHashCode());
        var superstate = superstateByGameId[gameId];
        string success = context.root.Transact(delegate () {
          return DefendRequestExecutor.Execute(context, superstate, request);
        });
        context.Flare(success.DStr());
        broadcastAfterRequest(new DefendRequestAsIRequest(request));
        context.Flare(GetDeterministicHashCode());
        return success;
      } catch (Exception e) {
        root.logger.Error(e.Message);
        throw e;
        //} catch (Exception) {
        //  Logger.Error("Caught exception, rolling back!");
        //  root.Revert(rollbackPoint);
        //  throw;
      }
    }

    public string RequestCounter(int gameId) {
      //var rollbackPoint = root.Snapshot();
      try {
        var request = new CounterRequest(gameId);
        broadcastBeforeRequest(new CounterRequestAsIRequest(request));
        context.Flare(GetDeterministicHashCode());
        var superstate = superstateByGameId[gameId];
        string result = context.root.Transact(delegate () {
          return CounterRequestExecutor.Execute(context, superstate, request);
        });
        context.Flare(result.DStr());
        broadcastAfterRequest(new CounterRequestAsIRequest(request));
        context.Flare(GetDeterministicHashCode());
        return result;
      } catch (Exception e) {
        root.logger.Error(e.Message);
        throw e;
        //} catch (Exception) {
        //  Logger.Error("Caught exception, rolling back!");
        //  root.Revert(rollbackPoint);
        //  throw;
      }
    }

    public string RequestFollowDirective(int gameId) {
      //var rollbackPoint = root.Snapshot();
      try {
        var request = new FollowDirectiveRequest(gameId);
        broadcastBeforeRequest(new FollowDirectiveRequestAsIRequest(request));
        context.Flare(GetDeterministicHashCode());
        var superstate = superstateByGameId[gameId];
        string success = context.root.Transact(delegate () {
          return FollowDirectiveRequestExecutor.Execute(context, superstate, request);
        });
        context.Flare(success.DStr());
        broadcastAfterRequest(new FollowDirectiveRequestAsIRequest(request));
        context.Flare(GetDeterministicHashCode());
        return success;
      } catch (Exception e) {
        root.logger.Error(e.Message);
        throw e;
        //} catch (Exception) {
        //  Logger.Error("Caught exception, rolling back!");
        //  root.Revert(rollbackPoint);
        //  throw;
      }
    }

    public string RequestTimeAnchorMove(int gameId, Location destination) {
      //var rollbackPoint = root.Snapshot();
      try {
        var request = new TimeAnchorMoveRequest(gameId, destination);
        broadcastBeforeRequest(request.AsIRequest());
        context.Flare(GetDeterministicHashCode());
        var superstate = superstateByGameId[gameId];
        string success = context.root.Transact(delegate () {
          return TimeAnchorMoveRequestExecutor.Execute(context, superstate, request);
        });
        context.Flare(success.DStr());
        broadcastAfterRequest(request.AsIRequest());
        context.Flare(GetDeterministicHashCode());
        return success;
      } catch (Exception e) {
        root.logger.Error(e.Message);
        throw e;
        //} catch (Exception) {
        //  Logger.Error("Caught exception, rolling back!");
        //  root.Revert(rollbackPoint);
        //  throw;
      }
    }

    public string RequestTimeShift(int gameId) {
      //var rollbackPoint = root.Snapshot();
      try {
        var request = new TimeShiftRequest(gameId);
        broadcastBeforeRequest(new TimeShiftRequestAsIRequest(request));
        context.Flare(GetDeterministicHashCode());
        var superstate = superstateByGameId[gameId];
        string result = context.root.Transact(delegate () {
          return TimeShiftRequestExecutor.Execute(context, superstate, request);
        });
        context.Flare(result.DStr());
        broadcastAfterRequest(new TimeShiftRequestAsIRequest(request));
        context.Flare(GetDeterministicHashCode());
        return result;
      } catch (Exception e) {
        root.logger.Error(e.Message);
        throw e;
        //} catch (Exception) {
        //  Logger.Error("Caught exception, rolling back!");
        //  root.Revert(rollbackPoint);
        //  throw;
      }
    }

    public string RequestResume(int gameId) {

      //var rollbackPoint = root.Snapshot();
      try {
        var request = new ResumeRequest(gameId);
        broadcastBeforeRequest(new ResumeRequestAsIRequest(request));
        context.Flare(GetDeterministicHashCode());
        var superstate = superstateByGameId[gameId];
        string success = context.root.Transact(delegate () {
          return ResumeRequestExecutor.Execute(context, superstate, request);
        });
        context.Flare(success.DStr());
        broadcastAfterRequest(new ResumeRequestAsIRequest(request));
        context.Flare(GetDeterministicHashCode());
        return success;
      } catch (Exception e) {
        root.logger.Error(e.Message);
        throw e;
        //} catch (Exception) {
        //  Logger.Error("Caught exception, rolling back!");
        //  root.Revert(rollbackPoint);
        //  throw;
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
