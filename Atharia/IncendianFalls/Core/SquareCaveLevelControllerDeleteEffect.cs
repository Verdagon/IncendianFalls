using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct SquareCaveLevelControllerDeleteEffect : ISquareCaveLevelControllerEffect {
  public readonly int id;
  public SquareCaveLevelControllerDeleteEffect(int id) {
    this.id = id;
  }
  int ISquareCaveLevelControllerEffect.id => id;
  public void visitISquareCaveLevelControllerEffect(ISquareCaveLevelControllerEffectVisitor visitor) {
    visitor.visitSquareCaveLevelControllerDeleteEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitSquareCaveLevelControllerEffect(this);
  }
  public bool isSubtractive() { return true; }
}

}
