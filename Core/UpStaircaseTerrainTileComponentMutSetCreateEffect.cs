using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct UpStaircaseTerrainTileComponentMutSetCreateEffect : IUpStaircaseTerrainTileComponentMutSetEffect {
  public readonly int id;
  public readonly UpStaircaseTerrainTileComponentMutSetIncarnation incarnation;
  public UpStaircaseTerrainTileComponentMutSetCreateEffect(
      int id,
      UpStaircaseTerrainTileComponentMutSetIncarnation incarnation) {
    this.id = id;
    this.incarnation = incarnation;
  }
  int IUpStaircaseTerrainTileComponentMutSetEffect.id => id;
  public void visit(IUpStaircaseTerrainTileComponentMutSetEffectVisitor visitor) {
    visitor.visitUpStaircaseTerrainTileComponentMutSetCreateEffect(this);
  }
}

}
