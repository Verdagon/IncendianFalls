using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class FireBombTTCMutSetIncarnation {
  public readonly SortedSet<int> elements;

  public FireBombTTCMutSetIncarnation(SortedSet<int> elements) {
    this.elements = new SortedSet<int>(elements);
  }

  public FireBombTTCMutSetIncarnation Copy() {
    return new FireBombTTCMutSetIncarnation(new SortedSet<int>(elements));
  }
}

}
