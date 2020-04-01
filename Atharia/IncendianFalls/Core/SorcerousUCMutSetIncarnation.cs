using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class SorcerousUCMutSetIncarnation {
  public readonly SortedSet<int> elements;

  public SorcerousUCMutSetIncarnation(SortedSet<int> elements) {
    this.elements = new SortedSet<int>(elements);
  }

  public SorcerousUCMutSetIncarnation Copy() {
    return new SorcerousUCMutSetIncarnation(new SortedSet<int>(elements));
  }
}

}
