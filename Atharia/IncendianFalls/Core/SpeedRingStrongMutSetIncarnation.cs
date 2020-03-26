using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class SpeedRingStrongMutSetIncarnation {
  public readonly SortedSet<int> set;

  public SpeedRingStrongMutSetIncarnation(SortedSet<int> set) {
    this.set = new SortedSet<int>(set);
  }

  public SpeedRingStrongMutSetIncarnation Copy() {
    return new SpeedRingStrongMutSetIncarnation(new SortedSet<int>(set));
  }
}

}
