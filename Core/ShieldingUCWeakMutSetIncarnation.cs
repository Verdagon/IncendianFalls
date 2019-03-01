using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class ShieldingUCWeakMutSetIncarnation {
  public readonly SortedSet<int> set;

  public ShieldingUCWeakMutSetIncarnation(SortedSet<int> set) {
    this.set = new SortedSet<int>(set);
  }
}

}
