using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class KamikazeTargetImpulseStrongMutSetIncarnation {
  public readonly SortedSet<int> elements;

  public KamikazeTargetImpulseStrongMutSetIncarnation(SortedSet<int> elements) {
    this.elements = new SortedSet<int>(elements);
  }

  public KamikazeTargetImpulseStrongMutSetIncarnation Copy() {
    return new KamikazeTargetImpulseStrongMutSetIncarnation(new SortedSet<int>(elements));
  }
}

}
