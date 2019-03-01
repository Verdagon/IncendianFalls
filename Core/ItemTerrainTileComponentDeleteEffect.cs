using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct ItemTerrainTileComponentDeleteEffect : IItemTerrainTileComponentEffect {
  public readonly int id;
  public ItemTerrainTileComponentDeleteEffect(int id) {
    this.id = id;
  }
  int IItemTerrainTileComponentEffect.id => id;
  public void visit(IItemTerrainTileComponentEffectVisitor visitor) {
    visitor.visitItemTerrainTileComponentDeleteEffect(this);
  }
}

}
