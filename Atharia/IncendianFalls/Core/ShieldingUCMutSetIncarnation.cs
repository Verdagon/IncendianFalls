using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class ShieldingUCMutSetIncarnation {
  public readonly SortedSet<int> set;

  public ShieldingUCMutSetIncarnation(SortedSet<int> set) {
    this.set = new SortedSet<int>(set);
  }
}

}
