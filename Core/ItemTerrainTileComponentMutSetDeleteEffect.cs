using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct ItemTerrainTileComponentMutSetDeleteEffect : IItemTerrainTileComponentMutSetEffect {
  public readonly int id;
  public ItemTerrainTileComponentMutSetDeleteEffect(int id) {
    this.id = id;
  }
  int IItemTerrainTileComponentMutSetEffect.id => id;
  public void visit(IItemTerrainTileComponentMutSetEffectVisitor visitor) {
    visitor.visitItemTerrainTileComponentMutSetDeleteEffect(this);
  }
}

}
