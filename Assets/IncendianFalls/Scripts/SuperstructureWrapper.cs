using System.Collections;
using System.Collections.Generic;
using IncendianFalls;
using Domino;
using UnityEngine;
using Atharia.Model;
using System;

namespace IncendianFalls {
  public class SuperstructureWrapper : ISuperstructure {
    Superstructure ss;

    bool timing = false;
    System.Diagnostics.Stopwatch stopwatch;

    public SuperstructureWrapper(Superstructure ss) {
      this.ss = ss;
    }

    public Root GetRoot() {
      return ss.GetRoot();
    }

    public Superstate GetSuperstate(int gameId) {
      return ss.GetSuperstate(GetRoot().GetGame(gameId));
    }

    public Game RequestSetupGame(int randomSeed, bool squareLevelsOnly, bool gauntletMode) {
      ss.GetRoot().logger.Error("RequestSetupGame, no state.");

      if (timing) {
        stopwatch = new System.Diagnostics.Stopwatch();
        stopwatch.Start();
      }

      ss.GetRoot().logger.Error("RequestSetupGame");

      var result = ss.RequestSetupGame(randomSeed, squareLevelsOnly, gauntletMode);

      if (timing) {
        stopwatch.Stop();
        UnityEngine.Debug.LogError("Logic time: " + stopwatch.Elapsed.TotalMilliseconds);

        stopwatch = new System.Diagnostics.Stopwatch();
        stopwatch.Start();
        ss.GetRoot().FlushEvents();
        stopwatch.Stop();
        UnityEngine.Debug.LogError("RunTime " + stopwatch.Elapsed.TotalMilliseconds);
      }

      return result;
    }

    public Atharia.Model.Terrain RequestSetupTerrain(Pattern pattern) {
      ss.GetRoot().logger.Error("RequestSetupTerrain, no state.");

      if (timing) {
        stopwatch = new System.Diagnostics.Stopwatch();
        stopwatch.Start();
      }

      ss.GetRoot().logger.Error("RequestSetupTerrain");

      var result = ss.RequestSetupTerrain(pattern);

      if (timing) {
        stopwatch.Stop();
        UnityEngine.Debug.LogError("Logic time: " + stopwatch.Elapsed.TotalMilliseconds);

        stopwatch = new System.Diagnostics.Stopwatch();
        stopwatch.Start();
        ss.GetRoot().FlushEvents();
        stopwatch.Stop();
        UnityEngine.Debug.LogError("RunTime " + stopwatch.Elapsed.TotalMilliseconds);
      }

      return result;
    }

    public string RequestCheat(int gameId, string cheatName) {
      var executionStateStr = ss.GetRoot().GetGame(gameId).executionState.SummaryStr();
      ss.GetRoot().logger.Error("RequestAttack State: " + executionStateStr);

      if (timing) {
        stopwatch = new System.Diagnostics.Stopwatch();
        stopwatch.Start();
      }

      var result = ss.RequestCheat(gameId, cheatName);

      if (timing) {
        stopwatch.Stop();
        UnityEngine.Debug.LogError("Logic time: " + stopwatch.Elapsed.TotalMilliseconds);

        stopwatch = new System.Diagnostics.Stopwatch();
        stopwatch.Start();
        ss.GetRoot().FlushEvents();

        stopwatch.Stop();
        UnityEngine.Debug.LogError("RunTime " + stopwatch.Elapsed.TotalMilliseconds);
      }

      return result;
    }

    public string RequestAttack(int gameId, int targetUnitId) {
      var executionStateStr = ss.GetRoot().GetGame(gameId).executionState.SummaryStr();
      ss.GetRoot().logger.Error("RequestAttack State: " + executionStateStr);

      if (timing) {
        stopwatch = new System.Diagnostics.Stopwatch();
        stopwatch.Start();
      }

      var result = ss.RequestAttack(gameId, targetUnitId);

      if (timing) {
        stopwatch.Stop();
        UnityEngine.Debug.LogError("Logic time: " + stopwatch.Elapsed.TotalMilliseconds);

        stopwatch = new System.Diagnostics.Stopwatch();
        stopwatch.Start();
        ss.GetRoot().FlushEvents();

        stopwatch.Stop();
        UnityEngine.Debug.LogError("RunTime " + stopwatch.Elapsed.TotalMilliseconds);
      }

      return result;
    }

