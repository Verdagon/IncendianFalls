using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct OnFireTTCMutSetCreateEffect : IOnFireTTCMutSetEffect {
  public readonly int id;
  public OnFireTTCMutSetCreateEffect(int id) {
    this.id = id;
  }
  int IOnFireTTCMutSetEffect.id => id;
  public void visitIOnFireTTCMutSetEffect(IOnFireTTCMutSetEffectVisitor visitor) {
    visitor.visitOnFireTTCMutSetCreateEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitOnFireTTCMutSetEffect(this);
  }
  public bool isSubtractive() { return false; }
}

}
