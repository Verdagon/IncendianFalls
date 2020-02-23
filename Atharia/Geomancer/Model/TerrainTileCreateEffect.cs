using System;
using System.Collections;

using System.Collections.Generic;

namespace Geomancer.Model {

public struct TerrainTileCreateEffect : ITerrainTileEffect {
  public readonly int id;
  public TerrainTileCreateEffect(int id) {
    this.id = id;
  }
  int ITerrainTileEffect.id => id;
  public void visit(ITerrainTileEffectVisitor visitor) {
    visitor.visitTerrainTileCreateEffect(this);
  }
}

}
