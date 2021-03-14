using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct OnFireTTCMutSetDeleteEffect : IOnFireTTCMutSetEffect {
  public readonly int id;
  public OnFireTTCMutSetDeleteEffect(int id) {
    this.id = id;
  }
  int IOnFireTTCMutSetEffect.id => id;
  public void visitIOnFireTTCMutSetEffect(IOnFireTTCMutSetEffectVisitor visitor) {
    visitor.visitOnFireTTCMutSetDeleteEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitOnFireTTCMutSetEffect(this);
  }
  public bool isSubtractive() { return true; }
}

}
