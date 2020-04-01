using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class BlastRodMutSetIncarnation {
  public readonly SortedSet<int> elements;

  public BlastRodMutSetIncarnation(SortedSet<int> elements) {
    this.elements = new SortedSet<int>(elements);
  }

  public BlastRodMutSetIncarnation Copy() {
    return new BlastRodMutSetIncarnation(new SortedSet<int>(elements));
  }
}

}
