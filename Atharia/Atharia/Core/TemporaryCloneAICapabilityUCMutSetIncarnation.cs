using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class TemporaryCloneAICapabilityUCMutSetIncarnation {
  public readonly SortedSet<int> elements;

  public TemporaryCloneAICapabilityUCMutSetIncarnation(SortedSet<int> elements) {
    this.elements = new SortedSet<int>(elements);
  }

  public TemporaryCloneAICapabilityUCMutSetIncarnation Copy() {
    return new TemporaryCloneAICapabilityUCMutSetIncarnation(new SortedSet<int>(elements));
  }
}

}
