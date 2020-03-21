using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct BaseSightRangeUCMutSetAddEffect : IBaseSightRangeUCMutSetEffect {
  public readonly int id;
  public readonly int elementId;
  public BaseSightRangeUCMutSetAddEffect(int id, int elementId) {
    this.id = id;
    this.elementId = elementId;
  }
  int IBaseSightRangeUCMutSetEffect.id => id;
  public void visit(IBaseSightRangeUCMutSetEffectVisitor visitor) {
    visitor.visitBaseSightRangeUCMutSetAddEffect(this);
  }
}

}
