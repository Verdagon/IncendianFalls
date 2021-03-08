using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct BaseOffenseUCMutSetCreateEffect : IBaseOffenseUCMutSetEffect {
  public readonly int id;
  public BaseOffenseUCMutSetCreateEffect(int id) {
    this.id = id;
  }
  int IBaseOffenseUCMutSetEffect.id => id;
  public void visitIBaseOffenseUCMutSetEffect(IBaseOffenseUCMutSetEffectVisitor visitor) {
    visitor.visitBaseOffenseUCMutSetCreateEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitBaseOffenseUCMutSetEffect(this);
  }
  public bool isSubtractive() { return false; }
}

}
