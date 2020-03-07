using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class MiredUCMutSetIncarnation {
  public readonly SortedSet<int> set;

  public MiredUCMutSetIncarnation(SortedSet<int> set) {
    this.set = new SortedSet<int>(set);
  }
}

}
