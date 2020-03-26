using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct BloodTTCMutSetRemoveEffect : IBloodTTCMutSetEffect {
  public readonly int id;
  public readonly int element;
  public BloodTTCMutSetRemoveEffect(int id, int element) {
    this.id = id;
    this.element = element;
  }
  int IBloodTTCMutSetEffect.id => id;
  public void visitIBloodTTCMutSetEffect(IBloodTTCMutSetEffectVisitor visitor) {
    visitor.visitBloodTTCMutSetRemoveEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitBloodTTCMutSetEffect(this);
  }
}

}
