using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class FireTTCMutSetIncarnation {
  public readonly SortedSet<int> elements;

  public FireTTCMutSetIncarnation(SortedSet<int> elements) {
    this.elements = new SortedSet<int>(elements);
  }

  public FireTTCMutSetIncarnation Copy() {
    return new FireTTCMutSetIncarnation(new SortedSet<int>(elements));
  }
}

}
