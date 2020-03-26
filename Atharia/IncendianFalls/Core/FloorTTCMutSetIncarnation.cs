using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class FloorTTCMutSetIncarnation {
  public readonly SortedSet<int> set;

  public FloorTTCMutSetIncarnation(SortedSet<int> set) {
    this.set = new SortedSet<int>(set);
  }

  public FloorTTCMutSetIncarnation Copy() {
    return new FloorTTCMutSetIncarnation(new SortedSet<int>(set));
  }
}

}
