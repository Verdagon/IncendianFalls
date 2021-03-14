using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct EvolvifyImpulseStrongMutSetRemoveEffect : IEvolvifyImpulseStrongMutSetEffect {
  public readonly int id;
  public readonly int element;
  public EvolvifyImpulseStrongMutSetRemoveEffect(int id, int element) {
    this.id = id;
    this.element = element;
  }
  int IEvolvifyImpulseStrongMutSetEffect.id => id;
  public void visitIEvolvifyImpulseStrongMutSetEffect(IEvolvifyImpulseStrongMutSetEffectVisitor visitor) {
    visitor.visitEvolvifyImpulseStrongMutSetRemoveEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitEvolvifyImpulseStrongMutSetEffect(this);
  }
  public bool isSubtractive() { return true; }
}

}
