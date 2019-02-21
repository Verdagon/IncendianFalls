using System;
using System.Collections.Generic;

namespace Atharia.Model {
public struct DecorativeTerrainTileComponentMutSetCreateEffect : IDecorativeTerrainTileComponentMutSetEffect {
  public readonly int id;
  public readonly DecorativeTerrainTileComponentMutSetIncarnation incarnation;
  public DecorativeTerrainTileComponentMutSetCreateEffect(
      int id,
      DecorativeTerrainTileComponentMutSetIncarnation incarnation) {
    this.id = id;
    this.incarnation = incarnation;
  }
  int IDecorativeTerrainTileComponentMutSetEffect.id => id;
  public void visit(IDecorativeTerrainTileComponentMutSetEffectVisitor visitor) {
    visitor.visitDecorativeTerrainTileComponentMutSetCreateEffect(this);
  }
}

}
