using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class DoomedUCMutSetIncarnation {
  public readonly SortedSet<int> set;

  public DoomedUCMutSetIncarnation(SortedSet<int> set) {
    this.set = new SortedSet<int>(set);
  }
}

}
