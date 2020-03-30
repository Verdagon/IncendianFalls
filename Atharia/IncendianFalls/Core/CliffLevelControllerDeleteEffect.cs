using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct CliffLevelControllerDeleteEffect : ICliffLevelControllerEffect {
  public readonly int id;
  public CliffLevelControllerDeleteEffect(int id) {
    this.id = id;
  }
  int ICliffLevelControllerEffect.id => id;
  public void visitICliffLevelControllerEffect(ICliffLevelControllerEffectVisitor visitor) {
    visitor.visitCliffLevelControllerDeleteEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitCliffLevelControllerEffect(this);
  }
  public bool isSubtractive() { return true; }
}

}
