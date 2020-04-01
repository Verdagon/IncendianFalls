using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class KamikazeJumpImpulseStrongMutSetIncarnation {
  public readonly SortedSet<int> elements;

  public KamikazeJumpImpulseStrongMutSetIncarnation(SortedSet<int> elements) {
    this.elements = new SortedSet<int>(elements);
  }

  public KamikazeJumpImpulseStrongMutSetIncarnation Copy() {
    return new KamikazeJumpImpulseStrongMutSetIncarnation(new SortedSet<int>(elements));
  }
}

}
