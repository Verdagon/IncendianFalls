using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class MoveDirectiveUCMutSetIncarnation {
  public readonly SortedSet<int> set;

  public MoveDirectiveUCMutSetIncarnation(SortedSet<int> set) {
    this.set = new SortedSet<int>(set);
  }
}

}
