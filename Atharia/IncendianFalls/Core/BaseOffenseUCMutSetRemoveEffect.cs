using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct BaseOffenseUCMutSetRemoveEffect : IBaseOffenseUCMutSetEffect {
  public readonly int id;
  public readonly int element;
  public BaseOffenseUCMutSetRemoveEffect(int id, int element) {
    this.id = id;
    this.element = element;
  }
  int IBaseOffenseUCMutSetEffect.id => id;
  public void visitIBaseOffenseUCMutSetEffect(IBaseOffenseUCMutSetEffectVisitor visitor) {
    visitor.visitBaseOffenseUCMutSetRemoveEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitBaseOffenseUCMutSetEffect(this);
  }
}

}
