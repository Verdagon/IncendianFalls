using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct ITerrainTileComponentMutBunchDeleteEffect : IITerrainTileComponentMutBunchEffect {
  public readonly int id;
  public ITerrainTileComponentMutBunchDeleteEffect(int id) {
    this.id = id;
  }
  int IITerrainTileComponentMutBunchEffect.id => id;
  public void visit(IITerrainTileComponentMutBunchEffectVisitor visitor) {
    visitor.visitITerrainTileComponentMutBunchDeleteEffect(this);
  }
}

}
