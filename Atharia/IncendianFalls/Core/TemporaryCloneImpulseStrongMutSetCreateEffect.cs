using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct TemporaryCloneImpulseStrongMutSetCreateEffect : ITemporaryCloneImpulseStrongMutSetEffect {
  public readonly int id;
  public TemporaryCloneImpulseStrongMutSetCreateEffect(int id) {
    this.id = id;
  }
  int ITemporaryCloneImpulseStrongMutSetEffect.id => id;
  public void visitITemporaryCloneImpulseStrongMutSetEffect(ITemporaryCloneImpulseStrongMutSetEffectVisitor visitor) {
    visitor.visitTemporaryCloneImpulseStrongMutSetCreateEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitTemporaryCloneImpulseStrongMutSetEffect(this);
  }
}

}
