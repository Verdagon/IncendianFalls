using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class DoomedUCWeakMutSetIncarnation {
  public readonly SortedSet<int> set;

  public DoomedUCWeakMutSetIncarnation(SortedSet<int> set) {
    this.set = new SortedSet<int>(set);
  }
}

}
