using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct InvincibilityUCDeleteEffect : IInvincibilityUCEffect {
  public readonly int id;
  public InvincibilityUCDeleteEffect(int id) {
    this.id = id;
  }
  int IInvincibilityUCEffect.id => id;
  public void visitIInvincibilityUCEffect(IInvincibilityUCEffectVisitor visitor) {
    visitor.visitInvincibilityUCDeleteEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitInvincibilityUCEffect(this);
  }
}

}
