using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class FireBombImpulseStrongMutSetIncarnation {
  public readonly SortedSet<int> elements;

  public FireBombImpulseStrongMutSetIncarnation(SortedSet<int> elements) {
    this.elements = new SortedSet<int>(elements);
  }

  public FireBombImpulseStrongMutSetIncarnation Copy() {
    return new FireBombImpulseStrongMutSetIncarnation(new SortedSet<int>(elements));
  }
}

}
