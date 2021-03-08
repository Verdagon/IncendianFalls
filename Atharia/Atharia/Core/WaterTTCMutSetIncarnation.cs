using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class WaterTTCMutSetIncarnation {
  public readonly SortedSet<int> elements;

  public WaterTTCMutSetIncarnation(SortedSet<int> elements) {
    this.elements = new SortedSet<int>(elements);
  }

  public WaterTTCMutSetIncarnation Copy() {
    return new WaterTTCMutSetIncarnation(new SortedSet<int>(elements));
  }
}

}
