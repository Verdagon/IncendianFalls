using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class LightningChargedUCWeakMutSetIncarnation {
  public readonly SortedSet<int> elements;

  public LightningChargedUCWeakMutSetIncarnation(SortedSet<int> elements) {
    this.elements = new SortedSet<int>(elements);
  }

  public LightningChargedUCWeakMutSetIncarnation Copy() {
    return new LightningChargedUCWeakMutSetIncarnation(new SortedSet<int>(elements));
  }
}

}
