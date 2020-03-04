using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class MarkerTTCMutSetIncarnation {
  public readonly SortedSet<int> set;

  public MarkerTTCMutSetIncarnation(SortedSet<int> set) {
    this.set = new SortedSet<int>(set);
  }
}

}
