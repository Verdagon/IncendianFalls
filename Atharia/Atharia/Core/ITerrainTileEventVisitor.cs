using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public interface ITerrainTileEventVisitor {
  void VisitITerrainTileEvent(UnitUnleashBideEventAsITerrainTileEvent obj);
  void VisitITerrainTileEvent(TileExplodingEventAsITerrainTileEvent obj);
  void VisitITerrainTileEvent(TileBurningEventAsITerrainTileEvent obj);
}

}
