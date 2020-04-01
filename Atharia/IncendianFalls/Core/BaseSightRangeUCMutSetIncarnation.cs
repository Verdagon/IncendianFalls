using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class BaseSightRangeUCMutSetIncarnation {
  public readonly SortedSet<int> elements;

  public BaseSightRangeUCMutSetIncarnation(SortedSet<int> elements) {
    this.elements = new SortedSet<int>(elements);
  }

  public BaseSightRangeUCMutSetIncarnation Copy() {
    return new BaseSightRangeUCMutSetIncarnation(new SortedSet<int>(elements));
  }
}

}
