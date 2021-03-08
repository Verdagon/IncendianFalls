using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class FloorTTCMutSetIncarnation {
  public readonly SortedSet<int> elements;

  public FloorTTCMutSetIncarnation(SortedSet<int> elements) {
    this.elements = new SortedSet<int>(elements);
  }

  public FloorTTCMutSetIncarnation Copy() {
    return new FloorTTCMutSetIncarnation(new SortedSet<int>(elements));
  }
}

}