    public string RequestFire(int gameId, int targetUnitId) {
      var executionStateStr = ss.GetRoot().GetGame(gameId).executionState.SummaryStr();
      ss.GetRoot().logger.Error("RequestAttack State: " + executionStateStr);

      if (timing) {
        stopwatch = new System.Diagnostics.Stopwatch();
        stopwatch.Start();
      }

      var result = ss.RequestFire(gameId, targetUnitId);

      if (timing) {
        stopwatch.Stop();
        UnityEngine.Debug.LogError("Logic time: " + stopwatch.Elapsed.TotalMilliseconds);

        stopwatch = new System.Diagnostics.Stopwatch();
        stopwatch.Start();
        ss.GetRoot().FlushEvents();

        stopwatch.Stop();
        UnityEngine.Debug.LogError("RunTime " + stopwatch.Elapsed.TotalMilliseconds);
      }

      return result;
    }

    public string RequestMove(int gameId, Location newLocation) {
      var executionStateStr = ss.GetRoot().GetGame(gameId).executionState.SummaryStr();
      ss.GetRoot().logger.Error("RequestMove State: " + executionStateStr);

      if (timing) {
        stopwatch = new System.Diagnostics.Stopwatch();
        stopwatch.Start();
      }

      var result = ss.RequestMove(gameId, newLocation);

      if (timing) {
        stopwatch.Stop();
        UnityEngine.Debug.LogError("Logic time: " + stopwatch.Elapsed.TotalMilliseconds);

        stopwatch = new System.Diagnostics.Stopwatch();
        stopwatch.Start();
        ss.GetRoot().FlushEvents();
        stopwatch.Stop();
        UnityEngine.Debug.LogError("RunTime " + stopwatch.Elapsed.TotalMilliseconds);
      }

      return result;
    }

    public string RequestTimeAnchorMove(int gameId, Location newLocation) {
      var executionStateStr = ss.GetRoot().GetGame(gameId).executionState.SummaryStr();
      ss.GetRoot().logger.Error("RequestMove State: " + executionStateStr);

      if (timing) {
        stopwatch = new System.Diagnostics.Stopwatch();
        stopwatch.Start();
      }

      var result = ss.RequestTimeAnchorMove(gameId, newLocation);

      if (timing) {
        stopwatch.Stop();
        UnityEngine.Debug.LogError("Logic time: " + stopwatch.Elapsed.TotalMilliseconds);

        stopwatch = new System.Diagnostics.Stopwatch();
        stopwatch.Start();
        ss.GetRoot().FlushEvents();
        stopwatch.Stop();
        UnityEngine.Debug.LogError("RunTime " + stopwatch.Elapsed.TotalMilliseconds);
      }

      return result;
    }

    public string RequestTimeShift(int gameId) {
      var executionStateStr = ss.GetRoot().GetGame(gameId).executionState.SummaryStr();
      ss.GetRoot().logger.Error("RequestTimeShift State: " + executionStateStr);

      if (timing) {
        stopwatch = new System.Diagnostics.Stopwatch();
        stopwatch.Start();
      }

      var result = ss.RequestTimeShift(gameId);

      if (timing) {
        stopwatch.Stop();
        UnityEngine.Debug.LogError("Logic time: " + stopwatch.Elapsed.TotalMilliseconds);

        stopwatch = new System.Diagnostics.Stopwatch();
        stopwatch.Start();
        ss.GetRoot().FlushEvents();
        stopwatch.Stop();
        UnityEngine.Debug.LogError("RunTime " + stopwatch.Elapsed.TotalMilliseconds);
      }

      return result;
    }

