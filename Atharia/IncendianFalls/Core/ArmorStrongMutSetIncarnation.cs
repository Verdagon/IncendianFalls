using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class ArmorStrongMutSetIncarnation {
  public readonly SortedSet<int> set;

  public ArmorStrongMutSetIncarnation(SortedSet<int> set) {
    this.set = new SortedSet<int>(set);
  }
}

}
