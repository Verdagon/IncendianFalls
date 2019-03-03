using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct DownStaircaseTerrainTileComponentMutSetCreateEffect : IDownStaircaseTerrainTileComponentMutSetEffect {
  public readonly int id;
  public DownStaircaseTerrainTileComponentMutSetCreateEffect(int id) {
    this.id = id;
  }
  int IDownStaircaseTerrainTileComponentMutSetEffect.id => id;
  public void visit(IDownStaircaseTerrainTileComponentMutSetEffectVisitor visitor) {
    visitor.visitDownStaircaseTerrainTileComponentMutSetCreateEffect(this);
  }
}

}
