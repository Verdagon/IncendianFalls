using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct EvaporateImpulseStrongMutSetRemoveEffect : IEvaporateImpulseStrongMutSetEffect {
  public readonly int id;
  public readonly int element;
  public EvaporateImpulseStrongMutSetRemoveEffect(int id, int element) {
    this.id = id;
    this.element = element;
  }
  int IEvaporateImpulseStrongMutSetEffect.id => id;
  public void visitIEvaporateImpulseStrongMutSetEffect(IEvaporateImpulseStrongMutSetEffectVisitor visitor) {
    visitor.visitEvaporateImpulseStrongMutSetRemoveEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitEvaporateImpulseStrongMutSetEffect(this);
  }
}

}
