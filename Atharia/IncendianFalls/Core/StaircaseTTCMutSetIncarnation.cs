using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class StaircaseTTCMutSetIncarnation {
  public readonly SortedSet<int> set;

  public StaircaseTTCMutSetIncarnation(SortedSet<int> set) {
    this.set = new SortedSet<int>(set);
  }
}

}
