using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class WanderAICapabilityUCMutSetIncarnation {
  public readonly SortedSet<int> elements;

  public WanderAICapabilityUCMutSetIncarnation(SortedSet<int> elements) {
    this.elements = new SortedSet<int>(elements);
  }

  public WanderAICapabilityUCMutSetIncarnation Copy() {
    return new WanderAICapabilityUCMutSetIncarnation(new SortedSet<int>(elements));
  }
}

}
