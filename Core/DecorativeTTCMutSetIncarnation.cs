using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class DecorativeTTCMutSetIncarnation {
  public readonly SortedSet<int> set;

  public DecorativeTTCMutSetIncarnation(SortedSet<int> set) {
    this.set = new SortedSet<int>(set);
  }
}

}