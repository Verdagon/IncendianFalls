using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class MarkerTTCMutSetIncarnation {
  public readonly SortedSet<int> elements;

  public MarkerTTCMutSetIncarnation(SortedSet<int> elements) {
    this.elements = new SortedSet<int>(elements);
  }

  public MarkerTTCMutSetIncarnation Copy() {
    return new MarkerTTCMutSetIncarnation(new SortedSet<int>(elements));
  }
}

}
