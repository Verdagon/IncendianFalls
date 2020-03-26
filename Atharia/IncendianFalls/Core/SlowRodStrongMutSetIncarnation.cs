using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class SlowRodStrongMutSetIncarnation {
  public readonly SortedSet<int> set;

  public SlowRodStrongMutSetIncarnation(SortedSet<int> set) {
    this.set = new SortedSet<int>(set);
  }

  public SlowRodStrongMutSetIncarnation Copy() {
    return new SlowRodStrongMutSetIncarnation(new SortedSet<int>(set));
  }
}

}
