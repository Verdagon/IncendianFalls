using System;
using Atharia.Model;

namespace AthPlayer {
  public interface ISuperstructure {
    Root GetRoot();
    Game RequestSetupIncendianFallsGame(int randomSeed, bool squareLevelsOnly);
    Game RequestSetupGauntletGame(int randomSeed, bool squareLevelsOnly);
    Game RequestSetupEmberDeepGame(int randomSeed, int startLevel, bool squareLevelsOnly);
    Atharia.Model.Terrain RequestSetupTerrain(Pattern pattern);
    string RequestMove(int gameId, Location newLocation);
    string RequestTimeAnchorMove(int gameId, Location newLocation);
    string RequestResume(int gameId);
    string RequestFollowDirective(int gameId);
    string RequestAttack(int gameId, int targetUnitId);
    string RequestTimeShift(int gameId);
    string RequestDefy(int gameId);
    string RequestTrigger(int gameId, string triggerName);
    string RequestCounter(int gameId);
    string RequestCancel(int gameId);
    string RequestCheat(int gameId, string cheatName);
    string RequestFire(int gameId, int targetUnitId);
    string RequestFireBomb(int gameId, Location location);
    string RequestMire(int gameId, int targetUnitId);
    string RequestInteract(int gameId);
    Superstate GetSuperstate(int gameId);
  }
}
