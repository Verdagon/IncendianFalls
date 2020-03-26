using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class BlastRodStrongMutSetIncarnation {
  public readonly SortedSet<int> set;

  public BlastRodStrongMutSetIncarnation(SortedSet<int> set) {
    this.set = new SortedSet<int>(set);
  }

  public BlastRodStrongMutSetIncarnation Copy() {
    return new BlastRodStrongMutSetIncarnation(new SortedSet<int>(set));
  }
}

}
