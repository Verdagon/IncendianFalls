using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct LakeLevelControllerCreateEffect : ILakeLevelControllerEffect {
  public readonly int id;
  public readonly LakeLevelControllerIncarnation incarnation;
  public LakeLevelControllerCreateEffect(int id, LakeLevelControllerIncarnation incarnation) {
    this.id = id;
    this.incarnation = incarnation;
  }
  int ILakeLevelControllerEffect.id => id;
  public void visitILakeLevelControllerEffect(ILakeLevelControllerEffectVisitor visitor) {
    visitor.visitLakeLevelControllerCreateEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitLakeLevelControllerEffect(this);
  }
  public bool isSubtractive() { return false; }
}

}
