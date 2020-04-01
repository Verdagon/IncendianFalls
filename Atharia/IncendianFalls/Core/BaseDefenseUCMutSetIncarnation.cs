using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class BaseDefenseUCMutSetIncarnation {
  public readonly SortedSet<int> elements;

  public BaseDefenseUCMutSetIncarnation(SortedSet<int> elements) {
    this.elements = new SortedSet<int>(elements);
  }

  public BaseDefenseUCMutSetIncarnation Copy() {
    return new BaseDefenseUCMutSetIncarnation(new SortedSet<int>(elements));
  }
}

}
