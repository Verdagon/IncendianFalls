using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class GameIncarnation {
  public readonly int rand;
  public readonly bool squareLevelsOnly;
  public readonly int levels;
  public  int player;
  public  int level;
  public  int time;
  public readonly int executionState;
  public  string instructions;
  public  bool hideInput;
  public readonly int events;
  public readonly int eventedUnits;
  public readonly int eventedTerrainTiles;
  public GameIncarnation(
      int rand,
      bool squareLevelsOnly,
      int levels,
      int player,
      int level,
      int time,
      int executionState,
      string instructions,
      bool hideInput,
      int events,
      int eventedUnits,
      int eventedTerrainTiles) {
    this.rand = rand;
    this.squareLevelsOnly = squareLevelsOnly;
    this.levels = levels;
    this.player = player;
    this.level = level;
    this.time = time;
    this.executionState = executionState;
    this.instructions = instructions;
    this.hideInput = hideInput;
    this.events = events;
    this.eventedUnits = eventedUnits;
    this.eventedTerrainTiles = eventedTerrainTiles;
  }
}

}
