using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class AttackAICapabilityUCWeakMutSetIncarnation {
  public readonly SortedSet<int> set;

  public AttackAICapabilityUCWeakMutSetIncarnation(SortedSet<int> set) {
    this.set = new SortedSet<int>(set);
  }

  public AttackAICapabilityUCWeakMutSetIncarnation Copy() {
    return new AttackAICapabilityUCWeakMutSetIncarnation(new SortedSet<int>(set));
  }
}

}
