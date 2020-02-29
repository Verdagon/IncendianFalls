using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class MudTTCMutSetIncarnation {
  public readonly SortedSet<int> set;

  public MudTTCMutSetIncarnation(SortedSet<int> set) {
    this.set = new SortedSet<int>(set);
  }
}

}
