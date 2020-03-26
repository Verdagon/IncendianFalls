using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class KamikazeAICapabilityUCMutSetIncarnation {
  public readonly SortedSet<int> set;

  public KamikazeAICapabilityUCMutSetIncarnation(SortedSet<int> set) {
    this.set = new SortedSet<int>(set);
  }

  public KamikazeAICapabilityUCMutSetIncarnation Copy() {
    return new KamikazeAICapabilityUCMutSetIncarnation(new SortedSet<int>(set));
  }
}

}
