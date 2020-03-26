using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class KamikazeJumpImpulseStrongMutSetIncarnation {
  public readonly SortedSet<int> set;

  public KamikazeJumpImpulseStrongMutSetIncarnation(SortedSet<int> set) {
    this.set = new SortedSet<int>(set);
  }

  public KamikazeJumpImpulseStrongMutSetIncarnation Copy() {
    return new KamikazeJumpImpulseStrongMutSetIncarnation(new SortedSet<int>(set));
  }
}

}
