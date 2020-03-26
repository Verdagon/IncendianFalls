using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class SpeedRingMutSetIncarnation {
  public readonly SortedSet<int> set;

  public SpeedRingMutSetIncarnation(SortedSet<int> set) {
    this.set = new SortedSet<int>(set);
  }

  public SpeedRingMutSetIncarnation Copy() {
    return new SpeedRingMutSetIncarnation(new SortedSet<int>(set));
  }
}

}
