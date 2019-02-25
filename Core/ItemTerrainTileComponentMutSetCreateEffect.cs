using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct ItemTerrainTileComponentMutSetCreateEffect : IItemTerrainTileComponentMutSetEffect {
  public readonly int id;
  public readonly ItemTerrainTileComponentMutSetIncarnation incarnation;
  public ItemTerrainTileComponentMutSetCreateEffect(
      int id,
      ItemTerrainTileComponentMutSetIncarnation incarnation) {
    this.id = id;
    this.incarnation = incarnation;
  }
  int IItemTerrainTileComponentMutSetEffect.id => id;
  public void visit(IItemTerrainTileComponentMutSetEffectVisitor visitor) {
    visitor.visitItemTerrainTileComponentMutSetCreateEffect(this);
  }
}

}
