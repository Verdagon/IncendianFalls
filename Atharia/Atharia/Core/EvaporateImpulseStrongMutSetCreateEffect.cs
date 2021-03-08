using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct EvaporateImpulseStrongMutSetCreateEffect : IEvaporateImpulseStrongMutSetEffect {
  public readonly int id;
  public EvaporateImpulseStrongMutSetCreateEffect(int id) {
    this.id = id;
  }
  int IEvaporateImpulseStrongMutSetEffect.id => id;
  public void visitIEvaporateImpulseStrongMutSetEffect(IEvaporateImpulseStrongMutSetEffectVisitor visitor) {
    visitor.visitEvaporateImpulseStrongMutSetCreateEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitEvaporateImpulseStrongMutSetEffect(this);
  }
  public bool isSubtractive() { return false; }
}

}
