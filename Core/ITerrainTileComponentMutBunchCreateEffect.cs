using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct ITerrainTileComponentMutBunchCreateEffect : IITerrainTileComponentMutBunchEffect {
  public readonly int id;
  public ITerrainTileComponentMutBunchCreateEffect(int id) {
    this.id = id;
  }
  int IITerrainTileComponentMutBunchEffect.id => id;
  public void visit(IITerrainTileComponentMutBunchEffectVisitor visitor) {
    visitor.visitITerrainTileComponentMutBunchCreateEffect(this);
  }
}

}
