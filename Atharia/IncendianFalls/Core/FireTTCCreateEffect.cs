using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct FireTTCCreateEffect : IFireTTCEffect {
  public readonly int id;
  public FireTTCCreateEffect(int id) {
    this.id = id;
  }
  int IFireTTCEffect.id => id;
  public void visit(IFireTTCEffectVisitor visitor) {
    visitor.visitFireTTCCreateEffect(this);
  }
}

}
