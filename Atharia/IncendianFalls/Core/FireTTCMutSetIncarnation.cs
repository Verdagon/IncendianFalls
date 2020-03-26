using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class FireTTCMutSetIncarnation {
  public readonly SortedSet<int> set;

  public FireTTCMutSetIncarnation(SortedSet<int> set) {
    this.set = new SortedSet<int>(set);
  }

  public FireTTCMutSetIncarnation Copy() {
    return new FireTTCMutSetIncarnation(new SortedSet<int>(set));
  }
}

}
