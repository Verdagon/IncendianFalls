using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class CounteringUCWeakMutSetIncarnation {
  public readonly SortedSet<int> set;

  public CounteringUCWeakMutSetIncarnation(SortedSet<int> set) {
    this.set = new SortedSet<int>(set);
  }
}

}
