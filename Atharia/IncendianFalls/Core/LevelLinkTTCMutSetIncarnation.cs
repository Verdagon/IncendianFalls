using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class LevelLinkTTCMutSetIncarnation {
  public readonly SortedSet<int> elements;

  public LevelLinkTTCMutSetIncarnation(SortedSet<int> elements) {
    this.elements = new SortedSet<int>(elements);
  }

  public LevelLinkTTCMutSetIncarnation Copy() {
    return new LevelLinkTTCMutSetIncarnation(new SortedSet<int>(elements));
  }
}

}
