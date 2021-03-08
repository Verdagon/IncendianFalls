using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class WallTTCMutSetIncarnation {
  public readonly SortedSet<int> elements;

  public WallTTCMutSetIncarnation(SortedSet<int> elements) {
    this.elements = new SortedSet<int>(elements);
  }

  public WallTTCMutSetIncarnation Copy() {
    return new WallTTCMutSetIncarnation(new SortedSet<int>(elements));
  }
}

}
