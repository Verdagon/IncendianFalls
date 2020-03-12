using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct TerrainTileWeakMutSetCreateEffect : ITerrainTileWeakMutSetEffect {
  public readonly int id;
  public TerrainTileWeakMutSetCreateEffect(int id) {
    this.id = id;
  }
  int ITerrainTileWeakMutSetEffect.id => id;
  public void visit(ITerrainTileWeakMutSetEffectVisitor visitor) {
    visitor.visitTerrainTileWeakMutSetCreateEffect(this);
  }
}

}
