using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class OnFireUCWeakMutSetIncarnation {
  public readonly SortedSet<int> elements;

  public OnFireUCWeakMutSetIncarnation(SortedSet<int> elements) {
    this.elements = new SortedSet<int>(elements);
  }

  public OnFireUCWeakMutSetIncarnation Copy() {
    return new OnFireUCWeakMutSetIncarnation(new SortedSet<int>(elements));
  }
}

}
