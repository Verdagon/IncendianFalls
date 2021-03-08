using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class CounterImpulseStrongMutSetIncarnation {
  public readonly SortedSet<int> elements;

  public CounterImpulseStrongMutSetIncarnation(SortedSet<int> elements) {
    this.elements = new SortedSet<int>(elements);
  }

  public CounterImpulseStrongMutSetIncarnation Copy() {
    return new CounterImpulseStrongMutSetIncarnation(new SortedSet<int>(elements));
  }
}

}
