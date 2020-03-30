using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct BaseSightRangeUCCreateEffect : IBaseSightRangeUCEffect {
  public readonly int id;
  public readonly BaseSightRangeUCIncarnation incarnation;
  public BaseSightRangeUCCreateEffect(int id, BaseSightRangeUCIncarnation incarnation) {
    this.id = id;
    this.incarnation = incarnation;
  }
  int IBaseSightRangeUCEffect.id => id;
  public void visitIBaseSightRangeUCEffect(IBaseSightRangeUCEffectVisitor visitor) {
    visitor.visitBaseSightRangeUCCreateEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitBaseSightRangeUCEffect(this);
  }
  public bool isSubtractive() { return false; }
}

}
