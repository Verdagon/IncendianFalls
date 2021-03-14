using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class LotusTTCMutSetIncarnation {
  public readonly SortedSet<int> elements;

  public LotusTTCMutSetIncarnation(SortedSet<int> elements) {
    this.elements = new SortedSet<int>(elements);
  }

  public LotusTTCMutSetIncarnation Copy() {
    return new LotusTTCMutSetIncarnation(new SortedSet<int>(elements));
  }
}

}
