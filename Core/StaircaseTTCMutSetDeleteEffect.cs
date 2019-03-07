using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct StaircaseTTCMutSetDeleteEffect : IStaircaseTTCMutSetEffect {
  public readonly int id;
  public StaircaseTTCMutSetDeleteEffect(int id) {
    this.id = id;
  }
  int IStaircaseTTCMutSetEffect.id => id;
  public void visit(IStaircaseTTCMutSetEffectVisitor visitor) {
    visitor.visitStaircaseTTCMutSetDeleteEffect(this);
  }
}

}
