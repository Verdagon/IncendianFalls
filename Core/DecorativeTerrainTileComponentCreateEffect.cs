using System;
using System.Collections.Generic;

namespace Atharia.Model {

public struct DecorativeTerrainTileComponentCreateEffect : IDecorativeTerrainTileComponentEffect {
  public readonly int id;
  public readonly DecorativeTerrainTileComponentIncarnation incarnation;
  public DecorativeTerrainTileComponentCreateEffect(
      int id,
      DecorativeTerrainTileComponentIncarnation incarnation) {
    this.id = id;
    this.incarnation = incarnation;
  }
  int IDecorativeTerrainTileComponentEffect.id => id;
  public void visit(IDecorativeTerrainTileComponentEffectVisitor visitor) {
    visitor.visitDecorativeTerrainTileComponentCreateEffect(this);
  }
}
       
}
