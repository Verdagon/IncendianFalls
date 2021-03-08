using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class BlastRodStrongMutSetIncarnation {
  public readonly SortedSet<int> elements;

  public BlastRodStrongMutSetIncarnation(SortedSet<int> elements) {
    this.elements = new SortedSet<int>(elements);
  }

  public BlastRodStrongMutSetIncarnation Copy() {
    return new BlastRodStrongMutSetIncarnation(new SortedSet<int>(elements));
  }
}

}
