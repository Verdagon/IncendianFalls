using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct InvincibilityUCMutSetRemoveEffect : IInvincibilityUCMutSetEffect {
  public readonly int id;
  public readonly int elementId;
  public InvincibilityUCMutSetRemoveEffect(int id, int elementId) {
    this.id = id;
    this.elementId = elementId;
  }
  int IInvincibilityUCMutSetEffect.id => id;
  public void visit(IInvincibilityUCMutSetEffectVisitor visitor) {
    visitor.visitInvincibilityUCMutSetRemoveEffect(this);
  }
}

}
