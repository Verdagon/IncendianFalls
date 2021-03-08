using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class TemporaryCloneImpulseStrongMutSetIncarnation {
  public readonly SortedSet<int> elements;

  public TemporaryCloneImpulseStrongMutSetIncarnation(SortedSet<int> elements) {
    this.elements = new SortedSet<int>(elements);
  }

  public TemporaryCloneImpulseStrongMutSetIncarnation Copy() {
    return new TemporaryCloneImpulseStrongMutSetIncarnation(new SortedSet<int>(elements));
  }
}

}
