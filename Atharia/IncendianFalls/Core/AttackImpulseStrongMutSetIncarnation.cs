using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class AttackImpulseStrongMutSetIncarnation {
  public readonly SortedSet<int> set;

  public AttackImpulseStrongMutSetIncarnation(SortedSet<int> set) {
    this.set = new SortedSet<int>(set);
  }

  public AttackImpulseStrongMutSetIncarnation Copy() {
    return new AttackImpulseStrongMutSetIncarnation(new SortedSet<int>(set));
  }
}

}
