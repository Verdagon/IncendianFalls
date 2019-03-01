using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct DecorativeTerrainTileComponentMutSetCreateEffect : IDecorativeTerrainTileComponentMutSetEffect {
  public readonly int id;
  public DecorativeTerrainTileComponentMutSetCreateEffect(int id) {
    this.id = id;
  }
  int IDecorativeTerrainTileComponentMutSetEffect.id => id;
  public void visit(IDecorativeTerrainTileComponentMutSetEffectVisitor visitor) {
    visitor.visitDecorativeTerrainTileComponentMutSetCreateEffect(this);
  }
}

}
