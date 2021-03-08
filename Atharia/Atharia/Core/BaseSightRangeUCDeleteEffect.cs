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
  public void visitIBaseSightRangeUCEffect(IBaseSightRangeUCEffectVisitor visitor) {
    visitor.visitBaseSightRangeUCDeleteEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitBaseSightRangeUCEffect(this);
  }
  public bool isSubtractive() { return true; }
}

}
