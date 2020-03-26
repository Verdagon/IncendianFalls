using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct InvincibilityUCWeakMutSetRemoveEffect : IInvincibilityUCWeakMutSetEffect {
  public readonly int id;
  public readonly int element;
  public InvincibilityUCWeakMutSetRemoveEffect(int id, int element) {
    this.id = id;
    this.element = element;
  }
  int IInvincibilityUCWeakMutSetEffect.id => id;
  public void visitIInvincibilityUCWeakMutSetEffect(IInvincibilityUCWeakMutSetEffectVisitor visitor) {
    visitor.visitInvincibilityUCWeakMutSetRemoveEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitInvincibilityUCWeakMutSetEffect(this);
  }
}

}
