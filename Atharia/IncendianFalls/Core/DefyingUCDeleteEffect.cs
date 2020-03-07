using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct DefyingUCDeleteEffect : IDefyingUCEffect {
  public readonly int id;
  public DefyingUCDeleteEffect(int id) {
    this.id = id;
  }
  int IDefyingUCEffect.id => id;
  public void visit(IDefyingUCEffectVisitor visitor) {
    visitor.visitDefyingUCDeleteEffect(this);
  }
}

}
