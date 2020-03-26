using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class GlaiveMutSetIncarnation {
  public readonly SortedSet<int> set;

  public GlaiveMutSetIncarnation(SortedSet<int> set) {
    this.set = new SortedSet<int>(set);
  }

  public GlaiveMutSetIncarnation Copy() {
    return new GlaiveMutSetIncarnation(new SortedSet<int>(set));
  }
}

}
