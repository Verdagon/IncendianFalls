using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class TimeAnchorTTCMutSetIncarnation {
  public readonly SortedSet<int> set;

  public TimeAnchorTTCMutSetIncarnation(SortedSet<int> set) {
    this.set = new SortedSet<int>(set);
  }
}

}
