using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class InertiaRingMutSetIncarnation {
  public readonly SortedSet<int> set;

  public InertiaRingMutSetIncarnation(SortedSet<int> set) {
    this.set = new SortedSet<int>(set);
  }
}

}
