using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class BaseMovementTimeUCMutSetIncarnation {
  public readonly SortedSet<int> set;

  public BaseMovementTimeUCMutSetIncarnation(SortedSet<int> set) {
    this.set = new SortedSet<int>(set);
  }
}

}
