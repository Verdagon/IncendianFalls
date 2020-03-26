using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class AttackAICapabilityUCMutSetIncarnation {
  public readonly SortedSet<int> set;

  public AttackAICapabilityUCMutSetIncarnation(SortedSet<int> set) {
    this.set = new SortedSet<int>(set);
  }

  public AttackAICapabilityUCMutSetIncarnation Copy() {
    return new AttackAICapabilityUCMutSetIncarnation(new SortedSet<int>(set));
  }
}

}
