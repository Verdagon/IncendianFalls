using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct BaseMovementTimeUCMutSetAddEffect : IBaseMovementTimeUCMutSetEffect {
  public readonly int id;
  public readonly int element;
  public BaseMovementTimeUCMutSetAddEffect(int id, int element) {
    this.id = id;
    this.element = element;
  }
  int IBaseMovementTimeUCMutSetEffect.id => id;
  public void visitIBaseMovementTimeUCMutSetEffect(IBaseMovementTimeUCMutSetEffectVisitor visitor) {
    visitor.visitBaseMovementTimeUCMutSetAddEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitBaseMovementTimeUCMutSetEffect(this);
  }
  public bool isSubtractive() { return false; }
}

}
