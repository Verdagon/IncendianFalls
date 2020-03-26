using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct TerrainTileWeakMutSetCreateEffect : ITerrainTileWeakMutSetEffect {
  public readonly int id;
  public TerrainTileWeakMutSetCreateEffect(int id) {
    this.id = id;
  }
  int ITerrainTileWeakMutSetEffect.id => id;
  public void visitITerrainTileWeakMutSetEffect(ITerrainTileWeakMutSetEffectVisitor visitor) {
    visitor.visitTerrainTileWeakMutSetCreateEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitTerrainTileWeakMutSetEffect(this);
  }
}

}
