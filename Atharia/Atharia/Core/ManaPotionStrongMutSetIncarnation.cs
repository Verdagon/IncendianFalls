using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class ManaPotionStrongMutSetIncarnation {
  public readonly SortedSet<int> elements;

  public ManaPotionStrongMutSetIncarnation(SortedSet<int> elements) {
    this.elements = new SortedSet<int>(elements);
  }

  public ManaPotionStrongMutSetIncarnation Copy() {
    return new ManaPotionStrongMutSetIncarnation(new SortedSet<int>(elements));
  }
}

}
