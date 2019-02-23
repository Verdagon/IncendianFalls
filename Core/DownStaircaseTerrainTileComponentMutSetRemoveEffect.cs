using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct DownStaircaseTerrainTileComponentMutSetRemoveEffect : IDownStaircaseTerrainTileComponentMutSetEffect {
  public readonly int id;
  public readonly int elementId;
  public DownStaircaseTerrainTileComponentMutSetRemoveEffect(int id, int elementId) {
    this.id = id;
    this.elementId = elementId;
  }
  int IDownStaircaseTerrainTileComponentMutSetEffect.id => id;
  public void visit(IDownStaircaseTerrainTileComponentMutSetEffectVisitor visitor) {
    visitor.visitDownStaircaseTerrainTileComponentMutSetRemoveEffect(this);
  }
}

}
