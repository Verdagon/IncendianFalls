using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class TutorialDefyCounterUCMutSetIncarnation {
  public readonly SortedSet<int> elements;

  public TutorialDefyCounterUCMutSetIncarnation(SortedSet<int> elements) {
    this.elements = new SortedSet<int>(elements);
  }

  public TutorialDefyCounterUCMutSetIncarnation Copy() {
    return new TutorialDefyCounterUCMutSetIncarnation(new SortedSet<int>(elements));
  }
}

}
