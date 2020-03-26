using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class TemporaryCloneAICapabilityUCMutSetIncarnation {
  public readonly SortedSet<int> set;

  public TemporaryCloneAICapabilityUCMutSetIncarnation(SortedSet<int> set) {
    this.set = new SortedSet<int>(set);
  }

  public TemporaryCloneAICapabilityUCMutSetIncarnation Copy() {
    return new TemporaryCloneAICapabilityUCMutSetIncarnation(new SortedSet<int>(set));
  }
}

}
