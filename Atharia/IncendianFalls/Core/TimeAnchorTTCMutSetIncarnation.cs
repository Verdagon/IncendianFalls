using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class TimeAnchorTTCMutSetIncarnation {
  public readonly SortedSet<int> elements;

  public TimeAnchorTTCMutSetIncarnation(SortedSet<int> elements) {
    this.elements = new SortedSet<int>(elements);
  }

  public TimeAnchorTTCMutSetIncarnation Copy() {
    return new TimeAnchorTTCMutSetIncarnation(new SortedSet<int>(elements));
  }
}

}
