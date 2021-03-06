using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct NoImpulseStrongMutSetRemoveEffect : INoImpulseStrongMutSetEffect {
  public readonly int id;
  public readonly int element;
  public NoImpulseStrongMutSetRemoveEffect(int id, int element) {
    this.id = id;
    this.element = element;
  }
  int INoImpulseStrongMutSetEffect.id => id;
  public void visitINoImpulseStrongMutSetEffect(INoImpulseStrongMutSetEffectVisitor visitor) {
    visitor.visitNoImpulseStrongMutSetRemoveEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitNoImpulseStrongMutSetEffect(this);
  }
  public bool isSubtractive() { return true; }
}

}
