using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class CliffLandingTTCMutSetIncarnation {
  public readonly SortedSet<int> elements;

  public CliffLandingTTCMutSetIncarnation(SortedSet<int> elements) {
    this.elements = new SortedSet<int>(elements);
  }

  public CliffLandingTTCMutSetIncarnation Copy() {
    return new CliffLandingTTCMutSetIncarnation(new SortedSet<int>(elements));
  }
}

}
