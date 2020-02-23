using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class BloodTTCMutSetIncarnation {
  public readonly SortedSet<int> set;

  public BloodTTCMutSetIncarnation(SortedSet<int> set) {
    this.set = new SortedSet<int>(set);
  }
}

}
