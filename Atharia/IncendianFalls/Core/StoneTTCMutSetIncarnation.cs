using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class StoneTTCMutSetIncarnation {
  public readonly SortedSet<int> set;

  public StoneTTCMutSetIncarnation(SortedSet<int> set) {
    this.set = new SortedSet<int>(set);
  }

  public StoneTTCMutSetIncarnation Copy() {
    return new StoneTTCMutSetIncarnation(new SortedSet<int>(set));
  }
}

}
