using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct TerrainTileByLocationMutMapDeleteEffect : ITerrainTileByLocationMutMapEffect {
  public readonly int id;
  public TerrainTileByLocationMutMapDeleteEffect(int id) {
    this.id = id;
  }
  int ITerrainTileByLocationMutMapEffect.id => id;
  public void visitITerrainTileByLocationMutMapEffect(ITerrainTileByLocationMutMapEffectVisitor visitor) {
    visitor.visitTerrainTileByLocationMutMapDeleteEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitTerrainTileByLocationMutMapEffect(this);
  }
}

}
