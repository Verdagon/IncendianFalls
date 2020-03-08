using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct SlowRodDeleteEffect : ISlowRodEffect {
  public readonly int id;
  public SlowRodDeleteEffect(int id) {
    this.id = id;
  }
  int ISlowRodEffect.id => id;
  public void visit(ISlowRodEffectVisitor visitor) {
    visitor.visitSlowRodDeleteEffect(this);
  }
}

}
