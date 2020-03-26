using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct ITerrainTileEventMutListAddEffect : IITerrainTileEventMutListEffect {
  public readonly int id;
  public readonly int index;
  public readonly ITerrainTileEvent element;
  public ITerrainTileEventMutListAddEffect(int id, int index, ITerrainTileEvent element) {
    this.id = id;
    this.index = index;
    this.element = element;
  }
  int IITerrainTileEventMutListEffect.id => id;
  public void visitIITerrainTileEventMutListEffect(IITerrainTileEventMutListEffectVisitor visitor) {
    visitor.visitITerrainTileEventMutListAddEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitITerrainTileEventMutListEffect(this);
  }
}

}
