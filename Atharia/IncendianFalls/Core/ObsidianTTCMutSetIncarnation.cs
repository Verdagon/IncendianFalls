using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class ObsidianTTCMutSetIncarnation {
  public readonly SortedSet<int> elements;

  public ObsidianTTCMutSetIncarnation(SortedSet<int> elements) {
    this.elements = new SortedSet<int>(elements);
  }

  public ObsidianTTCMutSetIncarnation Copy() {
    return new ObsidianTTCMutSetIncarnation(new SortedSet<int>(elements));
  }
}

}
