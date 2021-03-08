using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct BaseOffenseUCMutSetAddEffect : IBaseOffenseUCMutSetEffect {
  public readonly int id;
  public readonly int element;
  public BaseOffenseUCMutSetAddEffect(int id, int element) {
    this.id = id;
    this.element = element;
  }
  int IBaseOffenseUCMutSetEffect.id => id;
  public void visitIBaseOffenseUCMutSetEffect(IBaseOffenseUCMutSetEffectVisitor visitor) {
    visitor.visitBaseOffenseUCMutSetAddEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitBaseOffenseUCMutSetEffect(this);
  }
  public bool isSubtractive() { return false; }
}

}
