using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct ShieldingUCCreateEffect : IShieldingUCEffect {
  public readonly int id;
  public ShieldingUCCreateEffect(int id) {
    this.id = id;
  }
  int IShieldingUCEffect.id => id;
  public void visit(IShieldingUCEffectVisitor visitor) {
    visitor.visitShieldingUCCreateEffect(this);
  }
}

}
