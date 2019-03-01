using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class UpStaircaseTerrainTileComponentMutSetIncarnation {
  public readonly SortedSet<int> set;

  public UpStaircaseTerrainTileComponentMutSetIncarnation(SortedSet<int> set) {
    this.set = new SortedSet<int>(set);
  }
}

}
