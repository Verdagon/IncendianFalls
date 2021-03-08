using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class IncendianFallsLevelLinkerTTCMutSetIncarnation {
  public readonly SortedSet<int> elements;

  public IncendianFallsLevelLinkerTTCMutSetIncarnation(SortedSet<int> elements) {
    this.elements = new SortedSet<int>(elements);
  }

  public IncendianFallsLevelLinkerTTCMutSetIncarnation Copy() {
    return new IncendianFallsLevelLinkerTTCMutSetIncarnation(new SortedSet<int>(elements));
  }
}

}
