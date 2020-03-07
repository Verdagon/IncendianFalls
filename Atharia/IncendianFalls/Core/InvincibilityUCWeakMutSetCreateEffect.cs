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
  public void visit(IInvincibilityUCWeakMutSetEffectVisitor visitor) {
    visitor.visitInvincibilityUCWeakMutSetCreateEffect(this);
  }
}

}
