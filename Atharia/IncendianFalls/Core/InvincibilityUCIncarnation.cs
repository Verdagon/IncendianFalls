using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class InvincibilityUCIncarnation : IInvincibilityUCEffectVisitor {
  public InvincibilityUCIncarnation(
) {
  }
  public InvincibilityUCIncarnation Copy() {
    return new InvincibilityUCIncarnation(
    );
  }

  public void visitInvincibilityUCCreateEffect(InvincibilityUCCreateEffect e) {}
  public void visitInvincibilityUCDeleteEffect(InvincibilityUCDeleteEffect e) {}

  public void ApplyEffect(IInvincibilityUCEffect effect) { effect.visitIInvincibilityUCEffect(this); }
}

}
