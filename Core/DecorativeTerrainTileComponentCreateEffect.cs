using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct DecorativeTerrainTileComponentCreateEffect : IDecorativeTerrainTileComponentEffect {
  public readonly int id;
  public DecorativeTerrainTileComponentCreateEffect(int id) {
    this.id = id;
  }
  int IDecorativeTerrainTileComponentEffect.id => id;
  public void visit(IDecorativeTerrainTileComponentEffectVisitor visitor) {
    visitor.visitDecorativeTerrainTileComponentCreateEffect(this);
  }
}

}
