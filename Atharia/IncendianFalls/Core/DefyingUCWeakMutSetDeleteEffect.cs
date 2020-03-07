using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct DefyingUCWeakMutSetDeleteEffect : IDefyingUCWeakMutSetEffect {
  public readonly int id;
  public DefyingUCWeakMutSetDeleteEffect(int id) {
    this.id = id;
  }
  int IDefyingUCWeakMutSetEffect.id => id;
  public void visit(IDefyingUCWeakMutSetEffectVisitor visitor) {
    visitor.visitDefyingUCWeakMutSetDeleteEffect(this);
  }
}

}
