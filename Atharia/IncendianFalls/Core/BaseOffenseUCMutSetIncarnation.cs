using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class BaseOffenseUCMutSetIncarnation {
  public readonly SortedSet<int> set;

  public BaseOffenseUCMutSetIncarnation(SortedSet<int> set) {
    this.set = new SortedSet<int>(set);
  }

  public BaseOffenseUCMutSetIncarnation Copy() {
    return new BaseOffenseUCMutSetIncarnation(new SortedSet<int>(set));
  }
}

}
