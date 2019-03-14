using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class GameIncarnation {
  public readonly int rand;
  public readonly bool squareLevelsOnly;
  public readonly bool gauntletMode;
  public readonly int levels;
  public  int player;
  public  int level;
  public  int time;
  public readonly int executionState;
  public GameIncarnation(
      int rand,
      bool squareLevelsOnly,
      bool gauntletMode,
      int levels,
      int player,
      int level,
      int time,
      int executionState) {
    this.rand = rand;
    this.squareLevelsOnly = squareLevelsOnly;
    this.gauntletMode = gauntletMode;
    this.levels = levels;
    this.player = player;
    this.level = level;
    this.time = time;
    this.executionState = executionState;
  }
}

}
