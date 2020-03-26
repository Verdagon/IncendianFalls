using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class TerrainTileWeakMutSetIncarnation {
  public readonly SortedSet<int> set;

  public TerrainTileWeakMutSetIncarnation(SortedSet<int> set) {
    this.set = new SortedSet<int>(set);
  }

  public TerrainTileWeakMutSetIncarnation Copy() {
    return new TerrainTileWeakMutSetIncarnation(new SortedSet<int>(set));
  }
}

}
