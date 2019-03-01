using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class KillDirectiveUCMutSetIncarnation {
  public readonly SortedSet<int> set;

  public KillDirectiveUCMutSetIncarnation(SortedSet<int> set) {
    this.set = new SortedSet<int>(set);
  }
}

}
