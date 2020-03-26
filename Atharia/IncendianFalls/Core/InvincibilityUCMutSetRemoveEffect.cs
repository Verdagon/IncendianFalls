using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct InvincibilityUCMutSetRemoveEffect : IInvincibilityUCMutSetEffect {
  public readonly int id;
  public readonly int element;
  public InvincibilityUCMutSetRemoveEffect(int id, int element) {
    this.id = id;
    this.element = element;
  }
  int IInvincibilityUCMutSetEffect.id => id;
  public void visitIInvincibilityUCMutSetEffect(IInvincibilityUCMutSetEffectVisitor visitor) {
    visitor.visitInvincibilityUCMutSetRemoveEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitInvincibilityUCMutSetEffect(this);
  }
}

}
