using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class DefyingUCMutSetIncarnation {
  public readonly SortedSet<int> elements;

  public DefyingUCMutSetIncarnation(SortedSet<int> elements) {
    this.elements = new SortedSet<int>(elements);
  }

  public DefyingUCMutSetIncarnation Copy() {
    return new DefyingUCMutSetIncarnation(new SortedSet<int>(elements));
  }
}

}
