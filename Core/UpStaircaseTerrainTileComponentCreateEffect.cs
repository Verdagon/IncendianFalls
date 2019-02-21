using System;
using System.Collections.Generic;

namespace Atharia.Model {

public struct UpStaircaseTerrainTileComponentCreateEffect : IUpStaircaseTerrainTileComponentEffect {
  public readonly int id;
  public readonly UpStaircaseTerrainTileComponentIncarnation incarnation;
  public UpStaircaseTerrainTileComponentCreateEffect(
      int id,
      UpStaircaseTerrainTileComponentIncarnation incarnation) {
    this.id = id;
    this.incarnation = incarnation;
  }
  int IUpStaircaseTerrainTileComponentEffect.id => id;
  public void visit(IUpStaircaseTerrainTileComponentEffectVisitor visitor) {
    visitor.visitUpStaircaseTerrainTileComponentCreateEffect(this);
  }
}
       
}
