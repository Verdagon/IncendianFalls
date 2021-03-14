using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class ExplosionRodMutSetIncarnation {
  public readonly SortedSet<int> elements;

  public ExplosionRodMutSetIncarnation(SortedSet<int> elements) {
    this.elements = new SortedSet<int>(elements);
  }

  public ExplosionRodMutSetIncarnation Copy() {
    return new ExplosionRodMutSetIncarnation(new SortedSet<int>(elements));
  }
}

}
