using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class ObsidianTTCMutSetIncarnation {
  public readonly SortedSet<int> set;

  public ObsidianTTCMutSetIncarnation(SortedSet<int> set) {
    this.set = new SortedSet<int>(set);
  }

  public ObsidianTTCMutSetIncarnation Copy() {
    return new ObsidianTTCMutSetIncarnation(new SortedSet<int>(set));
  }
}

}
