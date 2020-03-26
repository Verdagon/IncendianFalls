using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class SorcerousUCMutSetIncarnation {
  public readonly SortedSet<int> set;

  public SorcerousUCMutSetIncarnation(SortedSet<int> set) {
    this.set = new SortedSet<int>(set);
  }

  public SorcerousUCMutSetIncarnation Copy() {
    return new SorcerousUCMutSetIncarnation(new SortedSet<int>(set));
  }
}

}
