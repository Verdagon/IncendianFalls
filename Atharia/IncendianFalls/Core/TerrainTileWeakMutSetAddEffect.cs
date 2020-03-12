using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct TerrainTileWeakMutSetAddEffect : ITerrainTileWeakMutSetEffect {
  public readonly int id;
  public readonly int elementId;
  public TerrainTileWeakMutSetAddEffect(int id, int elementId) {
    this.id = id;
    this.elementId = elementId;
  }
  int ITerrainTileWeakMutSetEffect.id => id;
  public void visit(ITerrainTileWeakMutSetEffectVisitor visitor) {
    visitor.visitTerrainTileWeakMutSetAddEffect(this);
  }
}

}
