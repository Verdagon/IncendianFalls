using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct InvincibilityUCCreateEffect : IInvincibilityUCEffect {
  public readonly int id;
  public InvincibilityUCCreateEffect(int id) {
    this.id = id;
  }
  int IInvincibilityUCEffect.id => id;
  public void visit(IInvincibilityUCEffectVisitor visitor) {
    visitor.visitInvincibilityUCCreateEffect(this);
  }
}

}
