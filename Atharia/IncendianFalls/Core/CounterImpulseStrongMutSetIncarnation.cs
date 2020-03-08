using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class CounterImpulseStrongMutSetIncarnation {
  public readonly SortedSet<int> set;

  public CounterImpulseStrongMutSetIncarnation(SortedSet<int> set) {
    this.set = new SortedSet<int>(set);
  }
}

}
