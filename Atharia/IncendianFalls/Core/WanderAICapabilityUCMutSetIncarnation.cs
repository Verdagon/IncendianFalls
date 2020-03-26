using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class WanderAICapabilityUCMutSetIncarnation {
  public readonly SortedSet<int> set;

  public WanderAICapabilityUCMutSetIncarnation(SortedSet<int> set) {
    this.set = new SortedSet<int>(set);
  }

  public WanderAICapabilityUCMutSetIncarnation Copy() {
    return new WanderAICapabilityUCMutSetIncarnation(new SortedSet<int>(set));
  }
}

}
