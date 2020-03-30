using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct InvincibilityUCMutSetCreateEffect : IInvincibilityUCMutSetEffect {
  public readonly int id;
  public InvincibilityUCMutSetCreateEffect(int id) {
    this.id = id;
  }
  int IInvincibilityUCMutSetEffect.id => id;
  public void visitIInvincibilityUCMutSetEffect(IInvincibilityUCMutSetEffectVisitor visitor) {
    visitor.visitInvincibilityUCMutSetCreateEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitInvincibilityUCMutSetEffect(this);
  }
  public bool isSubtractive() { return false; }
}

}
