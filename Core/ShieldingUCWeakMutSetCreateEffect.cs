using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct ShieldingUCWeakMutSetCreateEffect : IShieldingUCWeakMutSetEffect {
  public readonly int id;
  public readonly ShieldingUCWeakMutSetIncarnation incarnation;
  public ShieldingUCWeakMutSetCreateEffect(
      int id,
      ShieldingUCWeakMutSetIncarnation incarnation) {
    this.id = id;
    this.incarnation = incarnation;
  }
  int IShieldingUCWeakMutSetEffect.id => id;
  public void visit(IShieldingUCWeakMutSetEffectVisitor visitor) {
    visitor.visitShieldingUCWeakMutSetCreateEffect(this);
  }
}

}
