using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class UnleashBideImpulseStrongMutSetIncarnation {
  public readonly SortedSet<int> elements;

  public UnleashBideImpulseStrongMutSetIncarnation(SortedSet<int> elements) {
    this.elements = new SortedSet<int>(elements);
  }

  public UnleashBideImpulseStrongMutSetIncarnation Copy() {
    return new UnleashBideImpulseStrongMutSetIncarnation(new SortedSet<int>(elements));
  }
}

}
