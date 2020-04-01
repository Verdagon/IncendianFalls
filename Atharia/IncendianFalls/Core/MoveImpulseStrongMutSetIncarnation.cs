using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class MoveImpulseStrongMutSetIncarnation {
  public readonly SortedSet<int> elements;

  public MoveImpulseStrongMutSetIncarnation(SortedSet<int> elements) {
    this.elements = new SortedSet<int>(elements);
  }

  public MoveImpulseStrongMutSetIncarnation Copy() {
    return new MoveImpulseStrongMutSetIncarnation(new SortedSet<int>(elements));
  }
}

}
