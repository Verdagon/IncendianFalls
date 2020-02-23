using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class GrassTTCMutSetIncarnation {
  public readonly SortedSet<int> set;

  public GrassTTCMutSetIncarnation(SortedSet<int> set) {
    this.set = new SortedSet<int>(set);
  }
}

}
