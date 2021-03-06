using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct TerrainTileByLocationMutMapCreateEffect : ITerrainTileByLocationMutMapEffect {
  public readonly int id;
  public TerrainTileByLocationMutMapCreateEffect(int id) {
    this.id = id;
  }
  int ITerrainTileByLocationMutMapEffect.id => id;
  public void visitITerrainTileByLocationMutMapEffect(ITerrainTileByLocationMutMapEffectVisitor visitor) {
    visitor.visitTerrainTileByLocationMutMapCreateEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitTerrainTileByLocationMutMapEffect(this);
  }
  public bool isSubtractive() { return false; }
}

}
