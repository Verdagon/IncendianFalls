using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct FireImpulseStrongMutSetAddEffect : IFireImpulseStrongMutSetEffect {
  public readonly int id;
  public readonly int element;
  public FireImpulseStrongMutSetAddEffect(int id, int element) {
    this.id = id;
    this.element = element;
  }
  int IFireImpulseStrongMutSetEffect.id => id;
  public void visitIFireImpulseStrongMutSetEffect(IFireImpulseStrongMutSetEffectVisitor visitor) {
    visitor.visitFireImpulseStrongMutSetAddEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitFireImpulseStrongMutSetEffect(this);
  }
  public bool isSubtractive() { return false; }
}

}
