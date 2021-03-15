using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class BequeathUCMutSetIncarnation {
  public readonly SortedSet<int> elements;

  public BequeathUCMutSetIncarnation(SortedSet<int> elements) {
    this.elements = new SortedSet<int>(elements);
  }

  public BequeathUCMutSetIncarnation Copy() {
    return new BequeathUCMutSetIncarnation(new SortedSet<int>(elements));
  }
}

}
