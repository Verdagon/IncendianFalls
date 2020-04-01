using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class AttackAICapabilityUCWeakMutSetIncarnation {
  public readonly SortedSet<int> elements;

  public AttackAICapabilityUCWeakMutSetIncarnation(SortedSet<int> elements) {
    this.elements = new SortedSet<int>(elements);
  }

  public AttackAICapabilityUCWeakMutSetIncarnation Copy() {
    return new AttackAICapabilityUCWeakMutSetIncarnation(new SortedSet<int>(elements));
  }
}

}
