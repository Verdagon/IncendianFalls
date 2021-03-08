using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class NoImpulseStrongMutSetIncarnation {
  public readonly SortedSet<int> elements;

  public NoImpulseStrongMutSetIncarnation(SortedSet<int> elements) {
    this.elements = new SortedSet<int>(elements);
  }

  public NoImpulseStrongMutSetIncarnation Copy() {
    return new NoImpulseStrongMutSetIncarnation(new SortedSet<int>(elements));
  }
}

}
