using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class ObsidianFloorTTCMutSetIncarnation {
  public readonly SortedSet<int> elements;

  public ObsidianFloorTTCMutSetIncarnation(SortedSet<int> elements) {
    this.elements = new SortedSet<int>(elements);
  }

  public ObsidianFloorTTCMutSetIncarnation Copy() {
    return new ObsidianFloorTTCMutSetIncarnation(new SortedSet<int>(elements));
  }
}

}
