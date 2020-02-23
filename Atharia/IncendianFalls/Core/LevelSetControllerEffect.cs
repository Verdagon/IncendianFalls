using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct LevelSetControllerEffect : ILevelEffect {
  public readonly int id;
  public readonly ILevelController newValue;
  public LevelSetControllerEffect(
      int id,
      ILevelController newValue) {
    this.id = id;
    this.newValue = newValue;
  }
  int ILevelEffect.id => id;

  public void visit(ILevelEffectVisitor visitor) {
    visitor.visitLevelSetControllerEffect(this);
  }
}

}
