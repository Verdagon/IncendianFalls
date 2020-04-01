using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class CaveWallTTCMutSetIncarnation {
  public readonly SortedSet<int> elements;

  public CaveWallTTCMutSetIncarnation(SortedSet<int> elements) {
    this.elements = new SortedSet<int>(elements);
  }

  public CaveWallTTCMutSetIncarnation Copy() {
    return new CaveWallTTCMutSetIncarnation(new SortedSet<int>(elements));
  }
}

}
