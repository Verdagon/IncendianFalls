using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class FireImpulseStrongMutSetIncarnation {
  public readonly SortedSet<int> elements;

  public FireImpulseStrongMutSetIncarnation(SortedSet<int> elements) {
    this.elements = new SortedSet<int>(elements);
  }

  public FireImpulseStrongMutSetIncarnation Copy() {
    return new FireImpulseStrongMutSetIncarnation(new SortedSet<int>(elements));
  }
}

}
