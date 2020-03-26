using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class DefyingUCWeakMutSetIncarnation {
  public readonly SortedSet<int> set;

  public DefyingUCWeakMutSetIncarnation(SortedSet<int> set) {
    this.set = new SortedSet<int>(set);
  }

  public DefyingUCWeakMutSetIncarnation Copy() {
    return new DefyingUCWeakMutSetIncarnation(new SortedSet<int>(set));
  }
}

}
