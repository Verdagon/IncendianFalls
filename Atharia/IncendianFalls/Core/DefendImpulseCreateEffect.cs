using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct DefendImpulseCreateEffect : IDefendImpulseEffect {
  public readonly int id;
  public DefendImpulseCreateEffect(int id) {
    this.id = id;
  }
  int IDefendImpulseEffect.id => id;
  public void visit(IDefendImpulseEffectVisitor visitor) {
    visitor.visitDefendImpulseCreateEffect(this);
  }
}

}
