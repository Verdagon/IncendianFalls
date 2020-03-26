using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct TerrainTileWeakMutSetAddEffect : ITerrainTileWeakMutSetEffect {
  public readonly int id;
  public readonly int element;
  public TerrainTileWeakMutSetAddEffect(int id, int element) {
    this.id = id;
    this.element = element;
  }
  int ITerrainTileWeakMutSetEffect.id => id;
  public void visitITerrainTileWeakMutSetEffect(ITerrainTileWeakMutSetEffectVisitor visitor) {
    visitor.visitTerrainTileWeakMutSetAddEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitTerrainTileWeakMutSetEffect(this);
  }
}

}
