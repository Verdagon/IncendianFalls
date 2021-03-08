using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class LevelMutSetIncarnation {
  public readonly SortedSet<int> elements;

  public LevelMutSetIncarnation(SortedSet<int> elements) {
    this.elements = new SortedSet<int>(elements);
  }

  public LevelMutSetIncarnation Copy() {
    return new LevelMutSetIncarnation(new SortedSet<int>(elements));
  }
}

}
