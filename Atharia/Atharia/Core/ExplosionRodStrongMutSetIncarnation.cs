using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class ExplosionRodStrongMutSetIncarnation {
  public readonly SortedSet<int> elements;

  public ExplosionRodStrongMutSetIncarnation(SortedSet<int> elements) {
    this.elements = new SortedSet<int>(elements);
  }

  public ExplosionRodStrongMutSetIncarnation Copy() {
    return new ExplosionRodStrongMutSetIncarnation(new SortedSet<int>(elements));
  }
}

}
