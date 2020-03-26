using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class TerrainIncarnation : ITerrainEffectVisitor {
  public  Pattern pattern;
  public readonly float elevationStepHeight;
  public readonly int tiles;
  public TerrainIncarnation(
      Pattern pattern,
      float elevationStepHeight,
      int tiles) {
    this.pattern = pattern;
    this.elevationStepHeight = elevationStepHeight;
    this.tiles = tiles;
  }
  public TerrainIncarnation Copy() {
    return new TerrainIncarnation(
pattern,
elevationStepHeight,
tiles    );
  }

  public void visitTerrainCreateEffect(TerrainCreateEffect e) {}
  public void visitTerrainDeleteEffect(TerrainDeleteEffect e) {}
public void visitTerrainSetPatternEffect(TerrainSetPatternEffect e) { this.pattern = e.newValue; }


  public void ApplyEffect(ITerrainEffect effect) { effect.visitITerrainEffect(this); }
}

}
