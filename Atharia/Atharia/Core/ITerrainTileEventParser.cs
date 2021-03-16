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
      case "TileExplodingEvent":
        return new TileExplodingEventAsITerrainTileEvent(TileExplodingEvent.Parse(source));
      case "TileBurningEvent":
        return new TileBurningEventAsITerrainTileEvent(TileBurningEvent.Parse(source));
      default:
        throw new Exception("Unexpected: " + nextThingPeek);
    }
  }
}

}
