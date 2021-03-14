using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct OnFireTTCDeleteEffect : IOnFireTTCEffect {
  public readonly int id;
  public OnFireTTCDeleteEffect(int id) {
    this.id = id;
  }
  int IOnFireTTCEffect.id => id;
  public void visitIOnFireTTCEffect(IOnFireTTCEffectVisitor visitor) {
    visitor.visitOnFireTTCDeleteEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitOnFireTTCEffect(this);
  }
  public bool isSubtractive() { return true; }
}

}
