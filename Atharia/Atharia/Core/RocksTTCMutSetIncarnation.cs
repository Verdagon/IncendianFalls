using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class RocksTTCMutSetIncarnation {
  public readonly SortedSet<int> elements;

  public RocksTTCMutSetIncarnation(SortedSet<int> elements) {
    this.elements = new SortedSet<int>(elements);
  }

  public RocksTTCMutSetIncarnation Copy() {
    return new RocksTTCMutSetIncarnation(new SortedSet<int>(elements));
  }
}

}
