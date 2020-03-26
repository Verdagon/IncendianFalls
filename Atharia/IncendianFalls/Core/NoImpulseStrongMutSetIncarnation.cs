using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class NoImpulseStrongMutSetIncarnation {
  public readonly SortedSet<int> set;

  public NoImpulseStrongMutSetIncarnation(SortedSet<int> set) {
    this.set = new SortedSet<int>(set);
  }

  public NoImpulseStrongMutSetIncarnation Copy() {
    return new NoImpulseStrongMutSetIncarnation(new SortedSet<int>(set));
  }
}

}
