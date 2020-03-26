using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class StartBidingImpulseStrongMutSetIncarnation {
  public readonly SortedSet<int> set;

  public StartBidingImpulseStrongMutSetIncarnation(SortedSet<int> set) {
    this.set = new SortedSet<int>(set);
  }

  public StartBidingImpulseStrongMutSetIncarnation Copy() {
    return new StartBidingImpulseStrongMutSetIncarnation(new SortedSet<int>(set));
  }
}

}
