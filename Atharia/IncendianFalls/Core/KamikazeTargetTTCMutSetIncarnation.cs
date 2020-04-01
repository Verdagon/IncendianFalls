using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class KamikazeTargetTTCMutSetIncarnation {
  public readonly SortedSet<int> elements;

  public KamikazeTargetTTCMutSetIncarnation(SortedSet<int> elements) {
    this.elements = new SortedSet<int>(elements);
  }

  public KamikazeTargetTTCMutSetIncarnation Copy() {
    return new KamikazeTargetTTCMutSetIncarnation(new SortedSet<int>(elements));
  }
}

}
