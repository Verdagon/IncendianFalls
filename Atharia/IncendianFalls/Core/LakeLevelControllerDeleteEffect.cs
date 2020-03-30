using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct LakeLevelControllerDeleteEffect : ILakeLevelControllerEffect {
  public readonly int id;
  public LakeLevelControllerDeleteEffect(int id) {
    this.id = id;
  }
  int ILakeLevelControllerEffect.id => id;
  public void visitILakeLevelControllerEffect(ILakeLevelControllerEffectVisitor visitor) {
    visitor.visitLakeLevelControllerDeleteEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitLakeLevelControllerEffect(this);
  }
  public bool isSubtractive() { return true; }
}

}
