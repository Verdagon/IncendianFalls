using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class PursueImpulseStrongMutSetIncarnation {
  public readonly SortedSet<int> set;

  public PursueImpulseStrongMutSetIncarnation(SortedSet<int> set) {
    this.set = new SortedSet<int>(set);
  }

  public PursueImpulseStrongMutSetIncarnation Copy() {
    return new PursueImpulseStrongMutSetIncarnation(new SortedSet<int>(set));
  }
}

}
