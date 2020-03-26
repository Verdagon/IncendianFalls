using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct BaseSightRangeUCMutSetAddEffect : IBaseSightRangeUCMutSetEffect {
  public readonly int id;
  public readonly int element;
  public BaseSightRangeUCMutSetAddEffect(int id, int element) {
    this.id = id;
    this.element = element;
  }
  int IBaseSightRangeUCMutSetEffect.id => id;
  public void visitIBaseSightRangeUCMutSetEffect(IBaseSightRangeUCMutSetEffectVisitor visitor) {
    visitor.visitBaseSightRangeUCMutSetAddEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitBaseSightRangeUCMutSetEffect(this);
  }
}

}
