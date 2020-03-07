using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct DefyingUCMutSetDeleteEffect : IDefyingUCMutSetEffect {
  public readonly int id;
  public DefyingUCMutSetDeleteEffect(int id) {
    this.id = id;
  }
  int IDefyingUCMutSetEffect.id => id;
  public void visit(IDefyingUCMutSetEffectVisitor visitor) {
    visitor.visitDefyingUCMutSetDeleteEffect(this);
  }
}

}
