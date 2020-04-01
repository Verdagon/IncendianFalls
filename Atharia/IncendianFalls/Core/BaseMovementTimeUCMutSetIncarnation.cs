using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class BaseMovementTimeUCMutSetIncarnation {
  public readonly SortedSet<int> elements;

  public BaseMovementTimeUCMutSetIncarnation(SortedSet<int> elements) {
    this.elements = new SortedSet<int>(elements);
  }

  public BaseMovementTimeUCMutSetIncarnation Copy() {
    return new BaseMovementTimeUCMutSetIncarnation(new SortedSet<int>(elements));
  }
}

}
