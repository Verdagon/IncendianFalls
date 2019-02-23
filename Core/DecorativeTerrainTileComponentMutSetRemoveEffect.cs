using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct DecorativeTerrainTileComponentMutSetRemoveEffect : IDecorativeTerrainTileComponentMutSetEffect {
  public readonly int id;
  public readonly int elementId;
  public DecorativeTerrainTileComponentMutSetRemoveEffect(int id, int elementId) {
    this.id = id;
    this.elementId = elementId;
  }
  int IDecorativeTerrainTileComponentMutSetEffect.id => id;
  public void visit(IDecorativeTerrainTileComponentMutSetEffectVisitor visitor) {
    visitor.visitDecorativeTerrainTileComponentMutSetRemoveEffect(this);
  }
}

}
