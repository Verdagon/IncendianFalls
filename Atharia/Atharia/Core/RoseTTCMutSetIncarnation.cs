using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class RoseTTCMutSetIncarnation {
  public readonly SortedSet<int> elements;

  public RoseTTCMutSetIncarnation(SortedSet<int> elements) {
    this.elements = new SortedSet<int>(elements);
  }

  public RoseTTCMutSetIncarnation Copy() {
    return new RoseTTCMutSetIncarnation(new SortedSet<int>(elements));
  }
}

}
