using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class SpeedRingMutSetIncarnation {
  public readonly SortedSet<int> elements;

  public SpeedRingMutSetIncarnation(SortedSet<int> elements) {
    this.elements = new SortedSet<int>(elements);
  }

  public SpeedRingMutSetIncarnation Copy() {
    return new SpeedRingMutSetIncarnation(new SortedSet<int>(elements));
  }
}

}
