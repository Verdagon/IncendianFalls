using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class AttackAICapabilityUCMutSetIncarnation {
  public readonly SortedSet<int> elements;

  public AttackAICapabilityUCMutSetIncarnation(SortedSet<int> elements) {
    this.elements = new SortedSet<int>(elements);
  }

  public AttackAICapabilityUCMutSetIncarnation Copy() {
    return new AttackAICapabilityUCMutSetIncarnation(new SortedSet<int>(elements));
  }
}

}
