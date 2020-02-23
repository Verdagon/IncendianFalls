using System;
using System.Collections;

using System.Collections.Generic;

namespace Geomancer.Model {

public class TerrainTileByLocationMutMapIncarnation {
  public readonly SortedDictionary<Location, int> map;

  public TerrainTileByLocationMutMapIncarnation(SortedDictionary<Location, int> map) {
    this.map = map;
  }
}
         
}
