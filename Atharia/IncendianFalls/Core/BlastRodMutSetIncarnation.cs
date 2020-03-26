using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class BlastRodMutSetIncarnation {
  public readonly SortedSet<int> set;

  public BlastRodMutSetIncarnation(SortedSet<int> set) {
    this.set = new SortedSet<int>(set);
  }

  public BlastRodMutSetIncarnation Copy() {
    return new BlastRodMutSetIncarnation(new SortedSet<int>(set));
  }
}

}
