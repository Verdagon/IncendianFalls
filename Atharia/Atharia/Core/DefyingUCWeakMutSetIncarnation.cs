using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class DefyingUCWeakMutSetIncarnation {
  public readonly SortedSet<int> elements;

  public DefyingUCWeakMutSetIncarnation(SortedSet<int> elements) {
    this.elements = new SortedSet<int>(elements);
  }

  public DefyingUCWeakMutSetIncarnation Copy() {
    return new DefyingUCWeakMutSetIncarnation(new SortedSet<int>(elements));
  }
}

}
