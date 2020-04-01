using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class SpeedRingStrongMutSetIncarnation {
  public readonly SortedSet<int> elements;

  public SpeedRingStrongMutSetIncarnation(SortedSet<int> elements) {
    this.elements = new SortedSet<int>(elements);
  }

  public SpeedRingStrongMutSetIncarnation Copy() {
    return new SpeedRingStrongMutSetIncarnation(new SortedSet<int>(elements));
  }
}

}
