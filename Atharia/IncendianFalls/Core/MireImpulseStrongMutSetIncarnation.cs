using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class MireImpulseStrongMutSetIncarnation {
  public readonly SortedSet<int> set;

  public MireImpulseStrongMutSetIncarnation(SortedSet<int> set) {
    this.set = new SortedSet<int>(set);
  }

  public MireImpulseStrongMutSetIncarnation Copy() {
    return new MireImpulseStrongMutSetIncarnation(new SortedSet<int>(set));
  }
}

}
