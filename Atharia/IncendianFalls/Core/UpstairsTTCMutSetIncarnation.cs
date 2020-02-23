using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class UpstairsTTCMutSetIncarnation {
  public readonly SortedSet<int> set;

  public UpstairsTTCMutSetIncarnation(SortedSet<int> set) {
    this.set = new SortedSet<int>(set);
  }
}

}
