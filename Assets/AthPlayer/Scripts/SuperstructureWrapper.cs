using System.Collections;
using System.Collections.Generic;
using AthPlayer;
using Domino;
using UnityEngine;
using Atharia.Model;
using System;
using IncendianFalls;

namespace AthPlayer {
  public class SuperstructureWrapper : ISuperstructure {
    Superstructure ss;

    public SuperstructureWrapper(Superstructure ss) {
      this.ss = ss;
    }

    public Root GetRoot() {
      return ss.GetRoot();
    }

    public Superstate GetSuperstate(int gameId) {
      return ss.GetSuperstate(GetRoot().GetGame(gameId));
    }

    public Game RequestSetupGauntletGame(int randomSeed, bool squareLevelsOnly) {
      var result = ss.RequestSetupGauntletGame(randomSeed, squareLevelsOnly);

      return result;
    }

    public Game RequestSetupIncendianFallsGame(int randomSeed, bool squareLevelsOnly) {
      var result = ss.RequestSetupIncendianFallsGame(randomSeed, squareLevelsOnly);

      return result;
    }

    public Game RequestSetupEmberDeepGame(int randomSeed, bool squareLevelsOnly) {
      var result = ss.RequestSetupEmberDeepGame(randomSeed, squareLevelsOnly);

      return result;
    }

    public Atharia.Model.Terrain RequestSetupTerrain(Pattern pattern) {
      var result = ss.RequestSetupTerrain(pattern);

      return result;
    }

    public string RequestCheat(int gameId, string cheatName) {
      var result = ss.RequestCheat(gameId, cheatName);

      return result;
    }

    public string RequestAttack(int gameId, int targetUnitId) {
      var result = ss.RequestAttack(gameId, targetUnitId);

      return result;
    }

    public string RequestFire(int gameId, int targetUnitId) {
      //var executionStateStr = ss.GetRoot().GetGame(gameId).executionState.SummaryStr();
      //ss.GetRoot().logger.Error("RequestAttack State: " + executionStateStr);

      var result = ss.RequestFire(gameId, targetUnitId);

      return result;
    }

    public string RequestMove(int gameId, Location newLocation) {
      //var executionStateStr = ss.GetRoot().GetGame(gameId).executionState.SummaryStr();
      //ss.GetRoot().logger.Error("RequestMove State: " + executionStateStr);

      var result = ss.RequestMove(gameId, newLocation);

      return result;
    }

    public string RequestTimeAnchorMove(int gameId, Location newLocation) {
      //var executionStateStr = ss.GetRoot().GetGame(gameId).executionState.SummaryStr();
      //ss.GetRoot().logger.Error("RequestMove State: " + executionStateStr);

      var result = ss.RequestTimeAnchorMove(gameId, newLocation);

      return result;
    }

    public string RequestTimeShift(int gameId) {
      //var executionStateStr = ss.GetRoot().GetGame(gameId).executionState.SummaryStr();
      //ss.GetRoot().logger.Error("RequestTimeShift State: " + executionStateStr);

      var result = ss.RequestTimeShift(gameId);

      return result;
    }

    public string RequestResume(int gameId) {
      //var executionStateStr = ss.GetRoot().GetGame(gameId).executionState.SummaryStr();
      //ss.GetRoot().logger.Error("RequestResume State: " + executionStateStr);

      var result = ss.RequestResume(gameId);

      return result;
    }

    public string RequestDefend(int gameId) {
      //var executionStateStr = ss.GetRoot().GetGame(gameId).executionState.SummaryStr();
      //ss.GetRoot().logger.Error("RequestDefend State: " + executionStateStr);

      var result = ss.RequestDefend(gameId);

      return result;
    }

    public string RequestCounter(int gameId) {
      //var executionStateStr = ss.GetRoot().GetGame(gameId).executionState.SummaryStr();
      //ss.GetRoot().logger.Error("RequestDefend State: " + executionStateStr);

      var result = ss.RequestCounter(gameId);

      return result;
    }

    public string RequestInteract(int gameId) {
      //var executionStateStr = ss.GetRoot().GetGame(gameId).executionState.SummaryStr();
      //ss.GetRoot().logger.Error("RequestDefend State: " + executionStateStr);

      var result = ss.RequestInteract(gameId);

      return result;
    }

    public string RequestFollowDirective(int gameId) {
      //var executionStateStr = ss.GetRoot().GetGame(gameId).executionState.SummaryStr();
      //ss.GetRoot().logger.Error("RequestFollowDirective State: " + executionStateStr);

      string result = ss.RequestFollowDirective(gameId);

      return result;
    }

    public string RequestOverlayAction(int gameId, int buttonIndex) {
      return ss.RequestOverlayAction(gameId, buttonIndex);
    }
  }
}
