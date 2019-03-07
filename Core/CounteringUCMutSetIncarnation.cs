using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class CounteringUCMutSetIncarnation {
  public readonly SortedSet<int> set;

  public CounteringUCMutSetIncarnation(SortedSet<int> set) {
    this.set = new SortedSet<int>(set);
  }
}

}
