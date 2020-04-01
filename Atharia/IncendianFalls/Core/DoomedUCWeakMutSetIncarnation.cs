using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class DoomedUCWeakMutSetIncarnation {
  public readonly SortedSet<int> elements;

  public DoomedUCWeakMutSetIncarnation(SortedSet<int> elements) {
    this.elements = new SortedSet<int>(elements);
  }

  public DoomedUCWeakMutSetIncarnation Copy() {
    return new DoomedUCWeakMutSetIncarnation(new SortedSet<int>(elements));
  }
}

}
