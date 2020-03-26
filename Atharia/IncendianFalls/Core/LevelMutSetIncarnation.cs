using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class LevelMutSetIncarnation {
  public readonly SortedSet<int> set;

  public LevelMutSetIncarnation(SortedSet<int> set) {
    this.set = new SortedSet<int>(set);
  }

  public LevelMutSetIncarnation Copy() {
    return new LevelMutSetIncarnation(new SortedSet<int>(set));
  }
}

}
