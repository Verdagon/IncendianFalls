using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct ShieldingUCMutSetRemoveEffect : IShieldingUCMutSetEffect {
  public readonly int id;
  public readonly int elementId;
  public ShieldingUCMutSetRemoveEffect(int id, int elementId) {
    this.id = id;
    this.elementId = elementId;
  }
  int IShieldingUCMutSetEffect.id => id;
  public void visit(IShieldingUCMutSetEffectVisitor visitor) {
    visitor.visitShieldingUCMutSetRemoveEffect(this);
  }
}

}
