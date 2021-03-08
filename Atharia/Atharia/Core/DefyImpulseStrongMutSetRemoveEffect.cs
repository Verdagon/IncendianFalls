using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct DefyImpulseStrongMutSetRemoveEffect : IDefyImpulseStrongMutSetEffect {
  public readonly int id;
  public readonly int element;
  public DefyImpulseStrongMutSetRemoveEffect(int id, int element) {
    this.id = id;
    this.element = element;
  }
  int IDefyImpulseStrongMutSetEffect.id => id;
  public void visitIDefyImpulseStrongMutSetEffect(IDefyImpulseStrongMutSetEffectVisitor visitor) {
    visitor.visitDefyImpulseStrongMutSetRemoveEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitDefyImpulseStrongMutSetEffect(this);
  }
  public bool isSubtractive() { return true; }
}

}
