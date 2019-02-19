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
  public int GetDeterministicHashCode() {
    int s = 0;
    s = s * 37 + rand.GetDeterministicHashCode();
    s = s * 37 + squareLevelsOnly.GetDeterministicHashCode();
    s = s * 37 + levels.GetDeterministicHashCode();
    s = s * 37 + player.GetDeterministicHashCode();
    s = s * 37 + level.GetDeterministicHashCode();
    s = s * 37 + time.GetDeterministicHashCode();
    s = s * 37 + executionState.GetDeterministicHashCode();
    return s;
  }
}

}
