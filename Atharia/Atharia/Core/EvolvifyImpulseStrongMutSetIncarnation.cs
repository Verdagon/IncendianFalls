using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class EvolvifyImpulseStrongMutSetIncarnation {
  public readonly SortedSet<int> elements;

  public EvolvifyImpulseStrongMutSetIncarnation(SortedSet<int> elements) {
    this.elements = new SortedSet<int>(elements);
  }

  public EvolvifyImpulseStrongMutSetIncarnation Copy() {
    return new EvolvifyImpulseStrongMutSetIncarnation(new SortedSet<int>(elements));
  }
}

}
