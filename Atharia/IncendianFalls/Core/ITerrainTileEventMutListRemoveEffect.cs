using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct ITerrainTileEventMutListRemoveEffect : IITerrainTileEventMutListEffect {
  public readonly int id;
  public readonly int index;
  public ITerrainTileEventMutListRemoveEffect(int id, int index) {
    this.id = id;
    this.index = index;
  }
  int IITerrainTileEventMutListEffect.id => id;
  public void visitIITerrainTileEventMutListEffect(IITerrainTileEventMutListEffectVisitor visitor) {
    visitor.visitITerrainTileEventMutListRemoveEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitITerrainTileEventMutListEffect(this);
  }
}

}
