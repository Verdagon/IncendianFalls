using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class FireImpulseStrongMutSetIncarnation {
  public readonly SortedSet<int> set;

  public FireImpulseStrongMutSetIncarnation(SortedSet<int> set) {
    this.set = new SortedSet<int>(set);
  }
}

}
