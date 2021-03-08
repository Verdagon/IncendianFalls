using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class DoomedUCMutSetIncarnation {
  public readonly SortedSet<int> elements;

  public DoomedUCMutSetIncarnation(SortedSet<int> elements) {
    this.elements = new SortedSet<int>(elements);
  }

  public DoomedUCMutSetIncarnation Copy() {
    return new DoomedUCMutSetIncarnation(new SortedSet<int>(elements));
  }
}

}
