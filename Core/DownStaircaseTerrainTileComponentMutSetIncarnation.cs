using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class DownStaircaseTerrainTileComponentMutSetIncarnation {
  public readonly SortedSet<int> set;

  public DownStaircaseTerrainTileComponentMutSetIncarnation(SortedSet<int> set) {
    this.set = new SortedSet<int>(set);
  }
}

}
