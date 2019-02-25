using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct ItemTerrainTileComponentMutSetRemoveEffect : IItemTerrainTileComponentMutSetEffect {
  public readonly int id;
  public readonly int elementId;
  public ItemTerrainTileComponentMutSetRemoveEffect(int id, int elementId) {
    this.id = id;
    this.elementId = elementId;
  }
  int IItemTerrainTileComponentMutSetEffect.id => id;
  public void visit(IItemTerrainTileComponentMutSetEffectVisitor visitor) {
    visitor.visitItemTerrainTileComponentMutSetRemoveEffect(this);
  }
}

}
