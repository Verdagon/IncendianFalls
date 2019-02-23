using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct ShieldingUCMutSetCreateEffect : IShieldingUCMutSetEffect {
  public readonly int id;
  public readonly ShieldingUCMutSetIncarnation incarnation;
  public ShieldingUCMutSetCreateEffect(
      int id,
      ShieldingUCMutSetIncarnation incarnation) {
    this.id = id;
    this.incarnation = incarnation;
  }
  int IShieldingUCMutSetEffect.id => id;
  public void visit(IShieldingUCMutSetEffectVisitor visitor) {
    visitor.visitShieldingUCMutSetCreateEffect(this);
  }
}

}
