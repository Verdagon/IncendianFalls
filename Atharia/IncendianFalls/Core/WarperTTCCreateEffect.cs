using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct WarperTTCCreateEffect : IWarperTTCEffect {
  public readonly int id;
  public WarperTTCCreateEffect(int id) {
    this.id = id;
  }
  int IWarperTTCEffect.id => id;
  public void visit(IWarperTTCEffectVisitor visitor) {
    visitor.visitWarperTTCCreateEffect(this);
  }
}

}
