using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct ItemTerrainTileComponentMutSetCreateEffect : IItemTerrainTileComponentMutSetEffect {
  public readonly int id;
  public ItemTerrainTileComponentMutSetCreateEffect(int id) {
    this.id = id;
  }
  int IItemTerrainTileComponentMutSetEffect.id => id;
  public void visit(IItemTerrainTileComponentMutSetEffectVisitor visitor) {
    visitor.visitItemTerrainTileComponentMutSetCreateEffect(this);
  }
}

}
