using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class SlowRodStrongMutSetIncarnation {
  public readonly SortedSet<int> elements;

  public SlowRodStrongMutSetIncarnation(SortedSet<int> elements) {
    this.elements = new SortedSet<int>(elements);
  }

  public SlowRodStrongMutSetIncarnation Copy() {
    return new SlowRodStrongMutSetIncarnation(new SortedSet<int>(elements));
  }
}

}
