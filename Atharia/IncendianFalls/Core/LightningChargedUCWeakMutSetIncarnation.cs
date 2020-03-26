using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class LightningChargedUCWeakMutSetIncarnation {
  public readonly SortedSet<int> set;

  public LightningChargedUCWeakMutSetIncarnation(SortedSet<int> set) {
    this.set = new SortedSet<int>(set);
  }

  public LightningChargedUCWeakMutSetIncarnation Copy() {
    return new LightningChargedUCWeakMutSetIncarnation(new SortedSet<int>(set));
  }
}

}
