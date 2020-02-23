using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class ArmorMutSetIncarnation {
  public readonly SortedSet<int> set;

  public ArmorMutSetIncarnation(SortedSet<int> set) {
    this.set = new SortedSet<int>(set);
  }
}

}
