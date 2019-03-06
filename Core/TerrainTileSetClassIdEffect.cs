using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct TerrainTileSetClassIdEffect : ITerrainTileEffect {
  public readonly int id;
  public readonly string newValue;
  public TerrainTileSetClassIdEffect(
      int id,
      string newValue) {
    this.id = id;
    this.newValue = newValue;
  }
  int ITerrainTileEffect.id => id;

  public void visit(ITerrainTileEffectVisitor visitor) {
    visitor.visitTerrainTileSetClassIdEffect(this);
  }
}

}
