using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class UnitMutSetIncarnation {
  public readonly SortedSet<int> elements;

  public UnitMutSetIncarnation(SortedSet<int> elements) {
    this.elements = new SortedSet<int>(elements);
  }

  public UnitMutSetIncarnation Copy() {
    return new UnitMutSetIncarnation(new SortedSet<int>(elements));
  }
}

}
