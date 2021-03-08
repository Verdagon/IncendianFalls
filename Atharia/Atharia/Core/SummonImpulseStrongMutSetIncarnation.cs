using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class SummonImpulseStrongMutSetIncarnation {
  public readonly SortedSet<int> elements;

  public SummonImpulseStrongMutSetIncarnation(SortedSet<int> elements) {
    this.elements = new SortedSet<int>(elements);
  }

  public SummonImpulseStrongMutSetIncarnation Copy() {
    return new SummonImpulseStrongMutSetIncarnation(new SortedSet<int>(elements));
  }
}

}
