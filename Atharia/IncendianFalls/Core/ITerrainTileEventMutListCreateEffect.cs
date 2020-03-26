using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct ITerrainTileEventMutListCreateEffect : IITerrainTileEventMutListEffect {
  public readonly int id;
  public ITerrainTileEventMutListCreateEffect(int id) {
    this.id = id;
  }
  int IITerrainTileEventMutListEffect.id => id;
  public void visitIITerrainTileEventMutListEffect(IITerrainTileEventMutListEffectVisitor visitor) {
    visitor.visitITerrainTileEventMutListCreateEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitITerrainTileEventMutListEffect(this);
  }
}

}
