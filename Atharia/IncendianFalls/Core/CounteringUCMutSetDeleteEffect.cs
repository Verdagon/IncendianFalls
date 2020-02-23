using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct CounteringUCMutSetDeleteEffect : ICounteringUCMutSetEffect {
  public readonly int id;
  public CounteringUCMutSetDeleteEffect(int id) {
    this.id = id;
  }
  int ICounteringUCMutSetEffect.id => id;
  public void visit(ICounteringUCMutSetEffectVisitor visitor) {
    visitor.visitCounteringUCMutSetDeleteEffect(this);
  }
}

}
