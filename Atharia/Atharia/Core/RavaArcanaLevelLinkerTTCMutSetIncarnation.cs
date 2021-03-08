using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class RavaArcanaLevelLinkerTTCMutSetIncarnation {
  public readonly SortedSet<int> elements;

  public RavaArcanaLevelLinkerTTCMutSetIncarnation(SortedSet<int> elements) {
    this.elements = new SortedSet<int>(elements);
  }

  public RavaArcanaLevelLinkerTTCMutSetIncarnation Copy() {
    return new RavaArcanaLevelLinkerTTCMutSetIncarnation(new SortedSet<int>(elements));
  }
}

}
