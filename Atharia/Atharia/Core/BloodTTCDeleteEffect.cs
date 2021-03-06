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
  public void visitIBloodTTCEffect(IBloodTTCEffectVisitor visitor) {
    visitor.visitBloodTTCDeleteEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitBloodTTCEffect(this);
  }
  public bool isSubtractive() { return true; }
}

}