    public string RequestResume(int gameId) {
      var executionStateStr = ss.GetRoot().GetGame(gameId).executionState.SummaryStr();
      ss.GetRoot().logger.Error("RequestResume State: " + executionStateStr);

      if (timing) {
        stopwatch = new System.Diagnostics.Stopwatch();
        stopwatch.Start();
      }

      var result = ss.RequestResume(gameId);

      if (timing) {
        stopwatch.Stop();
        UnityEngine.Debug.LogError("Logic time: " + stopwatch.Elapsed.TotalMilliseconds);

        stopwatch = new System.Diagnostics.Stopwatch();
        stopwatch.Start();
        ss.GetRoot().FlushEvents();
        stopwatch.Stop();
        UnityEngine.Debug.LogError("RunTime " + stopwatch.Elapsed.TotalMilliseconds);
      }

      return result;
    }

    public string RequestDefend(int gameId) {
      var executionStateStr = ss.GetRoot().GetGame(gameId).executionState.SummaryStr();
      ss.GetRoot().logger.Error("RequestDefend State: " + executionStateStr);

      if (timing) {
        stopwatch = new System.Diagnostics.Stopwatch();
        stopwatch.Start();
      }

      var result = ss.RequestDefend(gameId);

      if (timing) {
        stopwatch.Stop();
        UnityEngine.Debug.LogError("Logic time: " + stopwatch.Elapsed.TotalMilliseconds);

        stopwatch = new System.Diagnostics.Stopwatch();
        stopwatch.Start();
        ss.GetRoot().FlushEvents();
        stopwatch.Stop();
        UnityEngine.Debug.LogError("RunTime " + stopwatch.Elapsed.TotalMilliseconds);
      }

      return result;
    }

    public string RequestCounter(int gameId) {
      var executionStateStr = ss.GetRoot().GetGame(gameId).executionState.SummaryStr();
      ss.GetRoot().logger.Error("RequestDefend State: " + executionStateStr);

      if (timing) {
        stopwatch = new System.Diagnostics.Stopwatch();
        stopwatch.Start();
      }

      var result = ss.RequestCounter(gameId);

      if (timing) {
        stopwatch.Stop();
        UnityEngine.Debug.LogError("Logic time: " + stopwatch.Elapsed.TotalMilliseconds);

        stopwatch = new System.Diagnostics.Stopwatch();
        stopwatch.Start();
        ss.GetRoot().FlushEvents();
        stopwatch.Stop();
        UnityEngine.Debug.LogError("RunTime " + stopwatch.Elapsed.TotalMilliseconds);
      }

      return result;
    }

    public string RequestInteract(int gameId) {
      var executionStateStr = ss.GetRoot().GetGame(gameId).executionState.SummaryStr();
      ss.GetRoot().logger.Error("RequestDefend State: " + executionStateStr);

      if (timing) {
        stopwatch = new System.Diagnostics.Stopwatch();
        stopwatch.Start();
      }

      var result = ss.RequestInteract(gameId);

      if (timing) {
        stopwatch.Stop();
        UnityEngine.Debug.LogError("Logic time: " + stopwatch.Elapsed.TotalMilliseconds);

        stopwatch = new System.Diagnostics.Stopwatch();
        stopwatch.Start();
        ss.GetRoot().FlushEvents();
        stopwatch.Stop();
        UnityEngine.Debug.LogError("RunTime " + stopwatch.Elapsed.TotalMilliseconds);
      }

      return result;
    }

    public string RequestFollowDirective(int gameId) {
      var executionStateStr = ss.GetRoot().GetGame(gameId).executionState.SummaryStr();
      ss.GetRoot().logger.Error("RequestFollowDirective State: " + executionStateStr);

      if (timing) {
        stopwatch = new System.Diagnostics.Stopwatch();
        stopwatch.Start();
      }

      string result = ss.RequestFollowDirective(gameId);

      if (timing) {
        stopwatch.Stop();
        UnityEngine.Debug.LogError("Logic time: " + stopwatch.Elapsed.TotalMilliseconds);

        stopwatch = new System.Diagnostics.Stopwatch();
        stopwatch.Start();
        ss.GetRoot().FlushEvents();
        stopwatch.Stop();
        UnityEngine.Debug.LogError("RunTime " + stopwatch.Elapsed.TotalMilliseconds);
      }

      return result;
    }
  }
}
