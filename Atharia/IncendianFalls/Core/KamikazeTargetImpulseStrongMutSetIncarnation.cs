using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class KamikazeTargetImpulseStrongMutSetIncarnation {
  public readonly SortedSet<int> set;

  public KamikazeTargetImpulseStrongMutSetIncarnation(SortedSet<int> set) {
    this.set = new SortedSet<int>(set);
  }

  public KamikazeTargetImpulseStrongMutSetIncarnation Copy() {
    return new KamikazeTargetImpulseStrongMutSetIncarnation(new SortedSet<int>(set));
  }
}

}
