using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct InvincibilityUCMutSetAddEffect : IInvincibilityUCMutSetEffect {
  public readonly int id;
  public readonly int element;
  public InvincibilityUCMutSetAddEffect(int id, int element) {
    this.id = id;
    this.element = element;
  }
  int IInvincibilityUCMutSetEffect.id => id;
  public void visitIInvincibilityUCMutSetEffect(IInvincibilityUCMutSetEffectVisitor visitor) {
    visitor.visitInvincibilityUCMutSetAddEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitInvincibilityUCMutSetEffect(this);
  }
  public bool isSubtractive() { return false; }
}

}
