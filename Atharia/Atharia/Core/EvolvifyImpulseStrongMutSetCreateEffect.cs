using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct EvolvifyImpulseStrongMutSetCreateEffect : IEvolvifyImpulseStrongMutSetEffect {
  public readonly int id;
  public EvolvifyImpulseStrongMutSetCreateEffect(int id) {
    this.id = id;
  }
  int IEvolvifyImpulseStrongMutSetEffect.id => id;
  public void visitIEvolvifyImpulseStrongMutSetEffect(IEvolvifyImpulseStrongMutSetEffectVisitor visitor) {
    visitor.visitEvolvifyImpulseStrongMutSetCreateEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitEvolvifyImpulseStrongMutSetEffect(this);
  }
  public bool isSubtractive() { return false; }
}

}
