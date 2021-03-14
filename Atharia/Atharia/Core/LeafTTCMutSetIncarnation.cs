using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class LeafTTCMutSetIncarnation {
  public readonly SortedSet<int> elements;

  public LeafTTCMutSetIncarnation(SortedSet<int> elements) {
    this.elements = new SortedSet<int>(elements);
  }

  public LeafTTCMutSetIncarnation Copy() {
    return new LeafTTCMutSetIncarnation(new SortedSet<int>(elements));
  }
}

}
