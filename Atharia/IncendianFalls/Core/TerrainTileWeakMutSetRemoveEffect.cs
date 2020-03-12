using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct TerrainTileWeakMutSetRemoveEffect : ITerrainTileWeakMutSetEffect {
  public readonly int id;
  public readonly int elementId;
  public TerrainTileWeakMutSetRemoveEffect(int id, int elementId) {
    this.id = id;
    this.elementId = elementId;
  }
  int ITerrainTileWeakMutSetEffect.id => id;
  public void visit(ITerrainTileWeakMutSetEffectVisitor visitor) {
    visitor.visitTerrainTileWeakMutSetRemoveEffect(this);
  }
}

}
