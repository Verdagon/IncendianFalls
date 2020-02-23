using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class WallTTCMutSetIncarnation {
  public readonly SortedSet<int> set;

  public WallTTCMutSetIncarnation(SortedSet<int> set) {
    this.set = new SortedSet<int>(set);
  }
}

}
