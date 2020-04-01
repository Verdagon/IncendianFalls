using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class EmberDeepLevelLinkerTTCMutSetIncarnation {
  public readonly SortedSet<int> elements;

  public EmberDeepLevelLinkerTTCMutSetIncarnation(SortedSet<int> elements) {
    this.elements = new SortedSet<int>(elements);
  }

  public EmberDeepLevelLinkerTTCMutSetIncarnation Copy() {
    return new EmberDeepLevelLinkerTTCMutSetIncarnation(new SortedSet<int>(elements));
  }
}

}
