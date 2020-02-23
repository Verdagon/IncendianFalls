using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct CounteringUCDeleteEffect : ICounteringUCEffect {
  public readonly int id;
  public CounteringUCDeleteEffect(int id) {
    this.id = id;
  }
  int ICounteringUCEffect.id => id;
  public void visit(ICounteringUCEffectVisitor visitor) {
    visitor.visitCounteringUCDeleteEffect(this);
  }
}

}
