using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class WarperTTCMutSetIncarnation {
  public readonly SortedSet<int> set;

  public WarperTTCMutSetIncarnation(SortedSet<int> set) {
    this.set = new SortedSet<int>(set);
  }
}

}
