using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class TimeCloneAICapabilityUCMutSetIncarnation {
  public readonly SortedSet<int> elements;

  public TimeCloneAICapabilityUCMutSetIncarnation(SortedSet<int> elements) {
    this.elements = new SortedSet<int>(elements);
  }

  public TimeCloneAICapabilityUCMutSetIncarnation Copy() {
    return new TimeCloneAICapabilityUCMutSetIncarnation(new SortedSet<int>(elements));
  }
}

}
