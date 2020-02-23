using System;
using System.Collections;

using System.Collections.Generic;

namespace Geomancer.Model {

public struct TerrainDeleteEffect : ITerrainEffect {
  public readonly int id;
  public TerrainDeleteEffect(int id) {
    this.id = id;
  }
  int ITerrainEffect.id => id;
  public void visit(ITerrainEffectVisitor visitor) {
    visitor.visitTerrainDeleteEffect(this);
  }
}

}
