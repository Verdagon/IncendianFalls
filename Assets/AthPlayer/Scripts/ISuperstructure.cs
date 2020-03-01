using System;
using Atharia.Model;

namespace AthPlayer {
  public interface ISuperstructure {
    Root GetRoot();
    Game RequestSetupIncendianFallsGame(int randomSeed, bool squareLevelsOnly);
    Game RequestSetupGauntletGame(int randomSeed, bool squareLevelsOnly);
    Game RequestSetupEmberDeepGame(int randomSeed, bool squareLevelsOnly);
    Atharia.Model.Terrain RequestSetupTerrain(Pattern pattern);
    string RequestMove(int gameId, Location newLocation);
    string RequestTimeAnchorMove(int gameId, Location newLocation);
    string RequestResume(int gameId);
    string RequestFollowDirective(int gameId);
    string RequestAttack(int gameId, int targetUnitId);
    string RequestTimeShift(int gameId);
    string RequestDefend(int gameId);
    string RequestOverlayAction(int gameId, int buttonIndex);
    string RequestCounter(int gameId);
    string RequestCheat(int gameId, string cheatName);
    string RequestFire(int gameId, int targetUnitId);
    string RequestInteract(int gameId);
    Superstate GetSuperstate(int gameId);
  }
}
