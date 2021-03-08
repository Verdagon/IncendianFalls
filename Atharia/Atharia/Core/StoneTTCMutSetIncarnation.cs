using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class StoneTTCMutSetIncarnation {
  public readonly SortedSet<int> elements;

  public StoneTTCMutSetIncarnation(SortedSet<int> elements) {
    this.elements = new SortedSet<int>(elements);
  }

  public StoneTTCMutSetIncarnation Copy() {
    return new StoneTTCMutSetIncarnation(new SortedSet<int>(elements));
  }
}

}
