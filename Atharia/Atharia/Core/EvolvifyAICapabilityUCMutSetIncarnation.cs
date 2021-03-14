using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class EvolvifyAICapabilityUCMutSetIncarnation {
  public readonly SortedSet<int> elements;

  public EvolvifyAICapabilityUCMutSetIncarnation(SortedSet<int> elements) {
    this.elements = new SortedSet<int>(elements);
  }

  public EvolvifyAICapabilityUCMutSetIncarnation Copy() {
    return new EvolvifyAICapabilityUCMutSetIncarnation(new SortedSet<int>(elements));
  }
}

}
