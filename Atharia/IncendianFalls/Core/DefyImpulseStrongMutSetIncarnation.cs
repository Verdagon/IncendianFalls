using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class DefyImpulseStrongMutSetIncarnation {
  public readonly SortedSet<int> set;

  public DefyImpulseStrongMutSetIncarnation(SortedSet<int> set) {
    this.set = new SortedSet<int>(set);
  }

  public DefyImpulseStrongMutSetIncarnation Copy() {
    return new DefyImpulseStrongMutSetIncarnation(new SortedSet<int>(set));
  }
}

}
