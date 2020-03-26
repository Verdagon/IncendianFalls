using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class ContinueBidingImpulseStrongMutSetIncarnation {
  public readonly SortedSet<int> set;

  public ContinueBidingImpulseStrongMutSetIncarnation(SortedSet<int> set) {
    this.set = new SortedSet<int>(set);
  }

  public ContinueBidingImpulseStrongMutSetIncarnation Copy() {
    return new ContinueBidingImpulseStrongMutSetIncarnation(new SortedSet<int>(set));
  }
}

}
