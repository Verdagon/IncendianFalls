using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class GlaiveMutSetIncarnation {
  public readonly SortedSet<int> elements;

  public GlaiveMutSetIncarnation(SortedSet<int> elements) {
    this.elements = new SortedSet<int>(elements);
  }

  public GlaiveMutSetIncarnation Copy() {
    return new GlaiveMutSetIncarnation(new SortedSet<int>(elements));
  }
}

}
