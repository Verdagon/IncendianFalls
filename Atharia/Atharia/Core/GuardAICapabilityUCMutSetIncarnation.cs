using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class GuardAICapabilityUCMutSetIncarnation {
  public readonly SortedSet<int> elements;

  public GuardAICapabilityUCMutSetIncarnation(SortedSet<int> elements) {
    this.elements = new SortedSet<int>(elements);
  }

  public GuardAICapabilityUCMutSetIncarnation Copy() {
    return new GuardAICapabilityUCMutSetIncarnation(new SortedSet<int>(elements));
  }
}

}
