using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct DecorativeTerrainTileComponentDeleteEffect : IDecorativeTerrainTileComponentEffect {
  public readonly int id;
  public DecorativeTerrainTileComponentDeleteEffect(int id) {
    this.id = id;
  }
  int IDecorativeTerrainTileComponentEffect.id => id;
  public void visit(IDecorativeTerrainTileComponentEffectVisitor visitor) {
    visitor.visitDecorativeTerrainTileComponentDeleteEffect(this);
  }
}
       
}
