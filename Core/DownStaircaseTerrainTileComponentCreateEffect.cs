using System;
using System.Collections.Generic;

namespace Atharia.Model {

public struct DownStaircaseTerrainTileComponentCreateEffect : IDownStaircaseTerrainTileComponentEffect {
  public readonly int id;
  public readonly DownStaircaseTerrainTileComponentIncarnation incarnation;
  public DownStaircaseTerrainTileComponentCreateEffect(
      int id,
      DownStaircaseTerrainTileComponentIncarnation incarnation) {
    this.id = id;
    this.incarnation = incarnation;
  }
  int IDownStaircaseTerrainTileComponentEffect.id => id;
  public void visit(IDownStaircaseTerrainTileComponentEffectVisitor visitor) {
    visitor.visitDownStaircaseTerrainTileComponentCreateEffect(this);
  }
}
       
}
