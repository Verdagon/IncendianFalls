using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct DecorativeTTCDeleteEffect : IDecorativeTTCEffect {
  public readonly int id;
  public DecorativeTTCDeleteEffect(int id) {
    this.id = id;
  }
  int IDecorativeTTCEffect.id => id;
  public void visit(IDecorativeTTCEffectVisitor visitor) {
    visitor.visitDecorativeTTCDeleteEffect(this);
  }
}

}
