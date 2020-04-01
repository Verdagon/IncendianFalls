using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class ArmorMutSetIncarnation {
  public readonly SortedSet<int> elements;

  public ArmorMutSetIncarnation(SortedSet<int> elements) {
    this.elements = new SortedSet<int>(elements);
  }

  public ArmorMutSetIncarnation Copy() {
    return new ArmorMutSetIncarnation(new SortedSet<int>(elements));
  }
}

}
