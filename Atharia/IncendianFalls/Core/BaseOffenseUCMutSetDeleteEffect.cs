using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct BaseOffenseUCMutSetDeleteEffect : IBaseOffenseUCMutSetEffect {
  public readonly int id;
  public BaseOffenseUCMutSetDeleteEffect(int id) {
    this.id = id;
  }
  int IBaseOffenseUCMutSetEffect.id => id;
  public void visit(IBaseOffenseUCMutSetEffectVisitor visitor) {
    visitor.visitBaseOffenseUCMutSetDeleteEffect(this);
  }
}

}
