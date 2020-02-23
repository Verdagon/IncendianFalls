using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class RavaNestTTCMutSetIncarnation {
  public readonly SortedSet<int> set;

  public RavaNestTTCMutSetIncarnation(SortedSet<int> set) {
    this.set = new SortedSet<int>(set);
  }
}

}
