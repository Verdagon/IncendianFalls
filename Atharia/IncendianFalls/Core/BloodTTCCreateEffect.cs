using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct BloodTTCCreateEffect : IBloodTTCEffect {
  public readonly int id;
  public BloodTTCCreateEffect(int id) {
    this.id = id;
  }
  int IBloodTTCEffect.id => id;
  public void visit(IBloodTTCEffectVisitor visitor) {
    visitor.visitBloodTTCCreateEffect(this);
  }
}

}
