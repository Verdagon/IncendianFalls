using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class BaseOffenseUCMutSetIncarnation {
  public readonly SortedSet<int> elements;

  public BaseOffenseUCMutSetIncarnation(SortedSet<int> elements) {
    this.elements = new SortedSet<int>(elements);
  }

  public BaseOffenseUCMutSetIncarnation Copy() {
    return new BaseOffenseUCMutSetIncarnation(new SortedSet<int>(elements));
  }
}

}
