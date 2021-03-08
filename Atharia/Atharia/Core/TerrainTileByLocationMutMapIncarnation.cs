using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public class TerrainTileByLocationMutMapIncarnation {
  public readonly SortedDictionary<Location, int> elements;

  public TerrainTileByLocationMutMapIncarnation(SortedDictionary<Location, int> elements) {
    this.elements = elements;
  }

  public TerrainTileByLocationMutMapIncarnation Copy() {
    return new TerrainTileByLocationMutMapIncarnation(new SortedDictionary<Location, int>(elements));
  }
}
         
}
