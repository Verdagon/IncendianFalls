using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class LevelLinkTTCMutSetIncarnation {
  public readonly SortedSet<int> set;

  public LevelLinkTTCMutSetIncarnation(SortedSet<int> set) {
    this.set = new SortedSet<int>(set);
  }

  public LevelLinkTTCMutSetIncarnation Copy() {
    return new LevelLinkTTCMutSetIncarnation(new SortedSet<int>(set));
  }
}

}
