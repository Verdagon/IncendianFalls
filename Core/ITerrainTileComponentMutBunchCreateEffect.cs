using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct ITerrainTileComponentMutBunchCreateEffect : IITerrainTileComponentMutBunchEffect {
  public readonly int id;
  public readonly ITerrainTileComponentMutBunchIncarnation incarnation;
  public ITerrainTileComponentMutBunchCreateEffect(
      int id,
      ITerrainTileComponentMutBunchIncarnation incarnation) {
    this.id = id;
    this.incarnation = incarnation;
  }
  int IITerrainTileComponentMutBunchEffect.id => id;
  public void visit(IITerrainTileComponentMutBunchEffectVisitor visitor) {
    visitor.visitITerrainTileComponentMutBunchCreateEffect(this);
  }
}
       
}
