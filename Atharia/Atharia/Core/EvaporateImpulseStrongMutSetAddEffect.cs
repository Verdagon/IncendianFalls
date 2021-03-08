using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct EvaporateImpulseStrongMutSetAddEffect : IEvaporateImpulseStrongMutSetEffect {
  public readonly int id;
  public readonly int element;
  public EvaporateImpulseStrongMutSetAddEffect(int id, int element) {
    this.id = id;
    this.element = element;
  }
  int IEvaporateImpulseStrongMutSetEffect.id => id;
  public void visitIEvaporateImpulseStrongMutSetEffect(IEvaporateImpulseStrongMutSetEffectVisitor visitor) {
    visitor.visitEvaporateImpulseStrongMutSetAddEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitEvaporateImpulseStrongMutSetEffect(this);
  }
  public bool isSubtractive() { return false; }
}

}
