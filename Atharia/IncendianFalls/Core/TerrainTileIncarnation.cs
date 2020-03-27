using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class TerrainTileIncarnation : ITerrainTileEffectVisitor {
  public  ITerrainTileEvent evvent;
  public  int elevation;
  public readonly int components;
  public TerrainTileIncarnation(
      ITerrainTileEvent evvent,
      int elevation,
      int components) {
    this.evvent = evvent;
    this.elevation = elevation;
    this.components = components;
  }
  public TerrainTileIncarnation Copy() {
    return new TerrainTileIncarnation(
evvent,
elevation,
components    );
  }

  public void visitTerrainTileCreateEffect(TerrainTileCreateEffect e) {}
  public void visitTerrainTileDeleteEffect(TerrainTileDeleteEffect e) {}
public void visitTerrainTileSetEvventEffect(TerrainTileSetEvventEffect e) { this.evvent = e.newValue; }
public void visitTerrainTileSetElevationEffect(TerrainTileSetElevationEffect e) { this.elevation = e.newValue; }

  public void ApplyEffect(ITerrainTileEffect effect) { effect.visitITerrainTileEffect(this); }
}

}
