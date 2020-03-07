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
  public void visit(IInvincibilityUCMutSetEffectVisitor visitor) {
    visitor.visitInvincibilityUCMutSetCreateEffect(this);
  }
}

}
