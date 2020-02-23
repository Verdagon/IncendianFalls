using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class DownstairsTTCMutSetIncarnation {
  public readonly SortedSet<int> set;

  public DownstairsTTCMutSetIncarnation(SortedSet<int> set) {
    this.set = new SortedSet<int>(set);
  }
}

}
