using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct BaseDefenseUCMutSetRemoveEffect : IBaseDefenseUCMutSetEffect {
  public readonly int id;
  public readonly int element;
  public BaseDefenseUCMutSetRemoveEffect(int id, int element) {
    this.id = id;
    this.element = element;
  }
  int IBaseDefenseUCMutSetEffect.id => id;
  public void visitIBaseDefenseUCMutSetEffect(IBaseDefenseUCMutSetEffectVisitor visitor) {
    visitor.visitBaseDefenseUCMutSetRemoveEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitBaseDefenseUCMutSetEffect(this);
  }
  public bool isSubtractive() { return true; }
}

}
