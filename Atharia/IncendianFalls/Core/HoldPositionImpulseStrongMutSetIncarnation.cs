using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class HoldPositionImpulseStrongMutSetIncarnation {
  public readonly SortedSet<int> set;

  public HoldPositionImpulseStrongMutSetIncarnation(SortedSet<int> set) {
    this.set = new SortedSet<int>(set);
  }

  public HoldPositionImpulseStrongMutSetIncarnation Copy() {
    return new HoldPositionImpulseStrongMutSetIncarnation(new SortedSet<int>(set));
  }
}

}
