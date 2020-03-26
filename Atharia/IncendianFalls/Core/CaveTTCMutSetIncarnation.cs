using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class CaveTTCMutSetIncarnation {
  public readonly SortedSet<int> set;

  public CaveTTCMutSetIncarnation(SortedSet<int> set) {
    this.set = new SortedSet<int>(set);
  }

  public CaveTTCMutSetIncarnation Copy() {
    return new CaveTTCMutSetIncarnation(new SortedSet<int>(set));
  }
}

}
