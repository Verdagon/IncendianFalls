using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct ITerrainTileEventMutListDeleteEffect : IITerrainTileEventMutListEffect {
  public readonly int id;
  public ITerrainTileEventMutListDeleteEffect(int id) {
    this.id = id;
  }
  int IITerrainTileEventMutListEffect.id => id;
  public void visit(IITerrainTileEventMutListEffectVisitor visitor) {
    visitor.visitITerrainTileEventMutListDeleteEffect(this);
  }
}

}
