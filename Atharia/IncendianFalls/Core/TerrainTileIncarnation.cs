using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class TerrainTileIncarnation : ITerrainTileEffectVisitor {
  public readonly int events;
  public  int elevation;
  public readonly int components;
  public TerrainTileIncarnation(
      int events,
      int elevation,
      int components) {
    this.events = events;
    this.elevation = elevation;
    this.components = components;
  }
  public TerrainTileIncarnation Copy() {
    return new TerrainTileIncarnation(
events,
elevation,
components    );
  }

  public void visitTerrainTileCreateEffect(TerrainTileCreateEffect e) {}
  public void visitTerrainTileDeleteEffect(TerrainTileDeleteEffect e) {}

public void visitTerrainTileSetElevationEffect(TerrainTileSetElevationEffect e) { this.elevation = e.newValue; }

  public void ApplyEffect(ITerrainTileEffect effect) { effect.visitITerrainTileEffect(this); }
}

}
