using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct BaseSightRangeUCDeleteEffect : IBaseSightRangeUCEffect {
  public readonly int id;
  public BaseSightRangeUCDeleteEffect(int id) {
    this.id = id;
  }
  int IBaseSightRangeUCEffect.id => id;
  public void visit(IBaseSightRangeUCEffectVisitor visitor) {
    visitor.visitBaseSightRangeUCDeleteEffect(this);
  }
}

}
