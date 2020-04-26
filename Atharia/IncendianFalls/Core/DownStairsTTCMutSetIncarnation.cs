using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class DownStairsTTCMutSetIncarnation {
  public readonly SortedSet<int> elements;

  public DownStairsTTCMutSetIncarnation(SortedSet<int> elements) {
    this.elements = new SortedSet<int>(elements);
  }

  public DownStairsTTCMutSetIncarnation Copy() {
    return new DownStairsTTCMutSetIncarnation(new SortedSet<int>(elements));
  }
}

}
