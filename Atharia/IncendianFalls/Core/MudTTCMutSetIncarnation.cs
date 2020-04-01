using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class MudTTCMutSetIncarnation {
  public readonly SortedSet<int> elements;

  public MudTTCMutSetIncarnation(SortedSet<int> elements) {
    this.elements = new SortedSet<int>(elements);
  }

  public MudTTCMutSetIncarnation Copy() {
    return new MudTTCMutSetIncarnation(new SortedSet<int>(elements));
  }
}

}
