using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class MiredUCWeakMutSetIncarnation {
  public readonly SortedSet<int> set;

  public MiredUCWeakMutSetIncarnation(SortedSet<int> set) {
    this.set = new SortedSet<int>(set);
  }

  public MiredUCWeakMutSetIncarnation Copy() {
    return new MiredUCWeakMutSetIncarnation(new SortedSet<int>(set));
  }
}

}
