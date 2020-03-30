using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct SlowRodStrongMutSetDeleteEffect : ISlowRodStrongMutSetEffect {
  public readonly int id;
  public SlowRodStrongMutSetDeleteEffect(int id) {
    this.id = id;
  }
  int ISlowRodStrongMutSetEffect.id => id;
  public void visitISlowRodStrongMutSetEffect(ISlowRodStrongMutSetEffectVisitor visitor) {
    visitor.visitSlowRodStrongMutSetDeleteEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitSlowRodStrongMutSetEffect(this);
  }
  public bool isSubtractive() { return true; }
}

}
