using System;
using System.Collections.Generic;
using Atharia.Model;

namespace IncendianFalls {
  public interface IRequestObserver {
    void BeforeRequest(IRequest request);
    void AfterRequest(IRequest request);
  }

  public class Superstructure {
    private readonly ILogger logger;

    private readonly Root root;
    private readonly SortedDictionary<int, List<IRequestObserver>> observers;

    public Superstructure(ILogger logger) {
      this.logger = logger;
      root = new Root();
      observers = new SortedDictionary<int, List<IRequestObserver>>();
    }

    public void AddObserver(int id, IRequestObserver observer) {
      if (!observers.ContainsKey(id)) {
        observers.Add(id, new List<IRequestObserver>());
      }
      observers[id].Add(observer);
    }

    private void broadcastBeforeRequest(int id, IRequest request) {
      if (observers.ContainsKey(0)) {
        foreach (var observer in observers[0]) {
          observer.BeforeRequest(request);
        }
      }
      if (id != 0) {
        if (observers.ContainsKey(id)) {
          foreach (var observer in observers[id]) {
            observer.BeforeRequest(request);
          }
        }
      }
    }

    private void broadcastAfterRequest(int id, IRequest request) {
      if (observers.ContainsKey(id)) {
        foreach (var observer in observers[id]) {
          observer.AfterRequest(request);
        }
      }
      if (id != 0) {
        if (observers.ContainsKey(0)) {
          foreach (var observer in observers[0]) {
            observer.AfterRequest(request);
          }
        }
      }
    }

    public Root GetRoot() {
      return root;
    }

    public Game RequestSetupGame(int randomSeed, bool squareLevelsOnly) {
      var request = new SetupGameRequest(randomSeed, squareLevelsOnly);
      broadcastBeforeRequest(0, new SetupGameRequestAsIRequest(request));
      var game = SetupGameRequestExecutor.Execute(root, randomSeed, squareLevelsOnly);
      broadcastAfterRequest(0, new SetupGameRequestAsIRequest(request));
      return game;
    }

    public bool RequestInteract(int gameId) {
      var request = new InteractRequest(gameId);
      broadcastBeforeRequest(0, new InteractRequestAsIRequest(request));
      var success = new InteractRequestExecutor(logger).Execute(root, gameId, -1337);
      broadcastAfterRequest(0, new InteractRequestAsIRequest(request));
      return success;
    }

    public bool RequestMove(int gameId, Location newLocation) {
      var request = new MoveRequest(gameId, newLocation);
      broadcastBeforeRequest(gameId, new MoveRequestAsIRequest(request));
      bool did = new MoveRequestExecutor(logger).Execute(root, gameId, newLocation);
      broadcastAfterRequest(gameId, new MoveRequestAsIRequest(request));
      return did;
    }

    public bool RequestAttack(int gameId, int targetUnitId) {
      var request = new AttackRequest(gameId, targetUnitId);
      broadcastBeforeRequest(gameId, new AttackRequestAsIRequest(request));
      bool success = new AttackRequestExecutor(logger).Execute(root, gameId, targetUnitId);
      broadcastAfterRequest(gameId, new AttackRequestAsIRequest(request));
      return success;
    }

    public bool RequestTimeShift(
        int gameId,
        RootIncarnation pastIncarnation,
        int futuremostTime) {
      var request = new TimeShiftRequest(gameId, pastIncarnation.version, futuremostTime);
      broadcastBeforeRequest(gameId, new TimeShiftRequestAsIRequest(request));
      bool success = new TimeShiftRequestExecutor(logger).Execute(root, gameId, pastIncarnation, futuremostTime);
      broadcastAfterRequest(gameId, new TimeShiftRequestAsIRequest(request));
      return success;
    }

    public bool RequestDefend(int gameId) {
      var request = new DefendRequest(gameId);
      broadcastBeforeRequest(gameId, new DefendRequestAsIRequest(request));
      bool success = new DefendRequestExecutor(logger).Execute(root, gameId);
      broadcastAfterRequest(gameId, new DefendRequestAsIRequest(request));
      return success;
    }

    public bool RequestFollowDirective(int gameId) {
      var request = new FollowDirectiveRequest(gameId);
      broadcastBeforeRequest(gameId, new FollowDirectiveRequestAsIRequest(request));
      bool success = new FollowDirectiveRequestExecutor(logger).Execute(root, gameId);
      broadcastAfterRequest(gameId, new FollowDirectiveRequestAsIRequest(request));
      return success;
    }

    public bool RequestResume(int gameId) {
      var request = new ResumeRequest(gameId);
      broadcastBeforeRequest(gameId, new ResumeRequestAsIRequest(request));
      bool success = new ResumeRequestExecutor(logger).Execute(root, gameId);
      broadcastAfterRequest(gameId, new ResumeRequestAsIRequest(request));
      return success;
    }

    public RootIncarnation Snapshot() {
      var request = new SnapshotRequest();
      broadcastBeforeRequest(0, new SnapshotRequestAsIRequest(request));
      var snapshot = root.Snapshot();
      broadcastAfterRequest(0, new SnapshotRequestAsIRequest(request));
      return snapshot;
    }
  }
}
