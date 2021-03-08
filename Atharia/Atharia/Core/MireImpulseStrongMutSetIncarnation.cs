using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class MireImpulseStrongMutSetIncarnation {
  public readonly SortedSet<int> elements;

  public MireImpulseStrongMutSetIncarnation(SortedSet<int> elements) {
    this.elements = new SortedSet<int>(elements);
  }

  public MireImpulseStrongMutSetIncarnation Copy() {
    return new MireImpulseStrongMutSetIncarnation(new SortedSet<int>(elements));
  }
}

}
