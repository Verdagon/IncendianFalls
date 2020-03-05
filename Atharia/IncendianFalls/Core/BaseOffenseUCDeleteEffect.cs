using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct BaseOffenseUCDeleteEffect : IBaseOffenseUCEffect {
  public readonly int id;
  public BaseOffenseUCDeleteEffect(int id) {
    this.id = id;
  }
  int IBaseOffenseUCEffect.id => id;
  public void visit(IBaseOffenseUCEffectVisitor visitor) {
    visitor.visitBaseOffenseUCDeleteEffect(this);
  }
}

}
