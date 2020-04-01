using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class BideAICapabilityUCMutSetIncarnation {
  public readonly SortedSet<int> elements;

  public BideAICapabilityUCMutSetIncarnation(SortedSet<int> elements) {
    this.elements = new SortedSet<int>(elements);
  }

  public BideAICapabilityUCMutSetIncarnation Copy() {
    return new BideAICapabilityUCMutSetIncarnation(new SortedSet<int>(elements));
  }
}

}
