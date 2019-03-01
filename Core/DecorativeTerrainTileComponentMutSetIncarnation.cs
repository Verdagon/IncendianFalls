using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class DecorativeTerrainTileComponentMutSetIncarnation {
  public readonly SortedSet<int> set;

  public DecorativeTerrainTileComponentMutSetIncarnation(SortedSet<int> set) {
    this.set = new SortedSet<int>(set);
  }
}

}
