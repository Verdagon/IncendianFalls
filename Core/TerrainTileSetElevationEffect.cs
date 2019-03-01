using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct TerrainTileSetElevationEffect : ITerrainTileEffect {
  public readonly int id;
  public readonly int newValue;
  public TerrainTileSetElevationEffect(
      int id,
      int newValue) {
    this.id = id;
    this.newValue = newValue;
  }
  int ITerrainTileEffect.id => id;

  public void visit(ITerrainTileEffectVisitor visitor) {
    visitor.visitTerrainTileSetElevationEffect(this);
  }
}

}
