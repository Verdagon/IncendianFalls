using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class MagmaTTCMutSetIncarnation {
  public readonly SortedSet<int> set;

  public MagmaTTCMutSetIncarnation(SortedSet<int> set) {
    this.set = new SortedSet<int>(set);
  }

  public MagmaTTCMutSetIncarnation Copy() {
    return new MagmaTTCMutSetIncarnation(new SortedSet<int>(set));
  }
}

}
