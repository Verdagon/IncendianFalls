using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class GlaiveStrongMutSetIncarnation {
  public readonly SortedSet<int> set;

  public GlaiveStrongMutSetIncarnation(SortedSet<int> set) {
    this.set = new SortedSet<int>(set);
  }

  public GlaiveStrongMutSetIncarnation Copy() {
    return new GlaiveStrongMutSetIncarnation(new SortedSet<int>(set));
  }
}

}
