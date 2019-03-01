using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class ItemTerrainTileComponentMutSetIncarnation {
  public readonly SortedSet<int> set;

  public ItemTerrainTileComponentMutSetIncarnation(SortedSet<int> set) {
    this.set = new SortedSet<int>(set);
  }
}

}
