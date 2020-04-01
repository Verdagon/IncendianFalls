using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class PursueImpulseStrongMutSetIncarnation {
  public readonly SortedSet<int> elements;

  public PursueImpulseStrongMutSetIncarnation(SortedSet<int> elements) {
    this.elements = new SortedSet<int>(elements);
  }

  public PursueImpulseStrongMutSetIncarnation Copy() {
    return new PursueImpulseStrongMutSetIncarnation(new SortedSet<int>(elements));
  }
}

}
