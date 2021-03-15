using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class DeathTriggerUCMutSetIncarnation {
  public readonly SortedSet<int> elements;

  public DeathTriggerUCMutSetIncarnation(SortedSet<int> elements) {
    this.elements = new SortedSet<int>(elements);
  }

  public DeathTriggerUCMutSetIncarnation Copy() {
    return new DeathTriggerUCMutSetIncarnation(new SortedSet<int>(elements));
  }
}

}
