using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class OnFireUCMutSetIncarnation {
  public readonly SortedSet<int> elements;

  public OnFireUCMutSetIncarnation(SortedSet<int> elements) {
    this.elements = new SortedSet<int>(elements);
  }

  public OnFireUCMutSetIncarnation Copy() {
    return new OnFireUCMutSetIncarnation(new SortedSet<int>(elements));
  }
}

}
