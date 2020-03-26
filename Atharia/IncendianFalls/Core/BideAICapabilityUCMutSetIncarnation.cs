using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class BideAICapabilityUCMutSetIncarnation {
  public readonly SortedSet<int> set;

  public BideAICapabilityUCMutSetIncarnation(SortedSet<int> set) {
    this.set = new SortedSet<int>(set);
  }

  public BideAICapabilityUCMutSetIncarnation Copy() {
    return new BideAICapabilityUCMutSetIncarnation(new SortedSet<int>(set));
  }
}

}
