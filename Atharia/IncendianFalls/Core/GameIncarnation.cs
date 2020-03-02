using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class GameIncarnation {
  public readonly int rand;
  public readonly bool squareLevelsOnly;
  public readonly int levels;
  public  int player;
  public readonly int events;
  public  int level;
  public  int time;
  public readonly int executionState;
  public GameIncarnation(
      int rand,
      bool squareLevelsOnly,
      int levels,
      int player,
      int events,
      int level,
      int time,
      int executionState) {
    this.rand = rand;
    this.squareLevelsOnly = squareLevelsOnly;
    this.levels = levels;
    this.player = player;
    this.events = events;
    this.level = level;
    this.time = time;
    this.executionState = executionState;
  }
}

}
