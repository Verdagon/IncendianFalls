using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct FireTTCMutSetRemoveEffect : IFireTTCMutSetEffect {
  public readonly int id;
  public readonly int element;
  public FireTTCMutSetRemoveEffect(int id, int element) {
    this.id = id;
    this.element = element;
  }
  int IFireTTCMutSetEffect.id => id;
  public void visitIFireTTCMutSetEffect(IFireTTCMutSetEffectVisitor visitor) {
    visitor.visitFireTTCMutSetRemoveEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitFireTTCMutSetEffect(this);
  }
  public bool isSubtractive() { return true; }
}

}
