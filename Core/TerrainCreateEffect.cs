using System;
using System.Collections.Generic;

namespace Atharia.Model {

public struct TerrainCreateEffect : ITerrainEffect {
  public readonly int id;
  public readonly TerrainIncarnation incarnation;
  public TerrainCreateEffect(
      int id,
      TerrainIncarnation incarnation) {
    this.id = id;
    this.incarnation = incarnation;
  }
  int ITerrainEffect.id => id;
  public void visit(ITerrainEffectVisitor visitor) {
    visitor.visitTerrainCreateEffect(this);
  }
}
       
}
