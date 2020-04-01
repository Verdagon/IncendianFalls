using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class UpStairsTTCMutSetIncarnation {
  public readonly SortedSet<int> elements;

  public UpStairsTTCMutSetIncarnation(SortedSet<int> elements) {
    this.elements = new SortedSet<int>(elements);
  }

  public UpStairsTTCMutSetIncarnation Copy() {
    return new UpStairsTTCMutSetIncarnation(new SortedSet<int>(elements));
  }
}

}
