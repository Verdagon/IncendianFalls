using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct SlowRodMutSetDeleteEffect : ISlowRodMutSetEffect {
  public readonly int id;
  public SlowRodMutSetDeleteEffect(int id) {
    this.id = id;
  }
  int ISlowRodMutSetEffect.id => id;
  public void visitISlowRodMutSetEffect(ISlowRodMutSetEffectVisitor visitor) {
    visitor.visitSlowRodMutSetDeleteEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitSlowRodMutSetEffect(this);
  }
}

}
