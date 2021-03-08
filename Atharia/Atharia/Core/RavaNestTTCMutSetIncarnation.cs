using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class RavaNestTTCMutSetIncarnation {
  public readonly SortedSet<int> elements;

  public RavaNestTTCMutSetIncarnation(SortedSet<int> elements) {
    this.elements = new SortedSet<int>(elements);
  }

  public RavaNestTTCMutSetIncarnation Copy() {
    return new RavaNestTTCMutSetIncarnation(new SortedSet<int>(elements));
  }
}

}
