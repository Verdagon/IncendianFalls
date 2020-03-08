using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct FireTTCDeleteEffect : IFireTTCEffect {
  public readonly int id;
  public FireTTCDeleteEffect(int id) {
    this.id = id;
  }
  int IFireTTCEffect.id => id;
  public void visit(IFireTTCEffectVisitor visitor) {
    visitor.visitFireTTCDeleteEffect(this);
  }
}

}