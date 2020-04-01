using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class HoldPositionImpulseStrongMutSetIncarnation {
  public readonly SortedSet<int> elements;

  public HoldPositionImpulseStrongMutSetIncarnation(SortedSet<int> elements) {
    this.elements = new SortedSet<int>(elements);
  }

  public HoldPositionImpulseStrongMutSetIncarnation Copy() {
    return new HoldPositionImpulseStrongMutSetIncarnation(new SortedSet<int>(elements));
  }
}

}
