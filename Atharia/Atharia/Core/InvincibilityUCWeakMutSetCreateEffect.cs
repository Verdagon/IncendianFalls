using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct InvincibilityUCWeakMutSetCreateEffect : IInvincibilityUCWeakMutSetEffect {
  public readonly int id;
  public InvincibilityUCWeakMutSetCreateEffect(int id) {
    this.id = id;
  }
  int IInvincibilityUCWeakMutSetEffect.id => id;
  public void visitIInvincibilityUCWeakMutSetEffect(IInvincibilityUCWeakMutSetEffectVisitor visitor) {
    visitor.visitInvincibilityUCWeakMutSetCreateEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitInvincibilityUCWeakMutSetEffect(this);
  }
  public bool isSubtractive() { return false; }
}

}
