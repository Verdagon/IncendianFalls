using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class BaseCombatTimeUCMutSetIncarnation {
  public readonly SortedSet<int> set;

  public BaseCombatTimeUCMutSetIncarnation(SortedSet<int> set) {
    this.set = new SortedSet<int>(set);
  }

  public BaseCombatTimeUCMutSetIncarnation Copy() {
    return new BaseCombatTimeUCMutSetIncarnation(new SortedSet<int>(set));
  }
}

}
