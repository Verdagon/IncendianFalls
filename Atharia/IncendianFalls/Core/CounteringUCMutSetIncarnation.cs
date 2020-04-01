using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class CounteringUCMutSetIncarnation {
  public readonly SortedSet<int> elements;

  public CounteringUCMutSetIncarnation(SortedSet<int> elements) {
    this.elements = new SortedSet<int>(elements);
  }

  public CounteringUCMutSetIncarnation Copy() {
    return new CounteringUCMutSetIncarnation(new SortedSet<int>(elements));
  }
}

}
