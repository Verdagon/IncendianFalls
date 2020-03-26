using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct TerrainTileDeleteEffect : ITerrainTileEffect {
  public readonly int id;
  public TerrainTileDeleteEffect(int id) {
    this.id = id;
  }
  int ITerrainTileEffect.id => id;
  public void visitITerrainTileEffect(ITerrainTileEffectVisitor visitor) {
    visitor.visitTerrainTileDeleteEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitTerrainTileEffect(this);
  }
}

}
