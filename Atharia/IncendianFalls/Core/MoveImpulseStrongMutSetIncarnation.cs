using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class MoveImpulseStrongMutSetIncarnation {
  public readonly SortedSet<int> set;

  public MoveImpulseStrongMutSetIncarnation(SortedSet<int> set) {
    this.set = new SortedSet<int>(set);
  }
}

}
