using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class DirtTTCMutSetIncarnation {
  public readonly SortedSet<int> elements;

  public DirtTTCMutSetIncarnation(SortedSet<int> elements) {
    this.elements = new SortedSet<int>(elements);
  }

  public DirtTTCMutSetIncarnation Copy() {
    return new DirtTTCMutSetIncarnation(new SortedSet<int>(elements));
  }
}

}
