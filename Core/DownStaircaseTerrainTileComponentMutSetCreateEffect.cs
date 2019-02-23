using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct DownStaircaseTerrainTileComponentMutSetCreateEffect : IDownStaircaseTerrainTileComponentMutSetEffect {
  public readonly int id;
  public readonly DownStaircaseTerrainTileComponentMutSetIncarnation incarnation;
  public DownStaircaseTerrainTileComponentMutSetCreateEffect(
      int id,
      DownStaircaseTerrainTileComponentMutSetIncarnation incarnation) {
    this.id = id;
    this.incarnation = incarnation;
  }
  int IDownStaircaseTerrainTileComponentMutSetEffect.id => id;
  public void visit(IDownStaircaseTerrainTileComponentMutSetEffectVisitor visitor) {
    visitor.visitDownStaircaseTerrainTileComponentMutSetCreateEffect(this);
  }
}

}
