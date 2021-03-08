using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class StartBidingImpulseStrongMutSetIncarnation {
  public readonly SortedSet<int> elements;

  public StartBidingImpulseStrongMutSetIncarnation(SortedSet<int> elements) {
    this.elements = new SortedSet<int>(elements);
  }

  public StartBidingImpulseStrongMutSetIncarnation Copy() {
    return new StartBidingImpulseStrongMutSetIncarnation(new SortedSet<int>(elements));
  }
}

}
