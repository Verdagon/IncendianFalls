using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class AttackImpulseStrongMutSetIncarnation {
  public readonly SortedSet<int> elements;

  public AttackImpulseStrongMutSetIncarnation(SortedSet<int> elements) {
    this.elements = new SortedSet<int>(elements);
  }

  public AttackImpulseStrongMutSetIncarnation Copy() {
    return new AttackImpulseStrongMutSetIncarnation(new SortedSet<int>(elements));
  }
}

}
