using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct SquareCaveLevelControllerCreateEffect : ISquareCaveLevelControllerEffect {
  public readonly int id;
  public readonly SquareCaveLevelControllerIncarnation incarnation;
  public SquareCaveLevelControllerCreateEffect(int id, SquareCaveLevelControllerIncarnation incarnation) {
    this.id = id;
    this.incarnation = incarnation;
  }
  int ISquareCaveLevelControllerEffect.id => id;
  public void visitISquareCaveLevelControllerEffect(ISquareCaveLevelControllerEffectVisitor visitor) {
    visitor.visitSquareCaveLevelControllerCreateEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitSquareCaveLevelControllerEffect(this);
  }
  public bool isSubtractive() { return false; }
}

}
