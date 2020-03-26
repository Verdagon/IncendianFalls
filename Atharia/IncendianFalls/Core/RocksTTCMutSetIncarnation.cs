using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class RocksTTCMutSetIncarnation {
  public readonly SortedSet<int> set;

  public RocksTTCMutSetIncarnation(SortedSet<int> set) {
    this.set = new SortedSet<int>(set);
  }

  public RocksTTCMutSetIncarnation Copy() {
    return new RocksTTCMutSetIncarnation(new SortedSet<int>(set));
  }
}

}
