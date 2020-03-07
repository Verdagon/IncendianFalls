using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct InvincibilityUCMutSetDeleteEffect : IInvincibilityUCMutSetEffect {
  public readonly int id;
  public InvincibilityUCMutSetDeleteEffect(int id) {
    this.id = id;
  }
  int IInvincibilityUCMutSetEffect.id => id;
  public void visit(IInvincibilityUCMutSetEffectVisitor visitor) {
    visitor.visitInvincibilityUCMutSetDeleteEffect(this);
  }
}

}
