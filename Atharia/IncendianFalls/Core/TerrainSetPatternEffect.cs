using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct TerrainSetPatternEffect : ITerrainEffect {
  public readonly int id;
  public readonly Pattern newValue;
  public TerrainSetPatternEffect(
      int id,
      Pattern newValue) {
    this.id = id;
    this.newValue = newValue;
  }
  int ITerrainEffect.id => id;

  public void visitITerrainEffect(ITerrainEffectVisitor visitor) {
    visitor.visitTerrainSetPatternEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitTerrainEffect(this);
  }
}

}
