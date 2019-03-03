using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct ItemTerrainTileComponentCreateEffect : IItemTerrainTileComponentEffect {
  public readonly int id;
  public ItemTerrainTileComponentCreateEffect(int id) {
    this.id = id;
  }
  int IItemTerrainTileComponentEffect.id => id;
  public void visit(IItemTerrainTileComponentEffectVisitor visitor) {
    visitor.visitItemTerrainTileComponentCreateEffect(this);
  }
}

}
