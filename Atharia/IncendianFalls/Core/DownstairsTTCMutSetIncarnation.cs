using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class DownStairsTTCMutSetIncarnation {
  public readonly SortedSet<int> set;

  public DownStairsTTCMutSetIncarnation(SortedSet<int> set) {
    this.set = new SortedSet<int>(set);
  }

  public DownStairsTTCMutSetIncarnation Copy() {
    return new DownStairsTTCMutSetIncarnation(new SortedSet<int>(set));
  }
}

}
