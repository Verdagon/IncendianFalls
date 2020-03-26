using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct TerrainTileCreateEffect : ITerrainTileEffect {
  public readonly int id;
  public readonly TerrainTileIncarnation incarnation;
  public TerrainTileCreateEffect(int id, TerrainTileIncarnation incarnation) {
    this.id = id;
    this.incarnation = incarnation;
  }
  int ITerrainTileEffect.id => id;
  public void visitITerrainTileEffect(ITerrainTileEffectVisitor visitor) {
    visitor.visitTerrainTileCreateEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitTerrainTileEffect(this);
  }
}

}
