using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct BaseSightRangeUCMutSetRemoveEffect : IBaseSightRangeUCMutSetEffect {
  public readonly int id;
  public readonly int elementId;
  public BaseSightRangeUCMutSetRemoveEffect(int id, int elementId) {
    this.id = id;
    this.elementId = elementId;
  }
  int IBaseSightRangeUCMutSetEffect.id => id;
  public void visit(IBaseSightRangeUCMutSetEffectVisitor visitor) {
    visitor.visitBaseSightRangeUCMutSetRemoveEffect(this);
  }
}

}
