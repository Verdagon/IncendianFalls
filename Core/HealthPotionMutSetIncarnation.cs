using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class HealthPotionMutSetIncarnation {
  public readonly SortedSet<int> set;

  public HealthPotionMutSetIncarnation(SortedSet<int> set) {
    this.set = new SortedSet<int>(set);
  }
}

}
