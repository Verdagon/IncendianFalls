using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class TimeCloneAICapabilityUCWeakMutSetIncarnation {
  public readonly SortedSet<int> set;

  public TimeCloneAICapabilityUCWeakMutSetIncarnation(SortedSet<int> set) {
    this.set = new SortedSet<int>(set);
  }

  public TimeCloneAICapabilityUCWeakMutSetIncarnation Copy() {
    return new TimeCloneAICapabilityUCWeakMutSetIncarnation(new SortedSet<int>(set));
  }
}

}
