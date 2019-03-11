using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct LevelSetTimeEffect : ILevelEffect {
  public readonly int id;
  public readonly int newValue;
  public LevelSetTimeEffect(
      int id,
      int newValue) {
    this.id = id;
    this.newValue = newValue;
  }
  int ILevelEffect.id => id;

  public void visit(ILevelEffectVisitor visitor) {
    visitor.visitLevelSetTimeEffect(this);
  }
}

}
