using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct DownStaircaseTerrainTileComponentDeleteEffect : IDownStaircaseTerrainTileComponentEffect {
  public readonly int id;
  public DownStaircaseTerrainTileComponentDeleteEffect(int id) {
    this.id = id;
  }
  int IDownStaircaseTerrainTileComponentEffect.id => id;
  public void visit(IDownStaircaseTerrainTileComponentEffectVisitor visitor) {
    visitor.visitDownStaircaseTerrainTileComponentDeleteEffect(this);
  }
}
       
}
