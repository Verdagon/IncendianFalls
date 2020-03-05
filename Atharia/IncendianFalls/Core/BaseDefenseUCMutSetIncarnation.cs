using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class BaseDefenseUCMutSetIncarnation {
  public readonly SortedSet<int> set;

  public BaseDefenseUCMutSetIncarnation(SortedSet<int> set) {
    this.set = new SortedSet<int>(set);
  }
}

}
