using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct DecorativeTerrainTileComponentMutSetDeleteEffect : IDecorativeTerrainTileComponentMutSetEffect {
  public readonly int id;
  public DecorativeTerrainTileComponentMutSetDeleteEffect(int id) {
    this.id = id;
  }
  int IDecorativeTerrainTileComponentMutSetEffect.id => id;
  public void visit(IDecorativeTerrainTileComponentMutSetEffectVisitor visitor) {
    visitor.visitDecorativeTerrainTileComponentMutSetDeleteEffect(this);
  }
}

}
