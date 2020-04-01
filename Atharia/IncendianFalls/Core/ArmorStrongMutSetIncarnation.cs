using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class ArmorStrongMutSetIncarnation {
  public readonly SortedSet<int> elements;

  public ArmorStrongMutSetIncarnation(SortedSet<int> elements) {
    this.elements = new SortedSet<int>(elements);
  }

  public ArmorStrongMutSetIncarnation Copy() {
    return new ArmorStrongMutSetIncarnation(new SortedSet<int>(elements));
  }
}

}
