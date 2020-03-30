using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct TemporaryCloneImpulseStrongMutSetRemoveEffect : ITemporaryCloneImpulseStrongMutSetEffect {
  public readonly int id;
  public readonly int element;
  public TemporaryCloneImpulseStrongMutSetRemoveEffect(int id, int element) {
    this.id = id;
    this.element = element;
  }
  int ITemporaryCloneImpulseStrongMutSetEffect.id => id;
  public void visitITemporaryCloneImpulseStrongMutSetEffect(ITemporaryCloneImpulseStrongMutSetEffectVisitor visitor) {
    visitor.visitTemporaryCloneImpulseStrongMutSetRemoveEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitTemporaryCloneImpulseStrongMutSetEffect(this);
  }
  public bool isSubtractive() { return true; }
}

}
