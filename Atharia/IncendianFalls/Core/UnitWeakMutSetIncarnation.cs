using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class UnitWeakMutSetIncarnation {
  public readonly SortedSet<int> set;

  public UnitWeakMutSetIncarnation(SortedSet<int> set) {
    this.set = new SortedSet<int>(set);
  }
}

}
