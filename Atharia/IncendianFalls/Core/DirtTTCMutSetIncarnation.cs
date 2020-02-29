using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class DirtTTCMutSetIncarnation {
  public readonly SortedSet<int> set;

  public DirtTTCMutSetIncarnation(SortedSet<int> set) {
    this.set = new SortedSet<int>(set);
  }
}

}
