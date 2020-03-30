using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct SlowRodCreateEffect : ISlowRodEffect {
  public readonly int id;
  public readonly SlowRodIncarnation incarnation;
  public SlowRodCreateEffect(int id, SlowRodIncarnation incarnation) {
    this.id = id;
    this.incarnation = incarnation;
  }
  int ISlowRodEffect.id => id;
  public void visitISlowRodEffect(ISlowRodEffectVisitor visitor) {
    visitor.visitSlowRodCreateEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitSlowRodEffect(this);
  }
  public bool isSubtractive() { return false; }
}

}
