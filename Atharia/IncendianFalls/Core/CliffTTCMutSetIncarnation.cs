using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class CliffTTCMutSetIncarnation {
  public readonly SortedSet<int> elements;

  public CliffTTCMutSetIncarnation(SortedSet<int> elements) {
    this.elements = new SortedSet<int>(elements);
  }

  public CliffTTCMutSetIncarnation Copy() {
    return new CliffTTCMutSetIncarnation(new SortedSet<int>(elements));
  }
}

}
