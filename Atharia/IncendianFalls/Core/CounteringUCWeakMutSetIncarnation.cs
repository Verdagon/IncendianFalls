using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class CounteringUCWeakMutSetIncarnation {
  public readonly SortedSet<int> elements;

  public CounteringUCWeakMutSetIncarnation(SortedSet<int> elements) {
    this.elements = new SortedSet<int>(elements);
  }

  public CounteringUCWeakMutSetIncarnation Copy() {
    return new CounteringUCWeakMutSetIncarnation(new SortedSet<int>(elements));
  }
}

}
