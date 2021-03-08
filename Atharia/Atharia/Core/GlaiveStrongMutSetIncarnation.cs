using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class GlaiveStrongMutSetIncarnation {
  public readonly SortedSet<int> elements;

  public GlaiveStrongMutSetIncarnation(SortedSet<int> elements) {
    this.elements = new SortedSet<int>(elements);
  }

  public GlaiveStrongMutSetIncarnation Copy() {
    return new GlaiveStrongMutSetIncarnation(new SortedSet<int>(elements));
  }
}

}
