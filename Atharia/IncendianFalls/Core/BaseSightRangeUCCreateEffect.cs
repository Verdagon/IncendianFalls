using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct BaseSightRangeUCCreateEffect : IBaseSightRangeUCEffect {
  public readonly int id;
  public BaseSightRangeUCCreateEffect(int id) {
    this.id = id;
  }
  int IBaseSightRangeUCEffect.id => id;
  public void visit(IBaseSightRangeUCEffectVisitor visitor) {
    visitor.visitBaseSightRangeUCCreateEffect(this);
  }
}

}
