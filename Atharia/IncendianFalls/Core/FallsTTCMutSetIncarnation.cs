using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class FallsTTCMutSetIncarnation {
  public readonly SortedSet<int> elements;

  public FallsTTCMutSetIncarnation(SortedSet<int> elements) {
    this.elements = new SortedSet<int>(elements);
  }

  public FallsTTCMutSetIncarnation Copy() {
    return new FallsTTCMutSetIncarnation(new SortedSet<int>(elements));
  }
}

}
