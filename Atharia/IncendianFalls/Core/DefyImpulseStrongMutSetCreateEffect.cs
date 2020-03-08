using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct DefyImpulseStrongMutSetCreateEffect : IDefyImpulseStrongMutSetEffect {
  public readonly int id;
  public DefyImpulseStrongMutSetCreateEffect(int id) {
    this.id = id;
  }
  int IDefyImpulseStrongMutSetEffect.id => id;
  public void visit(IDefyImpulseStrongMutSetEffectVisitor visitor) {
    visitor.visitDefyImpulseStrongMutSetCreateEffect(this);
  }
}

}