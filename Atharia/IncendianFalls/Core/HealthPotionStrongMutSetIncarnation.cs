using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class HealthPotionStrongMutSetIncarnation {
  public readonly SortedSet<int> set;

  public HealthPotionStrongMutSetIncarnation(SortedSet<int> set) {
    this.set = new SortedSet<int>(set);
  }

  public HealthPotionStrongMutSetIncarnation Copy() {
    return new HealthPotionStrongMutSetIncarnation(new SortedSet<int>(set));
  }
}

}
