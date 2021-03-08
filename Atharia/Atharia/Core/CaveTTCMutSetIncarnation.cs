using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class CaveTTCMutSetIncarnation {
  public readonly SortedSet<int> elements;

  public CaveTTCMutSetIncarnation(SortedSet<int> elements) {
    this.elements = new SortedSet<int>(elements);
  }

  public CaveTTCMutSetIncarnation Copy() {
    return new CaveTTCMutSetIncarnation(new SortedSet<int>(elements));
  }
}

}
