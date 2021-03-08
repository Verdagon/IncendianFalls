using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct CliffLevelControllerCreateEffect : ICliffLevelControllerEffect {
  public readonly int id;
  public readonly CliffLevelControllerIncarnation incarnation;
  public CliffLevelControllerCreateEffect(int id, CliffLevelControllerIncarnation incarnation) {
    this.id = id;
    this.incarnation = incarnation;
  }
  int ICliffLevelControllerEffect.id => id;
  public void visitICliffLevelControllerEffect(ICliffLevelControllerEffectVisitor visitor) {
    visitor.visitCliffLevelControllerCreateEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitCliffLevelControllerEffect(this);
  }
  public bool isSubtractive() { return false; }
}

}
