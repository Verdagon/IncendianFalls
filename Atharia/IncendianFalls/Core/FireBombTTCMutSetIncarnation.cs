using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class FireBombTTCMutSetIncarnation {
  public readonly SortedSet<int> set;

  public FireBombTTCMutSetIncarnation(SortedSet<int> set) {
    this.set = new SortedSet<int>(set);
  }

  public FireBombTTCMutSetIncarnation Copy() {
    return new FireBombTTCMutSetIncarnation(new SortedSet<int>(set));
  }
}

}
