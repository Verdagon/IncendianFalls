using System;
using System.Collections.Generic;

namespace Atharia.Model {
public struct UpStaircaseTerrainTileComponentMutSetDeleteEffect : IUpStaircaseTerrainTileComponentMutSetEffect {
  public readonly int id;
  public UpStaircaseTerrainTileComponentMutSetDeleteEffect(int id) {
    this.id = id;
  }
  int IUpStaircaseTerrainTileComponentMutSetEffect.id => id;
  public void visit(IUpStaircaseTerrainTileComponentMutSetEffectVisitor visitor) {
    visitor.visitUpStaircaseTerrainTileComponentMutSetDeleteEffect(this);
  }
}

}
