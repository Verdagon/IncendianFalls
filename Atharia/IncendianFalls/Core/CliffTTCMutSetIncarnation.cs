using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class CliffTTCMutSetIncarnation {
  public readonly SortedSet<int> set;

  public CliffTTCMutSetIncarnation(SortedSet<int> set) {
    this.set = new SortedSet<int>(set);
  }
}

}
