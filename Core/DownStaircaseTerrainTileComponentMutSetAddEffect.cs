using System;
using System.Collections.Generic;

namespace Atharia.Model {
public struct DownStaircaseTerrainTileComponentMutSetAddEffect : IDownStaircaseTerrainTileComponentMutSetEffect {
  public readonly int id;
  public readonly int elementId;
  public DownStaircaseTerrainTileComponentMutSetAddEffect(int id, int elementId) {
    this.id = id;
    this.elementId = elementId;
  }
  int IDownStaircaseTerrainTileComponentMutSetEffect.id => id;
  public void visit(IDownStaircaseTerrainTileComponentMutSetEffectVisitor visitor) {
    visitor.visitDownStaircaseTerrainTileComponentMutSetAddEffect(this);
  }
}

}
