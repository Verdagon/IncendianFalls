using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class ChallengingUCMutSetIncarnation {
  public readonly SortedSet<int> elements;

  public ChallengingUCMutSetIncarnation(SortedSet<int> elements) {
    this.elements = new SortedSet<int>(elements);
  }

  public ChallengingUCMutSetIncarnation Copy() {
    return new ChallengingUCMutSetIncarnation(new SortedSet<int>(elements));
  }
}

}
