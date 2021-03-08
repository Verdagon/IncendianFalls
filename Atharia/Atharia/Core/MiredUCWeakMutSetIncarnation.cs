using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class MiredUCWeakMutSetIncarnation {
  public readonly SortedSet<int> elements;

  public MiredUCWeakMutSetIncarnation(SortedSet<int> elements) {
    this.elements = new SortedSet<int>(elements);
  }

  public MiredUCWeakMutSetIncarnation Copy() {
    return new MiredUCWeakMutSetIncarnation(new SortedSet<int>(elements));
  }
}

}
