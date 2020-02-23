using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct BloodTTCDeleteEffect : IBloodTTCEffect {
  public readonly int id;
  public BloodTTCDeleteEffect(int id) {
    this.id = id;
  }
  int IBloodTTCEffect.id => id;
  public void visit(IBloodTTCEffectVisitor visitor) {
    visitor.visitBloodTTCDeleteEffect(this);
  }
}

}
