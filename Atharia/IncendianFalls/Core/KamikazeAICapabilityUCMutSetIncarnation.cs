using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class KamikazeAICapabilityUCMutSetIncarnation {
  public readonly SortedSet<int> elements;

  public KamikazeAICapabilityUCMutSetIncarnation(SortedSet<int> elements) {
    this.elements = new SortedSet<int>(elements);
  }

  public KamikazeAICapabilityUCMutSetIncarnation Copy() {
    return new KamikazeAICapabilityUCMutSetIncarnation(new SortedSet<int>(elements));
  }
}

}
