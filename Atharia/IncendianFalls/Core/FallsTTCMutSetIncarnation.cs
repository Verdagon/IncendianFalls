using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class FallsTTCMutSetIncarnation {
  public readonly SortedSet<int> set;

  public FallsTTCMutSetIncarnation(SortedSet<int> set) {
    this.set = new SortedSet<int>(set);
  }

  public FallsTTCMutSetIncarnation Copy() {
    return new FallsTTCMutSetIncarnation(new SortedSet<int>(set));
  }
}

}
