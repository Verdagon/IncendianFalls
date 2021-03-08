using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct DefyImpulseStrongMutSetDeleteEffect : IDefyImpulseStrongMutSetEffect {
  public readonly int id;
  public DefyImpulseStrongMutSetDeleteEffect(int id) {
    this.id = id;
  }
  int IDefyImpulseStrongMutSetEffect.id => id;
  public void visitIDefyImpulseStrongMutSetEffect(IDefyImpulseStrongMutSetEffectVisitor visitor) {
    visitor.visitDefyImpulseStrongMutSetDeleteEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitDefyImpulseStrongMutSetEffect(this);
  }
  public bool isSubtractive() { return true; }
}

}
