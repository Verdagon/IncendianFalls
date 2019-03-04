using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class UpStaircaseTTCMutSetIncarnation {
  public readonly SortedSet<int> set;

  public UpStaircaseTTCMutSetIncarnation(SortedSet<int> set) {
    this.set = new SortedSet<int>(set);
  }
}

}
