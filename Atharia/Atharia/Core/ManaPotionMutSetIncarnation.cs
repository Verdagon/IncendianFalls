using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class ManaPotionMutSetIncarnation {
  public readonly SortedSet<int> elements;

  public ManaPotionMutSetIncarnation(SortedSet<int> elements) {
    this.elements = new SortedSet<int>(elements);
  }

  public ManaPotionMutSetIncarnation Copy() {
    return new ManaPotionMutSetIncarnation(new SortedSet<int>(elements));
  }
}

}
