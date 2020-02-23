using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct ShieldingUCWeakMutSetCreateEffect : IShieldingUCWeakMutSetEffect {
  public readonly int id;
  public ShieldingUCWeakMutSetCreateEffect(int id) {
    this.id = id;
  }
  int IShieldingUCWeakMutSetEffect.id => id;
  public void visit(IShieldingUCWeakMutSetEffectVisitor visitor) {
    visitor.visitShieldingUCWeakMutSetCreateEffect(this);
  }
}

}
