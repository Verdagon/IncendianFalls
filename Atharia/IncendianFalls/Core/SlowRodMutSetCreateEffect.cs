using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct SlowRodMutSetCreateEffect : ISlowRodMutSetEffect {
  public readonly int id;
  public SlowRodMutSetCreateEffect(int id) {
    this.id = id;
  }
  int ISlowRodMutSetEffect.id => id;
  public void visitISlowRodMutSetEffect(ISlowRodMutSetEffectVisitor visitor) {
    visitor.visitSlowRodMutSetCreateEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitSlowRodMutSetEffect(this);
  }
  public bool isSubtractive() { return false; }
}

}
