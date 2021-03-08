using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class BloodTTCMutSetIncarnation {
  public readonly SortedSet<int> elements;

  public BloodTTCMutSetIncarnation(SortedSet<int> elements) {
    this.elements = new SortedSet<int>(elements);
  }

  public BloodTTCMutSetIncarnation Copy() {
    return new BloodTTCMutSetIncarnation(new SortedSet<int>(elements));
  }
}

}
