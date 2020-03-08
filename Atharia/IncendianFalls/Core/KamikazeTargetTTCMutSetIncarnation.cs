using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class KamikazeTargetTTCMutSetIncarnation {
  public readonly SortedSet<int> set;

  public KamikazeTargetTTCMutSetIncarnation(SortedSet<int> set) {
    this.set = new SortedSet<int>(set);
  }
}

}
