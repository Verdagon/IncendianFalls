using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct TerrainTileByLocationMutMapRemoveEffect : ITerrainTileByLocationMutMapEffect {
  public readonly int id;
  public readonly Location key;
  public TerrainTileByLocationMutMapRemoveEffect(int id, Location key) {
    this.id = id;
    this.key = key;
  }
  int ITerrainTileByLocationMutMapEffect.id => id;
  public void visit(ITerrainTileByLocationMutMapEffectVisitor visitor) {
    visitor.visitTerrainTileByLocationMutMapRemoveEffect(this);
  }
}

}