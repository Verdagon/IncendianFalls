using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct ITerrainTileEventMutListAddEffect : IITerrainTileEventMutListEffect {
  public readonly int id;
  public readonly ITerrainTileEvent element;
  public ITerrainTileEventMutListAddEffect(int id, ITerrainTileEvent element) {
    this.id = id;
    this.element = element;
  }
  int IITerrainTileEventMutListEffect.id => id;
  public void visit(IITerrainTileEventMutListEffectVisitor visitor) {
    visitor.visitITerrainTileEventMutListAddEffect(this);
  }
}

}
