using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct IImpulseStrongMutBunchDeleteEffect : IIImpulseStrongMutBunchEffect {
  public readonly int id;
  public IImpulseStrongMutBunchDeleteEffect(int id) {
    this.id = id;
  }
  int IIImpulseStrongMutBunchEffect.id => id;
  public void visit(IIImpulseStrongMutBunchEffectVisitor visitor) {
    visitor.visitIImpulseStrongMutBunchDeleteEffect(this);
  }
}

}
