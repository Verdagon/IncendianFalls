using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class InvincibilityUCMutSetIncarnation {
  public readonly SortedSet<int> set;

  public InvincibilityUCMutSetIncarnation(SortedSet<int> set) {
    this.set = new SortedSet<int>(set);
  }
}

}
