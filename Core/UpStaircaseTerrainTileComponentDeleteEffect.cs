using System;
using System.Collections.Generic;

namespace Atharia.Model {

public struct UpStaircaseTerrainTileComponentDeleteEffect : IUpStaircaseTerrainTileComponentEffect {
  public readonly int id;
  public UpStaircaseTerrainTileComponentDeleteEffect(int id) {
    this.id = id;
  }
  int IUpStaircaseTerrainTileComponentEffect.id => id;
  public void visit(IUpStaircaseTerrainTileComponentEffectVisitor visitor) {
    visitor.visitUpStaircaseTerrainTileComponentDeleteEffect(this);
  }
}
       
}
