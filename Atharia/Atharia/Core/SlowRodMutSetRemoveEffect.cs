using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct SlowRodMutSetRemoveEffect : ISlowRodMutSetEffect {
  public readonly int id;
  public readonly int element;
  public SlowRodMutSetRemoveEffect(int id, int element) {
    this.id = id;
    this.element = element;
  }
  int ISlowRodMutSetEffect.id => id;
  public void visitISlowRodMutSetEffect(ISlowRodMutSetEffectVisitor visitor) {
    visitor.visitSlowRodMutSetRemoveEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitSlowRodMutSetEffect(this);
  }
  public bool isSubtractive() { return true; }
}

}
