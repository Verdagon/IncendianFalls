using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class BaseSightRangeUCMutSetIncarnation {
  public readonly SortedSet<int> set;

  public BaseSightRangeUCMutSetIncarnation(SortedSet<int> set) {
    this.set = new SortedSet<int>(set);
  }

  public BaseSightRangeUCMutSetIncarnation Copy() {
    return new BaseSightRangeUCMutSetIncarnation(new SortedSet<int>(set));
  }
}

}
