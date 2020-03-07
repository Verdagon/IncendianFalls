using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct InvincibilityUCWeakMutSetAddEffect : IInvincibilityUCWeakMutSetEffect {
  public readonly int id;
  public readonly int elementId;
  public InvincibilityUCWeakMutSetAddEffect(int id, int elementId) {
    this.id = id;
    this.elementId = elementId;
  }
  int IInvincibilityUCWeakMutSetEffect.id => id;
  public void visit(IInvincibilityUCWeakMutSetEffectVisitor visitor) {
    visitor.visitInvincibilityUCWeakMutSetAddEffect(this);
  }
}

}
