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
  public void visitIWarperTTCEffect(IWarperTTCEffectVisitor visitor) {
    visitor.visitWarperTTCDeleteEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitWarperTTCEffect(this);
  }
  public bool isSubtractive() { return true; }
}

}
