using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct ItemTerrainTileComponentMutSetAddEffect : IItemTerrainTileComponentMutSetEffect {
  public readonly int id;
  public readonly int elementId;
  public ItemTerrainTileComponentMutSetAddEffect(int id, int elementId) {
    this.id = id;
    this.elementId = elementId;
  }
  int IItemTerrainTileComponentMutSetEffect.id => id;
  public void visit(IItemTerrainTileComponentMutSetEffectVisitor visitor) {
    visitor.visitItemTerrainTileComponentMutSetAddEffect(this);
  }
}

}
