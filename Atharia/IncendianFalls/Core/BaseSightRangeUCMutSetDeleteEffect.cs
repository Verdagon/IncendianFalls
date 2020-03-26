using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct BaseSightRangeUCMutSetDeleteEffect : IBaseSightRangeUCMutSetEffect {
  public readonly int id;
  public BaseSightRangeUCMutSetDeleteEffect(int id) {
    this.id = id;
  }
  int IBaseSightRangeUCMutSetEffect.id => id;
  public void visitIBaseSightRangeUCMutSetEffect(IBaseSightRangeUCMutSetEffectVisitor visitor) {
    visitor.visitBaseSightRangeUCMutSetDeleteEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitBaseSightRangeUCMutSetEffect(this);
  }
}

}
