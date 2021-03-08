using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class HealthPotionStrongMutSetIncarnation {
  public readonly SortedSet<int> elements;

  public HealthPotionStrongMutSetIncarnation(SortedSet<int> elements) {
    this.elements = new SortedSet<int>(elements);
  }

  public HealthPotionStrongMutSetIncarnation Copy() {
    return new HealthPotionStrongMutSetIncarnation(new SortedSet<int>(elements));
  }
}

}
