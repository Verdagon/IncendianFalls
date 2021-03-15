using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public static class ITerrainTileEventParser {
  public static ITerrainTileEvent Parse(ParseSource source) {
    var nextThingPeek = source.PeekNextWord();
    switch (nextThingPeek) {
      case "UnitUnleashBideEvent":
        return new UnitUnleashBideEventAsITerrainTileEvent(UnitUnleashBideEvent.Parse(source));
      case "UnitExplosionedEvent":
        return new UnitExplosionedEventAsITerrainTileEvent(UnitExplosionedEvent.Parse(source));
      case "UnitBurningEvent":
        return new UnitBurningEventAsITerrainTileEvent(UnitBurningEvent.Parse(source));
      case "UnitBlazedEvent":
        return new UnitBlazedEventAsITerrainTileEvent(UnitBlazedEvent.Parse(source));
      case "UnitFireBombedEvent":
        return new UnitFireBombedEventAsITerrainTileEvent(UnitFireBombedEvent.Parse(source));
      default:
        throw new Exception("Unexpected: " + nextThingPeek);
    }
  }
}

}
