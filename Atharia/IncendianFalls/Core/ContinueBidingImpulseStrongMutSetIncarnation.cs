using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class ContinueBidingImpulseStrongMutSetIncarnation {
  public readonly SortedSet<int> elements;

  public ContinueBidingImpulseStrongMutSetIncarnation(SortedSet<int> elements) {
    this.elements = new SortedSet<int>(elements);
  }

  public ContinueBidingImpulseStrongMutSetIncarnation Copy() {
    return new ContinueBidingImpulseStrongMutSetIncarnation(new SortedSet<int>(elements));
  }
}

}
