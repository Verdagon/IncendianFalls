using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct CounteringUCWeakMutSetDeleteEffect : ICounteringUCWeakMutSetEffect {
  public readonly int id;
  public CounteringUCWeakMutSetDeleteEffect(int id) {
    this.id = id;
  }
  int ICounteringUCWeakMutSetEffect.id => id;
  public void visit(ICounteringUCWeakMutSetEffectVisitor visitor) {
    visitor.visitCounteringUCWeakMutSetDeleteEffect(this);
  }
}

}
