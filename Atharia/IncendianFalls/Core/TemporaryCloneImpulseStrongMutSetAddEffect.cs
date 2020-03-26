using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct TemporaryCloneImpulseStrongMutSetAddEffect : ITemporaryCloneImpulseStrongMutSetEffect {
  public readonly int id;
  public readonly int element;
  public TemporaryCloneImpulseStrongMutSetAddEffect(int id, int element) {
    this.id = id;
    this.element = element;
  }
  int ITemporaryCloneImpulseStrongMutSetEffect.id => id;
  public void visitITemporaryCloneImpulseStrongMutSetEffect(ITemporaryCloneImpulseStrongMutSetEffectVisitor visitor) {
    visitor.visitTemporaryCloneImpulseStrongMutSetAddEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitTemporaryCloneImpulseStrongMutSetEffect(this);
  }
}

}
