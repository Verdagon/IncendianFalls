using System;
using System.Collections.Generic;

namespace Atharia.Model {
public struct UpStaircaseTerrainTileComponentMutSetAddEffect : IUpStaircaseTerrainTileComponentMutSetEffect {
  public readonly int id;
  public readonly int elementId;
  public UpStaircaseTerrainTileComponentMutSetAddEffect(int id, int elementId) {
    this.id = id;
    this.elementId = elementId;
  }
  int IUpStaircaseTerrainTileComponentMutSetEffect.id => id;
  public void visit(IUpStaircaseTerrainTileComponentMutSetEffectVisitor visitor) {
    visitor.visitUpStaircaseTerrainTileComponentMutSetAddEffect(this);
  }
}

}
