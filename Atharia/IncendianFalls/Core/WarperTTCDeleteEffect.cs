using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct WarperTTCDeleteEffect : IWarperTTCEffect {
  public readonly int id;
  public WarperTTCDeleteEffect(int id) {
    this.id = id;
  }
  int IWarperTTCEffect.id => id;
  public void visit(IWarperTTCEffectVisitor visitor) {
    visitor.visitWarperTTCDeleteEffect(this);
  }
}

}
