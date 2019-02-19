using System;
using System.Collections.Generic;

namespace Atharia.Model {

public class TerrainTileByLocationMutMapIncarnation {
  public readonly SortedDictionary<Location, int> map;

  public TerrainTileByLocationMutMapIncarnation(SortedDictionary<Location, int> map) {
    this.map = map;
  }

  public int GetDeterministicHashCode() {
    int hash = 0;
    hash = hash * 37 + map.Count;
    foreach (var entry in map) {
      hash = hash * 37 + entry.Key.GetDeterministicHashCode();
      hash = hash * 37 + entry.Value;
    }
    return hash;
  }
}
         
}
