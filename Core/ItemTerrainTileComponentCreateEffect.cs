using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct ItemTerrainTileComponentCreateEffect : IItemTerrainTileComponentEffect {
  public readonly int id;
  public readonly ItemTerrainTileComponentIncarnation incarnation;
  public ItemTerrainTileComponentCreateEffect(
      int id,
      ItemTerrainTileComponentIncarnation incarnation) {
    this.id = id;
    this.incarnation = incarnation;
  }
  int IItemTerrainTileComponentEffect.id => id;
  public void visit(IItemTerrainTileComponentEffectVisitor visitor) {
    visitor.visitItemTerrainTileComponentCreateEffect(this);
  }
}
       
}
