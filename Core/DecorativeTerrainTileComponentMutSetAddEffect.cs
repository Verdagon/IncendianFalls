using System;
using System.Collections.Generic;

namespace Atharia.Model {
public struct DecorativeTerrainTileComponentMutSetAddEffect : IDecorativeTerrainTileComponentMutSetEffect {
  public readonly int id;
  public readonly int elementId;
  public DecorativeTerrainTileComponentMutSetAddEffect(int id, int elementId) {
    this.id = id;
    this.elementId = elementId;
  }
  int IDecorativeTerrainTileComponentMutSetEffect.id => id;
  public void visit(IDecorativeTerrainTileComponentMutSetEffectVisitor visitor) {
    visitor.visitDecorativeTerrainTileComponentMutSetAddEffect(this);
  }
}

}
