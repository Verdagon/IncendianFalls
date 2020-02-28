using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class ItemTTCMutSetIncarnation {
  public readonly SortedSet<int> set;

  public ItemTTCMutSetIncarnation(SortedSet<int> set) {
    this.set = new SortedSet<int>(set);
  }
}

}
