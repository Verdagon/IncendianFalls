using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class SlowRodMutSetIncarnation {
  public readonly SortedSet<int> elements;

  public SlowRodMutSetIncarnation(SortedSet<int> elements) {
    this.elements = new SortedSet<int>(elements);
  }

  public SlowRodMutSetIncarnation Copy() {
    return new SlowRodMutSetIncarnation(new SortedSet<int>(elements));
  }
}

}
