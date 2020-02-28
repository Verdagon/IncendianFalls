using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class ManaPotionStrongMutSetIncarnation {
  public readonly SortedSet<int> set;

  public ManaPotionStrongMutSetIncarnation(SortedSet<int> set) {
    this.set = new SortedSet<int>(set);
  }
}

}
