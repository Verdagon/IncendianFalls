using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class CaveWallTTCMutSetIncarnation {
  public readonly SortedSet<int> set;

  public CaveWallTTCMutSetIncarnation(SortedSet<int> set) {
    this.set = new SortedSet<int>(set);
  }

  public CaveWallTTCMutSetIncarnation Copy() {
    return new CaveWallTTCMutSetIncarnation(new SortedSet<int>(set));
  }
}

}
