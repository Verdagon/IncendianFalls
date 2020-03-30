using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct DefyImpulseStrongMutSetAddEffect : IDefyImpulseStrongMutSetEffect {
  public readonly int id;
  public readonly int element;
  public DefyImpulseStrongMutSetAddEffect(int id, int element) {
    this.id = id;
    this.element = element;
  }
  int IDefyImpulseStrongMutSetEffect.id => id;
  public void visitIDefyImpulseStrongMutSetEffect(IDefyImpulseStrongMutSetEffectVisitor visitor) {
    visitor.visitDefyImpulseStrongMutSetAddEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitDefyImpulseStrongMutSetEffect(this);
  }
  public bool isSubtractive() { return false; }
}

}
