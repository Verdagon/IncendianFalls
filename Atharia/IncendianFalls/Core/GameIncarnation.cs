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
  public  int overlay;
  public GameIncarnation(
      int rand,
      bool squareLevelsOnly,
      int levels,
      int player,
      int level,
      int time,
      int executionState,
      int overlay) {
    this.rand = rand;
    this.squareLevelsOnly = squareLevelsOnly;
    this.levels = levels;
    this.player = player;
    this.level = level;
    this.time = time;
    this.executionState = executionState;
    this.overlay = overlay;
  }
}

}
