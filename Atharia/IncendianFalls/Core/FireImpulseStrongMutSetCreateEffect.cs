using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct FireImpulseStrongMutSetCreateEffect : IFireImpulseStrongMutSetEffect {
  public readonly int id;
  public FireImpulseStrongMutSetCreateEffect(int id) {
    this.id = id;
  }
  int IFireImpulseStrongMutSetEffect.id => id;
  public void visitIFireImpulseStrongMutSetEffect(IFireImpulseStrongMutSetEffectVisitor visitor) {
    visitor.visitFireImpulseStrongMutSetCreateEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitFireImpulseStrongMutSetEffect(this);
  }
  public bool isSubtractive() { return false; }
}

}
