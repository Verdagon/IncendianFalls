using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class TimeCloneAICapabilityUCWeakMutSetIncarnation {
  public readonly SortedSet<int> elements;

  public TimeCloneAICapabilityUCWeakMutSetIncarnation(SortedSet<int> elements) {
    this.elements = new SortedSet<int>(elements);
  }

  public TimeCloneAICapabilityUCWeakMutSetIncarnation Copy() {
    return new TimeCloneAICapabilityUCWeakMutSetIncarnation(new SortedSet<int>(elements));
  }
}

}
