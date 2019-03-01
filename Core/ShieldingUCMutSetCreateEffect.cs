using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct ShieldingUCMutSetCreateEffect : IShieldingUCMutSetEffect {
  public readonly int id;
  public ShieldingUCMutSetCreateEffect(int id) {
    this.id = id;
  }
  int IShieldingUCMutSetEffect.id => id;
  public void visit(IShieldingUCMutSetEffectVisitor visitor) {
    visitor.visitShieldingUCMutSetCreateEffect(this);
  }
}

}
