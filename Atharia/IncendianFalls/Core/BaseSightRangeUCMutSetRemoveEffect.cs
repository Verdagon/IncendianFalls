using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct BaseSightRangeUCMutSetRemoveEffect : IBaseSightRangeUCMutSetEffect {
  public readonly int id;
  public readonly int element;
  public BaseSightRangeUCMutSetRemoveEffect(int id, int element) {
    this.id = id;
    this.element = element;
  }
  int IBaseSightRangeUCMutSetEffect.id => id;
  public void visitIBaseSightRangeUCMutSetEffect(IBaseSightRangeUCMutSetEffectVisitor visitor) {
    visitor.visitBaseSightRangeUCMutSetRemoveEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitBaseSightRangeUCMutSetEffect(this);
  }
}

}
