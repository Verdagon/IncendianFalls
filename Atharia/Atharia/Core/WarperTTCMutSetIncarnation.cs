using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class WarperTTCMutSetIncarnation {
  public readonly SortedSet<int> elements;

  public WarperTTCMutSetIncarnation(SortedSet<int> elements) {
    this.elements = new SortedSet<int>(elements);
  }

  public WarperTTCMutSetIncarnation Copy() {
    return new WarperTTCMutSetIncarnation(new SortedSet<int>(elements));
  }
}

}
