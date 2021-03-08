using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class GrassTTCMutSetIncarnation {
  public readonly SortedSet<int> elements;

  public GrassTTCMutSetIncarnation(SortedSet<int> elements) {
    this.elements = new SortedSet<int>(elements);
  }

  public GrassTTCMutSetIncarnation Copy() {
    return new GrassTTCMutSetIncarnation(new SortedSet<int>(elements));
  }
}

}
