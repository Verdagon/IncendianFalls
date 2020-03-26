using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class GuardAICapabilityUCMutSetIncarnation {
  public readonly SortedSet<int> set;

  public GuardAICapabilityUCMutSetIncarnation(SortedSet<int> set) {
    this.set = new SortedSet<int>(set);
  }

  public GuardAICapabilityUCMutSetIncarnation Copy() {
    return new GuardAICapabilityUCMutSetIncarnation(new SortedSet<int>(set));
  }
}

}
