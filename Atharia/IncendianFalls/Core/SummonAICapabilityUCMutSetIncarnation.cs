using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class SummonAICapabilityUCMutSetIncarnation {
  public readonly SortedSet<int> set;

  public SummonAICapabilityUCMutSetIncarnation(SortedSet<int> set) {
    this.set = new SortedSet<int>(set);
  }
}

}
