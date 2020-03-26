using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class CliffLandingTTCMutSetIncarnation {
  public readonly SortedSet<int> set;

  public CliffLandingTTCMutSetIncarnation(SortedSet<int> set) {
    this.set = new SortedSet<int>(set);
  }

  public CliffLandingTTCMutSetIncarnation Copy() {
    return new CliffLandingTTCMutSetIncarnation(new SortedSet<int>(set));
  }
}

}
