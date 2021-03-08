using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class LightningChargedUCMutSetIncarnation {
  public readonly SortedSet<int> elements;

  public LightningChargedUCMutSetIncarnation(SortedSet<int> elements) {
    this.elements = new SortedSet<int>(elements);
  }

  public LightningChargedUCMutSetIncarnation Copy() {
    return new LightningChargedUCMutSetIncarnation(new SortedSet<int>(elements));
  }
}

}
