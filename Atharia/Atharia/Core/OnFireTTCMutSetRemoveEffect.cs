using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct OnFireTTCMutSetRemoveEffect : IOnFireTTCMutSetEffect {
  public readonly int id;
  public readonly int element;
  public OnFireTTCMutSetRemoveEffect(int id, int element) {
    this.id = id;
    this.element = element;
  }
  int IOnFireTTCMutSetEffect.id => id;
  public void visitIOnFireTTCMutSetEffect(IOnFireTTCMutSetEffectVisitor visitor) {
    visitor.visitOnFireTTCMutSetRemoveEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitOnFireTTCMutSetEffect(this);
  }
  public bool isSubtractive() { return true; }
}

}
