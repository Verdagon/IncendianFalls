using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct PentagonalCaveLevelControllerDeleteEffect : IPentagonalCaveLevelControllerEffect {
  public readonly int id;
  public PentagonalCaveLevelControllerDeleteEffect(int id) {
    this.id = id;
  }
  int IPentagonalCaveLevelControllerEffect.id => id;
  public void visitIPentagonalCaveLevelControllerEffect(IPentagonalCaveLevelControllerEffectVisitor visitor) {
    visitor.visitPentagonalCaveLevelControllerDeleteEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitPentagonalCaveLevelControllerEffect(this);
  }
  public bool isSubtractive() { return true; }
}

}
