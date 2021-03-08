using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class SummonAICapabilityUCMutSetIncarnation {
  public readonly SortedSet<int> elements;

  public SummonAICapabilityUCMutSetIncarnation(SortedSet<int> elements) {
    this.elements = new SortedSet<int>(elements);
  }

  public SummonAICapabilityUCMutSetIncarnation Copy() {
    return new SummonAICapabilityUCMutSetIncarnation(new SortedSet<int>(elements));
  }
}

}
