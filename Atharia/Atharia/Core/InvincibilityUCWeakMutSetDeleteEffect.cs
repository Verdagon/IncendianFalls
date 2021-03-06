using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct InvincibilityUCWeakMutSetDeleteEffect : IInvincibilityUCWeakMutSetEffect {
  public readonly int id;
  public InvincibilityUCWeakMutSetDeleteEffect(int id) {
    this.id = id;
  }
  int IInvincibilityUCWeakMutSetEffect.id => id;
  public void visitIInvincibilityUCWeakMutSetEffect(IInvincibilityUCWeakMutSetEffectVisitor visitor) {
    visitor.visitInvincibilityUCWeakMutSetDeleteEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitInvincibilityUCWeakMutSetEffect(this);
  }
  public bool isSubtractive() { return true; }
}

}
