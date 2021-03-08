using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct SlowRodMutSetAddEffect : ISlowRodMutSetEffect {
  public readonly int id;
  public readonly int element;
  public SlowRodMutSetAddEffect(int id, int element) {
    this.id = id;
    this.element = element;
  }
  int ISlowRodMutSetEffect.id => id;
  public void visitISlowRodMutSetEffect(ISlowRodMutSetEffectVisitor visitor) {
    visitor.visitSlowRodMutSetAddEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitSlowRodMutSetEffect(this);
  }
  public bool isSubtractive() { return false; }
}

}
