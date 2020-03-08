using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class TutorialDefyCounterUCMutSetIncarnation {
  public readonly SortedSet<int> set;

  public TutorialDefyCounterUCMutSetIncarnation(SortedSet<int> set) {
    this.set = new SortedSet<int>(set);
  }
}

}
