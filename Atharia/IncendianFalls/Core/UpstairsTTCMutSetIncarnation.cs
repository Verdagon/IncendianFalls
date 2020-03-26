using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class UpStairsTTCMutSetIncarnation {
  public readonly SortedSet<int> set;

  public UpStairsTTCMutSetIncarnation(SortedSet<int> set) {
    this.set = new SortedSet<int>(set);
  }

  public UpStairsTTCMutSetIncarnation Copy() {
    return new UpStairsTTCMutSetIncarnation(new SortedSet<int>(set));
  }
}

}
