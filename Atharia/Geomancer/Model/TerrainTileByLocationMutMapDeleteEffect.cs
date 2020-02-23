using System;
using System.Collections;

using System.Collections.Generic;

namespace Geomancer.Model {

public struct TerrainTileByLocationMutMapDeleteEffect : ITerrainTileByLocationMutMapEffect {
  public readonly int id;
  public TerrainTileByLocationMutMapDeleteEffect(int id) {
    this.id = id;
  }
  int ITerrainTileByLocationMutMapEffect.id => id;
  public void visit(ITerrainTileByLocationMutMapEffectVisitor visitor) {
    visitor.visitTerrainTileByLocationMutMapDeleteEffect(this);
  }
}

}
