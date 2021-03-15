using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public interface ITerrainTileEventVisitor {
  void VisitITerrainTileEvent(UnitUnleashBideEventAsITerrainTileEvent obj);
  void VisitITerrainTileEvent(UnitExplosionedEventAsITerrainTileEvent obj);
  void VisitITerrainTileEvent(UnitBurningEventAsITerrainTileEvent obj);
  void VisitITerrainTileEvent(UnitBlazedEventAsITerrainTileEvent obj);
  void VisitITerrainTileEvent(UnitFireBombedEventAsITerrainTileEvent obj);
}

}
