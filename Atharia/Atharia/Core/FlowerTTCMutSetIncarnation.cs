using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class FlowerTTCMutSetIncarnation {
  public readonly SortedSet<int> elements;

  public FlowerTTCMutSetIncarnation(SortedSet<int> elements) {
    this.elements = new SortedSet<int>(elements);
  }

  public FlowerTTCMutSetIncarnation Copy() {
    return new FlowerTTCMutSetIncarnation(new SortedSet<int>(elements));
  }
}

}
