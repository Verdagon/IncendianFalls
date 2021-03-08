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
  public void visitIIImpulseStrongMutBunchEffect(IIImpulseStrongMutBunchEffectVisitor visitor) {
    visitor.visitIImpulseStrongMutBunchDeleteEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitIImpulseStrongMutBunchEffect(this);
  }
  public bool isSubtractive() { return true; }
}

}
