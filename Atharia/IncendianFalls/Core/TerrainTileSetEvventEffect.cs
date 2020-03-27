using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct TerrainTileSetEvventEffect : ITerrainTileEffect {
  public readonly int id;
  public readonly ITerrainTileEvent newValue;
  public TerrainTileSetEvventEffect(
      int id,
      ITerrainTileEvent newValue) {
    this.id = id;
    this.newValue = newValue;
  }
  int ITerrainTileEffect.id => id;

  public void visitITerrainTileEffect(ITerrainTileEffectVisitor visitor) {
    visitor.visitTerrainTileSetEvventEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitTerrainTileEffect(this);
  }
}

}
