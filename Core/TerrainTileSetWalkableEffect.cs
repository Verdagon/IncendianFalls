using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct TerrainTileSetWalkableEffect : ITerrainTileEffect {
  public readonly int id;
  public readonly bool newValue;
  public TerrainTileSetWalkableEffect(
      int id,
      bool newValue) {
    this.id = id;
    this.newValue = newValue;
  }
  int ITerrainTileEffect.id => id;

  public void visit(ITerrainTileEffectVisitor visitor) {
    visitor.visitTerrainTileSetWalkableEffect(this);
  }
}

}
