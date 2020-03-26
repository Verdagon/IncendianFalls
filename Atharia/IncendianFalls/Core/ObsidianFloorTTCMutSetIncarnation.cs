using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class ObsidianFloorTTCMutSetIncarnation {
  public readonly SortedSet<int> set;

  public ObsidianFloorTTCMutSetIncarnation(SortedSet<int> set) {
    this.set = new SortedSet<int>(set);
  }

  public ObsidianFloorTTCMutSetIncarnation Copy() {
    return new ObsidianFloorTTCMutSetIncarnation(new SortedSet<int>(set));
  }
}

}
