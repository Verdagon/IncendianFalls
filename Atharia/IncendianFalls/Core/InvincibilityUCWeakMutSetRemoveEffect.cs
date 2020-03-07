using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct InvincibilityUCWeakMutSetRemoveEffect : IInvincibilityUCWeakMutSetEffect {
  public readonly int id;
  public readonly int elementId;
  public InvincibilityUCWeakMutSetRemoveEffect(int id, int elementId) {
    this.id = id;
    this.elementId = elementId;
  }
  int IInvincibilityUCWeakMutSetEffect.id => id;
  public void visit(IInvincibilityUCWeakMutSetEffectVisitor visitor) {
    visitor.visitInvincibilityUCWeakMutSetRemoveEffect(this);
  }
}

}
