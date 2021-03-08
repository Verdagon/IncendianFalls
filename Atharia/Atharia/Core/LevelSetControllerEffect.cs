using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct LevelSetControllerEffect : ILevelEffect {
  public readonly int id;
  public readonly int newValue;
  public LevelSetControllerEffect(
      int id,
      int newValue) {
    this.id = id;
    this.newValue = newValue;
  }
  int ILevelEffect.id => id;

  public void visitILevelEffect(ILevelEffectVisitor visitor) {
    visitor.visitLevelSetControllerEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitLevelEffect(this);
  }
  public bool isSubtractive() { return false; }
}

}
