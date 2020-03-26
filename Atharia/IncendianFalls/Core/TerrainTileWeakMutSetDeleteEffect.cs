using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct TerrainTileWeakMutSetDeleteEffect : ITerrainTileWeakMutSetEffect {
  public readonly int id;
  public TerrainTileWeakMutSetDeleteEffect(int id) {
    this.id = id;
  }
  int ITerrainTileWeakMutSetEffect.id => id;
  public void visitITerrainTileWeakMutSetEffect(ITerrainTileWeakMutSetEffectVisitor visitor) {
    visitor.visitTerrainTileWeakMutSetDeleteEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitTerrainTileWeakMutSetEffect(this);
  }
}

}
