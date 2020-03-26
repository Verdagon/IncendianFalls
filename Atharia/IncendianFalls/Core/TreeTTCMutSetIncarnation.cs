using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class TreeTTCMutSetIncarnation {
  public readonly SortedSet<int> set;

  public TreeTTCMutSetIncarnation(SortedSet<int> set) {
    this.set = new SortedSet<int>(set);
  }

  public TreeTTCMutSetIncarnation Copy() {
    return new TreeTTCMutSetIncarnation(new SortedSet<int>(set));
  }
}

}
