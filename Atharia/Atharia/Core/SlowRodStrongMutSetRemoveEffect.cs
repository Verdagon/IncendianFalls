using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct SlowRodStrongMutSetRemoveEffect : ISlowRodStrongMutSetEffect {
  public readonly int id;
  public readonly int element;
  public SlowRodStrongMutSetRemoveEffect(int id, int element) {
    this.id = id;
    this.element = element;
  }
  int ISlowRodStrongMutSetEffect.id => id;
  public void visitISlowRodStrongMutSetEffect(ISlowRodStrongMutSetEffectVisitor visitor) {
    visitor.visitSlowRodStrongMutSetRemoveEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitSlowRodStrongMutSetEffect(this);
  }
  public bool isSubtractive() { return true; }
}

}
