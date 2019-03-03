using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct TerrainCreateEffect : ITerrainEffect {
  public readonly int id;
  public TerrainCreateEffect(int id) {
    this.id = id;
  }
  int ITerrainEffect.id => id;
  public void visit(ITerrainEffectVisitor visitor) {
    visitor.visitTerrainCreateEffect(this);
  }
}

}
