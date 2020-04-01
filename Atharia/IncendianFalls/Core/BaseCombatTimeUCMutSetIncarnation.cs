using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class BaseCombatTimeUCMutSetIncarnation {
  public readonly SortedSet<int> elements;

  public BaseCombatTimeUCMutSetIncarnation(SortedSet<int> elements) {
    this.elements = new SortedSet<int>(elements);
  }

  public BaseCombatTimeUCMutSetIncarnation Copy() {
    return new BaseCombatTimeUCMutSetIncarnation(new SortedSet<int>(elements));
  }
}

}
