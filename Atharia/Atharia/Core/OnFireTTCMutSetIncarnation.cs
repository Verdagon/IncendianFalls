using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class OnFireTTCMutSetIncarnation {
  public readonly SortedSet<int> elements;

  public OnFireTTCMutSetIncarnation(SortedSet<int> elements) {
    this.elements = new SortedSet<int>(elements);
  }

  public OnFireTTCMutSetIncarnation Copy() {
    return new OnFireTTCMutSetIncarnation(new SortedSet<int>(elements));
  }
}

}
