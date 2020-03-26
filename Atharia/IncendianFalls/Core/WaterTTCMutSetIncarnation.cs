using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class WaterTTCMutSetIncarnation {
  public readonly SortedSet<int> set;

  public WaterTTCMutSetIncarnation(SortedSet<int> set) {
    this.set = new SortedSet<int>(set);
  }

  public WaterTTCMutSetIncarnation Copy() {
    return new WaterTTCMutSetIncarnation(new SortedSet<int>(set));
  }
}

}
