using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class DownStaircaseTTCMutSetIncarnation {
  public readonly SortedSet<int> set;

  public DownStaircaseTTCMutSetIncarnation(SortedSet<int> set) {
    this.set = new SortedSet<int>(set);
  }
}

}
