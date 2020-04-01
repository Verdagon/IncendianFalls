using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class EvaporateImpulseStrongMutSetIncarnation {
  public readonly SortedSet<int> elements;

  public EvaporateImpulseStrongMutSetIncarnation(SortedSet<int> elements) {
    this.elements = new SortedSet<int>(elements);
  }

  public EvaporateImpulseStrongMutSetIncarnation Copy() {
    return new EvaporateImpulseStrongMutSetIncarnation(new SortedSet<int>(elements));
  }
}

}
