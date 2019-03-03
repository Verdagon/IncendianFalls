using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct DownStaircaseTerrainTileComponentMutSetDeleteEffect : IDownStaircaseTerrainTileComponentMutSetEffect {
  public readonly int id;
  public DownStaircaseTerrainTileComponentMutSetDeleteEffect(int id) {
    this.id = id;
  }
  int IDownStaircaseTerrainTileComponentMutSetEffect.id => id;
  public void visit(IDownStaircaseTerrainTileComponentMutSetEffectVisitor visitor) {
    visitor.visitDownStaircaseTerrainTileComponentMutSetDeleteEffect(this);
  }
}

}
