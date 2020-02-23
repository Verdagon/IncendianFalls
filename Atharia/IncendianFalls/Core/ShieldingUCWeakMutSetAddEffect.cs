using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct ShieldingUCWeakMutSetAddEffect : IShieldingUCWeakMutSetEffect {
  public readonly int id;
  public readonly int elementId;
  public ShieldingUCWeakMutSetAddEffect(int id, int elementId) {
    this.id = id;
    this.elementId = elementId;
  }
  int IShieldingUCWeakMutSetEffect.id => id;
  public void visit(IShieldingUCWeakMutSetEffectVisitor visitor) {
    visitor.visitShieldingUCWeakMutSetAddEffect(this);
  }
}

}
