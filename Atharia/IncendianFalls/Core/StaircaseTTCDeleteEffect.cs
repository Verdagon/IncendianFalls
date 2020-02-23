using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct StaircaseTTCDeleteEffect : IStaircaseTTCEffect {
  public readonly int id;
  public StaircaseTTCDeleteEffect(int id) {
    this.id = id;
  }
  int IStaircaseTTCEffect.id => id;
  public void visit(IStaircaseTTCEffectVisitor visitor) {
    visitor.visitStaircaseTTCDeleteEffect(this);
  }
}

}
