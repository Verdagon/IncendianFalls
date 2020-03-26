using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class SlowRodMutSetIncarnation {
  public readonly SortedSet<int> set;

  public SlowRodMutSetIncarnation(SortedSet<int> set) {
    this.set = new SortedSet<int>(set);
  }

  public SlowRodMutSetIncarnation Copy() {
    return new SlowRodMutSetIncarnation(new SortedSet<int>(set));
  }
}

}
