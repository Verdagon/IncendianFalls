using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct ShieldingUCMutSetAddEffect : IShieldingUCMutSetEffect {
  public readonly int id;
  public readonly int elementId;
  public ShieldingUCMutSetAddEffect(int id, int elementId) {
    this.id = id;
    this.elementId = elementId;
  }
  int IShieldingUCMutSetEffect.id => id;
  public void visit(IShieldingUCMutSetEffectVisitor visitor) {
    visitor.visitShieldingUCMutSetAddEffect(this);
  }
}

}
