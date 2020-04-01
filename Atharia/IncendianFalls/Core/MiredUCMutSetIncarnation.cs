using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class MiredUCMutSetIncarnation {
  public readonly SortedSet<int> elements;

  public MiredUCMutSetIncarnation(SortedSet<int> elements) {
    this.elements = new SortedSet<int>(elements);
  }

  public MiredUCMutSetIncarnation Copy() {
    return new MiredUCMutSetIncarnation(new SortedSet<int>(elements));
  }
}

}
