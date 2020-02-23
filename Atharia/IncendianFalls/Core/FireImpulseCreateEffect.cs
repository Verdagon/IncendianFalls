using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct FireImpulseCreateEffect : IFireImpulseEffect {
  public readonly int id;
  public FireImpulseCreateEffect(int id) {
    this.id = id;
  }
  int IFireImpulseEffect.id => id;
  public void visit(IFireImpulseEffectVisitor visitor) {
    visitor.visitFireImpulseCreateEffect(this);
  }
}

}
