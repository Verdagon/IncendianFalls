using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class ItemTTCMutSetIncarnation {
  public readonly SortedSet<int> elements;

  public ItemTTCMutSetIncarnation(SortedSet<int> elements) {
    this.elements = new SortedSet<int>(elements);
  }

  public ItemTTCMutSetIncarnation Copy() {
    return new ItemTTCMutSetIncarnation(new SortedSet<int>(elements));
  }
}

}
