using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class HealthPotionMutSetIncarnation {
  public readonly SortedSet<int> elements;

  public HealthPotionMutSetIncarnation(SortedSet<int> elements) {
    this.elements = new SortedSet<int>(elements);
  }

  public HealthPotionMutSetIncarnation Copy() {
    return new HealthPotionMutSetIncarnation(new SortedSet<int>(elements));
  }
}

}
