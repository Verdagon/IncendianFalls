using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct FireImpulseStrongMutSetRemoveEffect : IFireImpulseStrongMutSetEffect {
  public readonly int id;
  public readonly int element;
  public FireImpulseStrongMutSetRemoveEffect(int id, int element) {
    this.id = id;
    this.element = element;
  }
  int IFireImpulseStrongMutSetEffect.id => id;
  public void visitIFireImpulseStrongMutSetEffect(IFireImpulseStrongMutSetEffectVisitor visitor) {
    visitor.visitFireImpulseStrongMutSetRemoveEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitFireImpulseStrongMutSetEffect(this);
  }
  public bool isSubtractive() { return true; }
}

}
