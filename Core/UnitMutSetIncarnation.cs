using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class UnitMutSetIncarnation {
  public readonly SortedSet<int> set;

  public UnitMutSetIncarnation(SortedSet<int> set) {
    this.set = new SortedSet<int>(set);
  }
}

}
