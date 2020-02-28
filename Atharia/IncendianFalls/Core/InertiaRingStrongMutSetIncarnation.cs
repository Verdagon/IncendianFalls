using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class InertiaRingStrongMutSetIncarnation {
  public readonly SortedSet<int> set;

  public InertiaRingStrongMutSetIncarnation(SortedSet<int> set) {
    this.set = new SortedSet<int>(set);
  }
}

}
