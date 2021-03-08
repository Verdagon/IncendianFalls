using System.Collections;
using System.Collections.Generic;
using AthPlayer;
using Domino;
using UnityEngine;
using Atharia.Model;
using System;
using IncendianFalls;

namespace AthPlayer {
  public class SuperstructureWrapper {
    Superstructure ss;
    public Queue<IEffect> waitingEffects { get; private set; }

    public SuperstructureWrapper(Superstructure ss) {
      this.ss = ss;
      waitingEffects = new Queue<IEffect>();
    }

    public Root GetRoot() {
      return ss.GetRoot();
    }

    public Superstate GetSuperstate(int gameId) {
      return ss.GetSuperstate(GetRoot().GetGame(gameId));
    }

    public Game RequestSetupGauntletGame(int randomSeed, bool squareLevelsOnly) {
      Asserts.Assert(waitingEffects.Count == 0);
      var (effects, result) = ss.RequestSetupGauntletGame(randomSeed, squareLevelsOnly);
      foreach (var effect in effects) {
        waitingEffects.Enqueue(effect);
      }
      return result;
    }

    public Game RequestSetupIncendianFallsGame(int randomSeed, bool squareLevelsOnly) {
      Asserts.Assert(waitingEffects.Count == 0);
      var (effects, result) = ss.RequestSetupIncendianFallsGame(randomSeed, squareLevelsOnly);
      foreach (var effect in effects) {
        waitingEffects.Enqueue(effect);
      }
      return result;
    }

    public Game RequestSetupEmberDeepGame(int randomSeed, int startLevel, bool squareLevelsOnly) {
      Asserts.Assert(waitingEffects.Count == 0);
      var (effects, result) = ss.RequestSetupEmberDeepGame(randomSeed, startLevel, squareLevelsOnly);
      foreach (var effect in effects) {
        waitingEffects.Enqueue(effect);
      }
      return result;
    }

    public Game RequestSetupRavaArcanaGame(int randomSeed, int startLevel, bool squareLevelsOnly) {
      Asserts.Assert(waitingEffects.Count == 0);
      var (effects, result) = ss.RequestSetupRavaArcanaGame(randomSeed, startLevel, squareLevelsOnly);
      foreach (var effect in effects) {
        waitingEffects.Enqueue(effect);
      }
      return result;
    }

    //public Atharia.Model.Terrain RequestSetupTerrain(Pattern pattern) {
    //  Asserts.Assert(waitingEffects.Count == 0);
    //  var (effects, result) = ss.RequestSetupTerrain(pattern);
    
      //foreach (var effect in effects) {
      //  waitingEffects.Enqueue(effect);
      //}
    //  return result;
    //}

    public string RequestCheat(int gameId, string cheatName) {
      Asserts.Assert(waitingEffects.Count == 0);
      var (effects, result) = ss.RequestCheat(gameId, cheatName);
      foreach (var effect in effects) {
        waitingEffects.Enqueue(effect);
      }
      return result;
    }

    public string RequestAttack(int gameId, int targetUnitId) {
      Asserts.Assert(waitingEffects.Count == 0);
      var (effects, result) = ss.RequestAttack(gameId, targetUnitId);
      foreach (var effect in effects) {
        waitingEffects.Enqueue(effect);
      }
      return result;
    }

    public string RequestFire(int gameId, int targetUnitId) {
      Asserts.Assert(waitingEffects.Count == 0);
      var (effects, result) = ss.RequestFire(gameId, targetUnitId);
      foreach (var effect in effects) {
        waitingEffects.Enqueue(effect);
      }
      return result;
    }

    public string RequestFireBomb(int gameId, Location location) {
      Asserts.Assert(waitingEffects.Count == 0);
      var (effects, result) = ss.RequestFireBomb(gameId, location);
      foreach (var effect in effects) {
        waitingEffects.Enqueue(effect);
      }
      return result;
    }

    public string RequestMire(int gameId, int targetUnitId) {
      Asserts.Assert(waitingEffects.Count == 0);
      var (effects, result) = ss.RequestMire(gameId, targetUnitId);
      foreach (var effect in effects) {
        waitingEffects.Enqueue(effect);
      }
      return result;
    }

    public string RequestMove(int gameId, Location newLocation) {
      Asserts.Assert(waitingEffects.Count == 0);
      var (effects, result) = ss.RequestMove(gameId, newLocation);
      foreach (var effect in effects) {
        waitingEffects.Enqueue(effect);
      }
      return result;
    }

    public string RequestTimeAnchorMove(int gameId, Location newLocation) {
      Asserts.Assert(waitingEffects.Count == 0);
      var (effects, result) = ss.RequestTimeAnchorMove(gameId, newLocation);
      foreach (var effect in effects) {
        waitingEffects.Enqueue(effect);
      }
      return result;
    }

    public string RequestTimeShift(int gameId) {
      Asserts.Assert(waitingEffects.Count == 0);
      var (effects, result) = ss.RequestTimeShift(gameId);
      foreach (var effect in effects) {
        waitingEffects.Enqueue(effect);
      }
      return result;
    }

    public string RequestResume(int gameId) {
      Asserts.Assert(waitingEffects.Count == 0);
      var (effects, result) = ss.RequestResume(gameId);
      foreach (var effect in effects) {
        waitingEffects.Enqueue(effect);
      }
      return result;
    }

    public string RequestDefy(int gameId) {
      Asserts.Assert(waitingEffects.Count == 0);
      var (effects, result) = ss.RequestDefy(gameId);
      foreach (var effect in effects) {
        waitingEffects.Enqueue(effect);
      }
      return result;
    }

    public string RequestCounter(int gameId) {
      Asserts.Assert(waitingEffects.Count == 0);
      var (effects, result) = ss.RequestCounter(gameId);
      foreach (var effect in effects) {
        waitingEffects.Enqueue(effect);
      }
      return result;
    }

    public string RequestInteract(int gameId) {
      Asserts.Assert(waitingEffects.Count == 0);
      var (effects, result) = ss.RequestInteract(gameId);
      foreach (var effect in effects) {
        waitingEffects.Enqueue(effect);
      }
      return result;
    }

    public string RequestCommAction(int gameId, int commId, int actionIndex) {
      Asserts.Assert(waitingEffects.Count == 0);
      var (effects, result) = ss.RequestCommAction(gameId, commId, actionIndex);
      foreach (var effect in effects) {
        waitingEffects.Enqueue(effect);
      }
      return result;
    }
  }
}
