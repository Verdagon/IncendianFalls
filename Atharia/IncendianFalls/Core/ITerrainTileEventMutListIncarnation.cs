using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public class ITerrainTileEventMutListIncarnation {
  public readonly List<ITerrainTileEvent> list;

  public ITerrainTileEventMutListIncarnation(List<ITerrainTileEvent> list) {
    this.list = list;
  }

  public ITerrainTileEventMutListIncarnation Copy() {
    return new ITerrainTileEventMutListIncarnation(new List<ITerrainTileEvent>(list));
  }
}
         
}
