using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct ShieldingUCCreateEffect : IShieldingUCEffect {
  public readonly int id;
  public readonly ShieldingUCIncarnation incarnation;
  public ShieldingUCCreateEffect(
      int id,
      ShieldingUCIncarnation incarnation) {
    this.id = id;
    this.incarnation = incarnation;
  }
  int IShieldingUCEffect.id => id;
  public void visit(IShieldingUCEffectVisitor visitor) {
    visitor.visitShieldingUCCreateEffect(this);
  }
}
       
}
