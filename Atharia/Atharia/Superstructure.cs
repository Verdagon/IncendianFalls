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

    public (List<IEffect>, Game) RequestSetupIncendianFallsGame(int randomSeed, bool squareLevelsOnly) {
      //var rollbackPoint = root.Snapshot();
      try {
        var request = new SetupIncendianFallsGameRequest(randomSeed, squareLevelsOnly);
        broadcastBeforeRequest(request.AsIRequest());
        context.Flare(GetDeterministicHashCode());
        Superstate superstate = null;
        var(events, game) = context.root.Transact(delegate () {
          return SetupIncendianFallsGameRequestExecutor.Execute(context, out superstate, request);
        });

        superstateByGameId.Add(game.id, superstate);

        broadcastAfterRequest(request.AsIRequest());
        context.Flare(GetDeterministicHashCode());
        return (events, game);
      } catch (Exception e) {
        root.logger.Error(e.Message + " " + e.StackTrace);
        throw;
        //} catch (Exception) {
        //  Logger.Error("Caught exception, rolling back!");
        //  root.Revert(rollbackPoint);
        //  throw;
      }
    }

    public (List<IEffect>, Game) RequestSetupGauntletGame(int randomSeed, bool squareLevelsOnly) {
      //var rollbackPoint = root.Snapshot();
      try {
        var request = new SetupGauntletGameRequest(randomSeed, squareLevelsOnly);
        broadcastBeforeRequest(request.AsIRequest());
        context.Flare(GetDeterministicHashCode());
        Superstate superstate = null;
        var(events, game) = context.root.Transact(delegate () {
          return SetupGauntletGameRequestExecutor.Execute(context, out superstate, request);
        });

        superstateByGameId.Add(game.id, superstate);

        // context.Flare(game.DStr());
        broadcastAfterRequest(request.AsIRequest());
        context.Flare(GetDeterministicHashCode());
        return (events, game);
      } catch (Exception e) {
        root.logger.Error(e.Message + " " + e.StackTrace);
        throw;
        //} catch (Exception) {
        //  Logger.Error("Caught exception, rolling back!");
        //  root.Revert(rollbackPoint);
        //  throw;
      }
    }


    public (List<IEffect>, Game) RequestSetupEmberDeepGame(int randomSeed, int startLevel, bool squareLevelsOnly) {
      //var rollbackPoint = root.Snapshot();
      try {
        var request = new SetupEmberDeepGameRequest(randomSeed, startLevel, squareLevelsOnly);
        broadcastBeforeRequest(request.AsIRequest());
        context.Flare(GetDeterministicHashCode());
        Superstate superstate = null;
        var(events, game) = context.root.Transact(delegate () {
          return SetupEmberDeepGameRequestExecutor.Execute(context, out superstate, request);
        });

        superstateByGameId.Add(game.id, superstate);

        // context.Flare(game.DStr());
        broadcastAfterRequest(request.AsIRequest());
        context.Flare(GetDeterministicHashCode());
        return (events, game);
      } catch (Exception e) {
        root.logger.Error(e.Message + " " + e.StackTrace);
        throw;
        //} catch (Exception) {
        //  Logger.Error("Caught exception, rolling back!");
        //  root.Revert(rollbackPoint);
        //  throw;
      }
    }

    public (List<IEffect>, Game) RequestSetupRavaArcanaGame(int randomSeed, int startLevel, bool squareLevelsOnly) {
      //var rollbackPoint = root.Snapshot();
      try {
        var request = new SetupRavaArcanaGameRequest(randomSeed, startLevel, squareLevelsOnly);
        broadcastBeforeRequest(request.AsIRequest());
        context.Flare(GetDeterministicHashCode());
        Superstate superstate = null;
        var(events, game) = context.root.Transact(delegate () {
          return SetupRavaArcanaGameRequestExecutor.Execute(context, out superstate, request);
        });

        superstateByGameId.Add(game.id, superstate);

        // context.Flare(game.DStr());
        broadcastAfterRequest(request.AsIRequest());
        context.Flare(GetDeterministicHashCode());
        return (events, game);
      } catch (Exception e) {
        root.logger.Error(e.Message + " " + e.StackTrace);
        throw;
        //} catch (Exception) {
        //  Logger.Error("Caught exception, rolling back!");
        //  root.Revert(rollbackPoint);
        //  throw;
      }
    }

    public (List<IEffect>, string) RequestCommAction(int gameId, int commId, int actionIndex) {
      //var rollbackPoint = root.Snapshot();
      try {
        var request = new CommActionRequest(gameId, commId, actionIndex);
        broadcastBeforeRequest(request.AsIRequest());
        context.Flare(GetDeterministicHashCode());
        var superstate = superstateByGameId[gameId];
        var (events, success) = context.root.Transact(delegate () {
          return CommActionRequestExecutor.Execute(context, superstate, request);
        });
        context.Flare(success.DStr());
        broadcastAfterRequest(request.AsIRequest());
        context.Flare(GetDeterministicHashCode());
        return (events, success);
      } catch (Exception e) {
        root.logger.Error(e.Message + "\n" + e.StackTrace);
        throw;
        //} catch (Exception) {
        //  Logger.Error("Caught exception, rolling back!");
        //  root.Revert(rollbackPoint);
        //  throw;
      }
    }

    public (List<IEffect>, string) RequestInteract(int gameId) {
      //var rollbackPoint = root.Snapshot();
      try {
        var request = new InteractRequest(gameId);
        broadcastBeforeRequest(new InteractRequestAsIRequest(request));
        context.Flare(GetDeterministicHashCode());
        var superstate = superstateByGameId[gameId];
        var(events, success) = context.root.Transact(delegate () {
          return InteractRequestExecutor.Execute(context, superstate, request);
        });
        context.Flare(success.DStr());
        broadcastAfterRequest(new InteractRequestAsIRequest(request));
        context.Flare(GetDeterministicHashCode());
        return (events, success);
      } catch (Exception e) {
        root.logger.Error(e.Message + "\n" + e.StackTrace);
        throw;
        //} catch (Exception) {
        //  Logger.Error("Caught exception, rolling back!");
        //  root.Revert(rollbackPoint);
        //  throw;
      }
    }

    public (List<IEffect>, string) RequestCheat(int gameId, string cheatName) {
      //var rollbackPoint = root.Snapshot();
      try {
        var request = new CheatRequest(gameId, cheatName);
        broadcastBeforeRequest(new CheatRequestAsIRequest(request));
        context.Flare(GetDeterministicHashCode());
        var superstate = superstateByGameId[gameId];
        var(events, success) = context.root.Transact(delegate () {
          return CheatRequestExecutor.Execute(context, superstate, request);
        });
        context.Flare(success.DStr());
        broadcastAfterRequest(new CheatRequestAsIRequest(request));
        context.Flare(GetDeterministicHashCode());
        return (events, success);
      } catch (Exception e) {
        root.logger.Error(e.Message + " " + e.StackTrace);
        throw;
        //} catch (Exception) {
        //  Logger.Error("Caught exception, rolling back!");
        //  root.Revert(rollbackPoint);
        //  throw;
      }
    }

    public (List<IEffect>, string) RequestMove(int gameId, Location newLocation) {
      //var rollbackPoint = root.Snapshot();
      try {
        var request = new MoveRequest(gameId, newLocation);
        broadcastBeforeRequest(new MoveRequestAsIRequest(request));
        context.Flare(GetDeterministicHashCode());
        var superstate = superstateByGameId[gameId];
        var  (events, success) = context.root.Transact(delegate () {
          return MoveRequestExecutor.Execute(context, superstate, request);
        });
        context.Flare(success.DStr());
        broadcastAfterRequest(new MoveRequestAsIRequest(request));
        context.Flare(GetDeterministicHashCode());
        return (events, success);
      } catch (Exception e) {
        root.logger.Error(e.Message + " " + e.StackTrace);
        throw;
        //} catch (Exception) {
        //  Logger.Error("Caught exception, rolling back!");
        //  root.Revert(rollbackPoint);
        //  throw;
      }
    }

    public (List<IEffect>, string) RequestFire(int gameId, int targetUnitId) {
      //var rollbackPoint = root.Snapshot();
      try {
        var request = new FireRequest(gameId, targetUnitId);
        broadcastBeforeRequest(new FireRequestAsIRequest(request));
        context.Flare(GetDeterministicHashCode());
        var superstate = superstateByGameId[gameId];
        var(events, result) = context.root.Transact(delegate () {
          return FireRequestExecutor.Execute(context, superstate, request);
        });
        context.Flare(result.DStr());
        broadcastAfterRequest(new FireRequestAsIRequest(request));
        context.Flare(GetDeterministicHashCode());
        return (events, result);
      } catch (Exception e) {
        root.logger.Error(e.Message + " " + e.StackTrace);
        throw;
        //} catch (Exception) {
        //  Logger.Error("Caught exception, rolling back!");
        //  root.Revert(rollbackPoint);
        //  throw;
      }
    }

    public (List<IEffect>, string) RequestFireBomb(int gameId, Location location) {
      //var rollbackPoint = root.Snapshot();
      try {
        var request = new FireBombRequest(gameId, location);
        broadcastBeforeRequest(request.AsIRequest());
        context.Flare(GetDeterministicHashCode());
        var superstate = superstateByGameId[gameId];
        var(events, result) = context.root.Transact(delegate () {
          return FireBombRequestExecutor.Execute(context, superstate, request);
        });
        context.Flare(result.DStr());
        broadcastAfterRequest(request.AsIRequest());
        context.Flare(GetDeterministicHashCode());
        return (events, result);
      } catch (Exception e) {
        root.logger.Error(e.Message + " " + e.StackTrace);
        throw;
        //} catch (Exception) {
        //  Logger.Error("Caught exception, rolling back!");
        //  root.Revert(rollbackPoint);
        //  throw;
      }
    }

    public (List<IEffect>, string) RequestMire(int gameId, int targetUnitId) {
      //var rollbackPoint = root.Snapshot();
      try {
        var request = new MireRequest(gameId, targetUnitId);
        broadcastBeforeRequest(new MireRequestAsIRequest(request));
        context.Flare(GetDeterministicHashCode());
        var superstate = superstateByGameId[gameId];
        var(events, result) = context.root.Transact(delegate () {
          return MireRequestExecutor.Execute(context, superstate, request);
        });
        context.Flare(result.DStr());
        broadcastAfterRequest(new MireRequestAsIRequest(request));
        context.Flare(GetDeterministicHashCode());
        return (events, result);
      } catch (Exception e) {
        root.logger.Error(e.Message + " " + e.StackTrace);
        throw;
        //} catch (Exception) {
        //  Logger.Error("Caught exception, rolling back!");
        //  root.Revert(rollbackPoint);
        //  throw;
      }
    }

    public (List<IEffect>, string) RequestAttack(int gameId, int targetUnitId) {
      
      //var rollbackPoint = root.Snapshot();
      try {
        var request = new AttackRequest(gameId, targetUnitId);
        broadcastBeforeRequest(new AttackRequestAsIRequest(request));
        context.Flare(GetDeterministicHashCode());
        var superstate = superstateByGameId[gameId];
        var  (events, success) = context.root.Transact(delegate () {
          return AttackRequestExecutor.Execute(context, superstate, request);
        });
        context.Flare(success.DStr());
        broadcastAfterRequest(new AttackRequestAsIRequest(request));
        context.Flare(GetDeterministicHashCode());
        return (events, success);
      } catch (Exception e) {
        root.logger.Error(e.Message + " " + e.StackTrace);
        throw;
        //} catch (Exception) {
        //  Logger.Error("Caught exception, rolling back!");
        //  root.Revert(rollbackPoint);
        //  throw;
      }
    }

    //public (List<IEffect>, string) RequestCancel(int gameId) {

    //  //var rollbackPoint = root.Snapshot();
    //  try {
    //    var request = new CancelRequest(gameId);
    //    broadcastBeforeRequest(request.AsIRequest());
    //    context.Flare(GetDeterministicHashCode());
    //    var superstate = superstateByGameId[gameId];
    //    var  (events, success) = context.root.Transact(delegate () {
    //      return CancelRequestExecutor.Execute(context, superstate, request);
    //    });
    //    context.Flare(success.DStr());
    //    broadcastAfterRequest(request.AsIRequest());
    //    context.Flare(GetDeterministicHashCode());
    //    return (events, success);
    //  } catch (Exception e) {
    //    root.logger.Error(e.Message + " " + e.StackTrace);
    //    throw;
    //    //} catch (Exception) {
    //    //  Logger.Error("Caught exception, rolling back!");
    //    //  root.Revert(rollbackPoint);
    //    //  throw;
    //  }
    //}

    public (List<IEffect>, string) RequestDefy(int gameId) {
      //var rollbackPoint = root.Snapshot();
      try {
        var request = new DefyRequest(gameId);
        broadcastBeforeRequest(new DefyRequestAsIRequest(request));
        context.Flare(GetDeterministicHashCode());
        var superstate = superstateByGameId[gameId];
        var  (events, success) = context.root.Transact(delegate () {
          return DefyRequestExecutor.Execute(context, superstate, request);
        });
        context.Flare(success.DStr());
        broadcastAfterRequest(new DefyRequestAsIRequest(request));
        context.Flare(GetDeterministicHashCode());
        return (events, success);
      } catch (Exception e) {
        root.logger.Error(e.Message + " " + e.StackTrace);
        throw;
        //} catch (Exception) {
        //  Logger.Error("Caught exception, rolling back!");
        //  root.Revert(rollbackPoint);
        //  throw;
      }
    }

    public (List<IEffect>, string) RequestCounter(int gameId) {
      //var rollbackPoint = root.Snapshot();
      try {
        var request = new CounterRequest(gameId);
        broadcastBeforeRequest(new CounterRequestAsIRequest(request));
        context.Flare(GetDeterministicHashCode());
        var superstate = superstateByGameId[gameId];
        var(events, result) = context.root.Transact(delegate () {
          return CounterRequestExecutor.Execute(context, superstate, request);
        });
        context.Flare(result.DStr());
        broadcastAfterRequest(new CounterRequestAsIRequest(request));
        context.Flare(GetDeterministicHashCode());
        return (events, result);
      } catch (Exception e) {
        root.logger.Error(e.Message + " " + e.StackTrace);
        throw;
        //} catch (Exception) {
        //  Logger.Error("Caught exception, rolling back!");
        //  root.Revert(rollbackPoint);
        //  throw;
      }
    }

    //public (List<IEffect>, string) RequestFollowDirective(int gameId) {
    //  //var rollbackPoint = root.Snapshot();
    //  try {
    //    var request = new FollowDirectiveRequest(gameId);
    //    broadcastBeforeRequest(new FollowDirectiveRequestAsIRequest(request));
    //    context.Flare(GetDeterministicHashCode());
    //    var superstate = superstateByGameId[gameId];
    //    var  (events, success) = context.root.Transact(delegate () {
    //      return FollowDirectiveRequestExecutor.Execute(context, superstate, request);
    //    });
    //    context.Flare(success.DStr());
    //    broadcastAfterRequest(new FollowDirectiveRequestAsIRequest(request));
    //    context.Flare(GetDeterministicHashCode());
    //    return (events, success);
    //  } catch (Exception e) {
    //    root.logger.Error(e.Message + " " + e.StackTrace);
    //    throw;
    //    //} catch (Exception) {
    //    //  Logger.Error("Caught exception, rolling back!");
    //    //  root.Revert(rollbackPoint);
    //    //  throw;
    //  }
    //}

    public (List<IEffect>, string) RequestTimeAnchorMove(int gameId, Location destination) {
      //var rollbackPoint = root.Snapshot();
      try {
        var request = new TimeAnchorMoveRequest(gameId, destination);
        broadcastBeforeRequest(request.AsIRequest());
        context.Flare(GetDeterministicHashCode());
        var superstate = superstateByGameId[gameId];
        var (events, success) =
          context.root.Transact(delegate () {
            return TimeAnchorMoveRequestExecutor.Execute(context, superstate, request);
          });
        context.Flare(success.DStr());
        broadcastAfterRequest(request.AsIRequest());
        context.Flare(GetDeterministicHashCode());
        return (events, success);
      } catch (Exception e) {
        root.logger.Error(e.Message + " " + e.StackTrace);
        throw;
        //} catch (Exception) {
        //  Logger.Error("Caught exception, rolling back!");
        //  root.Revert(rollbackPoint);
        //  throw;
      }
    }

    public (List<IEffect>, string) RequestTimeShift(int gameId) {
      //var rollbackPoint = root.Snapshot();
      try {
        var request = new TimeShiftRequest(gameId);
        broadcastBeforeRequest(new TimeShiftRequestAsIRequest(request));
        context.Flare(GetDeterministicHashCode());
        var superstate = superstateByGameId[gameId];
        var (events, result) = context.root.Transact(delegate () {
          return TimeShiftRequestExecutor.Execute(context, superstate, request);
        });
        context.Flare(result.DStr());
        broadcastAfterRequest(new TimeShiftRequestAsIRequest(request));
        context.Flare(GetDeterministicHashCode());
        return (events, result);
      } catch (Exception e) {
        root.logger.Error(e.Message + " " + e.StackTrace);
        throw;
        //} catch (Exception) {
        //  Logger.Error("Caught exception, rolling back!");
        //  root.Revert(rollbackPoint);
        //  throw;
      }
    }

    public (List<IEffect>, string) RequestResume(int gameId) {

      // Make sure the player just acted, or 

      //var rollbackPoint = root.Snapshot();
      try {
        var request = new ResumeRequest(gameId);
        broadcastBeforeRequest(new ResumeRequestAsIRequest(request));
        context.Flare(GetDeterministicHashCode());
        var superstate = superstateByGameId[gameId];
        var (events, success) = context.root.Transact<string>(delegate () {
          return ResumeRequestExecutor.Execute(context, superstate, request);
        });
        context.Flare(success.DStr());
        broadcastAfterRequest(new ResumeRequestAsIRequest(request));
        context.Flare(GetDeterministicHashCode());
        return (events, success);
      } catch (Exception e) {
        root.logger.Error(e.Message + "\n" + e.StackTrace);
        throw;
        //} catch (Exception) {
        //  Logger.Error("Caught exception, rolling back!");
        //  root.Revert(rollbackPoint);
        //  throw;
      }
    }

    // Read-only
    public List<Location> RequestFindPath(int gameId, int unitId, Location destination) {
      //var rollbackPoint = root.Snapshot();
      try {
        var request = new FindPathRequest(gameId, unitId, destination);
        context.Flare(GetDeterministicHashCode());
        var superstate = superstateByGameId[gameId];
        var (events, path) = context.root.Transact(delegate () {
          return FindPathRequestExecutor.Execute(context, superstate, request);
        });
        if (events.Count > 0) {
          root.logger.Error(events[0].ToString());
          Asserts.Assert(false);
        }
        context.Flare(GetDeterministicHashCode());
        return path;
      } catch (Exception e) {
        root.logger.Error(e.Message + "\n" + e.StackTrace);
        throw;
        //} catch (Exception) {
        //  Logger.Error("Caught exception, rolling back!");
        //  root.Revert(rollbackPoint);
        //  throw;
      }
    }

    //public (List<IEffect>, string) RequestContinuousResume(int gameId) {
    //  List<IEffect> effects = new List<IEffect>();
    //  var game = root.GetGame(gameId);
    //  var superstate = GetSuperstate(game);
    //  while (superstate.GetStateType() != MultiverseStateType.kBeforePlayerInput) {
    //    if (game.player.Exists() && !game.player.alive) {
    //      return (effects, "No player!");
    //    }
    //    var (newEffects, status) = RequestResume(game.id);
    //    effects.AddRange(newEffects);
    //    if (status != "") {
    //      return (effects, status);
    //    }
    //  }

    //  return (effects, "");
    //}



    public int GetDeterministicHashCode() {
      return root.GetDeterministicHashCode();
    }

    public void SanityCheck() {
      root.CheckForViolations();
    }
  }
}
