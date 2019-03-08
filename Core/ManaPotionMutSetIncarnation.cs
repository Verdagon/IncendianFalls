using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class ManaPotionMutSetIncarnation {
  public readonly SortedSet<int> set;

  public ManaPotionMutSetIncarnation(SortedSet<int> set) {
    this.set = new SortedSet<int>(set);
  }
}

}
