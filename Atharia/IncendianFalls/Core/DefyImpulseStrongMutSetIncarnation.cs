using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class DefyImpulseStrongMutSetIncarnation {
  public readonly SortedSet<int> elements;

  public DefyImpulseStrongMutSetIncarnation(SortedSet<int> elements) {
    this.elements = new SortedSet<int>(elements);
  }

  public DefyImpulseStrongMutSetIncarnation Copy() {
    return new DefyImpulseStrongMutSetIncarnation(new SortedSet<int>(elements));
  }
}

}
