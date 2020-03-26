using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct TerrainTileByLocationMutMapAddEffect : ITerrainTileByLocationMutMapEffect {
  public readonly int id;
  public readonly Location key;
  public readonly int value;
  public TerrainTileByLocationMutMapAddEffect(int id, Location key, int value) {
    this.id = id;
    this.key = key;
    this.value = value;
  }
  int ITerrainTileByLocationMutMapEffect.id => id;
  public void visitITerrainTileByLocationMutMapEffect(ITerrainTileByLocationMutMapEffectVisitor visitor) {
    visitor.visitTerrainTileByLocationMutMapAddEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitTerrainTileByLocationMutMapEffect(this);
  }
}

}
