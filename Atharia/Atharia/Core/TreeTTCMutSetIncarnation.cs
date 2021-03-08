using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class TreeTTCMutSetIncarnation {
  public readonly SortedSet<int> elements;

  public TreeTTCMutSetIncarnation(SortedSet<int> elements) {
    this.elements = new SortedSet<int>(elements);
  }

  public TreeTTCMutSetIncarnation Copy() {
    return new TreeTTCMutSetIncarnation(new SortedSet<int>(elements));
  }
}

}
