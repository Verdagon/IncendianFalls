using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct BaseSightRangeUCMutSetCreateEffect : IBaseSightRangeUCMutSetEffect {
  public readonly int id;
  public BaseSightRangeUCMutSetCreateEffect(int id) {
    this.id = id;
  }
  int IBaseSightRangeUCMutSetEffect.id => id;
  public void visit(IBaseSightRangeUCMutSetEffectVisitor visitor) {
    visitor.visitBaseSightRangeUCMutSetCreateEffect(this);
  }
}

}
