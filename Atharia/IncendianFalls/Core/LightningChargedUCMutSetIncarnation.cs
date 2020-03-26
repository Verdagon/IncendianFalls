using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class LightningChargedUCMutSetIncarnation {
  public readonly SortedSet<int> set;

  public LightningChargedUCMutSetIncarnation(SortedSet<int> set) {
    this.set = new SortedSet<int>(set);
  }

  public LightningChargedUCMutSetIncarnation Copy() {
    return new LightningChargedUCMutSetIncarnation(new SortedSet<int>(set));
  }
}

}
