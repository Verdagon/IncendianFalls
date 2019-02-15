using System;
using System.Collections.Generic;

namespace Atharia.Model {

public struct TerrainTileByLocationMutMapCreateEffect : ITerrainTileByLocationMutMapEffect {
  public readonly int id;
  public readonly TerrainTileByLocationMutMapIncarnation incarnation;
  public TerrainTileByLocationMutMapCreateEffect(
      int id,
      TerrainTileByLocationMutMapIncarnation incarnation) {
    this.id = id;
    this.incarnation = incarnation;
  }
  int ITerrainTileByLocationMutMapEffect.id => id;
  public void visit(ITerrainTileByLocationMutMapEffectVisitor visitor) {
    visitor.visitTerrainTileByLocationMutMapCreateEffect(this);
  }
}

}
