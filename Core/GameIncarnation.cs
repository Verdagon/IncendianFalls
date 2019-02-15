using System;
using System.Collections.Generic;

namespace Atharia.Model {
public class GameIncarnation {
  public int rand;
  public bool squareLevelsOnly;
  public int levels;
  public int player;
  public int level;
  public int time;
  public int executionState;
  public GameIncarnation(
      int rand,
      bool squareLevelsOnly,
      int levels,
      int player,
      int level,
      int time,
      int executionState) {
    this.rand = rand;
    this.squareLevelsOnly = squareLevelsOnly;
    this.levels = levels;
    this.player = player;
    this.level = level;
    this.time = time;
    this.executionState = executionState;
  }
}

}
