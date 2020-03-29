using System;
using System.Collections;

using System.Collections.Generic;

namespace Geomancer.Model {
public class TerrainTileIncarnation : ITerrainTileEffectVisitor {
  public  int elevation;
  public readonly int members;
  public TerrainTileIncarnation(
      int elevation,
      int members) {
    this.elevation = elevation;
    this.members = members;
  }
  public TerrainTileIncarnation Copy() {
    return new TerrainTileIncarnation(
elevation,
members    );
  }

  public void visitTerrainTileCreateEffect(TerrainTileCreateEffect e) {}
  public void visitTerrainTileDeleteEffect(TerrainTileDeleteEffect e) {}
public void visitTerrainTileSetElevationEffect(TerrainTileSetElevationEffect e) { this.elevation = e.newValue; }

  public void ApplyEffect(ITerrainTileEffect effect) { effect.visitITerrainTileEffect(this); }
}

}
