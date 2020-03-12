using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct ITerrainTileEventMutListRemoveEffect : IITerrainTileEventMutListEffect {
  public readonly int id;
  public readonly int elementId;
  public ITerrainTileEventMutListRemoveEffect(int id, int elementId) {
    this.id = id;
    this.elementId = elementId;
  }
  int IITerrainTileEventMutListEffect.id => id;
  public void visit(IITerrainTileEventMutListEffectVisitor visitor) {
    visitor.visitITerrainTileEventMutListRemoveEffect(this);
  }
}

}
