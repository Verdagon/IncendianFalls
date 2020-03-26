using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct TerrainTileWeakMutSetRemoveEffect : ITerrainTileWeakMutSetEffect {
  public readonly int id;
  public readonly int element;
  public TerrainTileWeakMutSetRemoveEffect(int id, int element) {
    this.id = id;
    this.element = element;
  }
  int ITerrainTileWeakMutSetEffect.id => id;
  public void visitITerrainTileWeakMutSetEffect(ITerrainTileWeakMutSetEffectVisitor visitor) {
    visitor.visitTerrainTileWeakMutSetRemoveEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitTerrainTileWeakMutSetEffect(this);
  }
}

}
