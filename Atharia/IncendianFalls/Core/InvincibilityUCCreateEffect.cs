using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct InvincibilityUCCreateEffect : IInvincibilityUCEffect {
  public readonly int id;
  public readonly InvincibilityUCIncarnation incarnation;
  public InvincibilityUCCreateEffect(int id, InvincibilityUCIncarnation incarnation) {
    this.id = id;
    this.incarnation = incarnation;
  }
  int IInvincibilityUCEffect.id => id;
  public void visitIInvincibilityUCEffect(IInvincibilityUCEffectVisitor visitor) {
    visitor.visitInvincibilityUCCreateEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitInvincibilityUCEffect(this);
  }
}

}
