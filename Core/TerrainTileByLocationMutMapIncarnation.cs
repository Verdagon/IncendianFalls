using System;
using System.Collections.Generic;

namespace Atharia.Model {

public class TerrainTileByLocationMutMapIncarnation {
  public readonly SortedDictionary<Location, int> map;

  public TerrainTileByLocationMutMapIncarnation(SortedDictionary<Location, int> map) {
    this.map = map;
  }
}
         
}
